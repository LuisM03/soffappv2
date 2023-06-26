using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using soffapp.Models;
using System.Diagnostics;

namespace soffapp.Controllers
{
    public class ComprasController : Controller
    {
        private SoffDataContext context = new SoffDataContext();

        public ComprasController(SoffDataContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            //var compras = context.Compras
            //    .Join(
            //      context.OrdenCompras,
            //      compra => compra.IdCompra,
            //      orden => orden.IdCompra,
            //      (compra, orden) => new { Compra = compra, OrdenCompras = orden }
            //  )
            //    .Join(
            //        context.Proveedors,
            //        compra => compra.Compra.IdProveedor,
            //        proveedor => proveedor.IdProveedor,
            //        (compra, proveedor) => new { Compra = compra, Proveedor = proveedor }
            //    ).Select(
            //        x => new
            //        {
            //            IdCompra = ,
            //        }
            //    )
            //    .ToList();

            var compras = context.Compras
                .Join(context.Proveedors,
                    c => c.IdProveedor,
                    p => p.IdProveedor,
                    (c, p) => new { Compra = c, Proveedor = p })
                .Join(context.OrdenCompras,
                    cp => cp.Compra.IdCompra,
                    oc => oc.IdCompra,
                    (cp, oc) => new { CompraProveedor = cp, OrdenCompra = oc })
                .GroupBy(result => new
                {
                    result.CompraProveedor.Compra.IdCompra,
                    result.CompraProveedor.Compra.FechaCompra,
                    result.CompraProveedor.Compra.Total,
                    result.CompraProveedor.Proveedor.Nombre
                })
                .Select(
                    result => new
                    {
                        result.Key.IdCompra,
                        result.Key.FechaCompra,
                        result.Key.Total,
                        result.Key.Nombre,
                        CantidadOrdenes = result.Count()
                    }
                )
                .ToList();



            ViewBag.Compras = compras;
            return View();
        }

        public async Task<IActionResult> Create(Compra compra)
        {
            if (!ModelState.IsValid)
            {
                var proveedor = await context.Proveedors.ToListAsync();
                if (proveedor.Count() > 0)
                {
                    compra.IdProveedor = proveedor[0].IdProveedor;
                    compra.Total = 0;
                    compra.FechaCompra = DateTime.Parse(DateTime.Today.ToString("D"));
                    context.Add(compra);
                    context.SaveChanges();
                    return Redirect($"/OrdenCompras/Create/{compra.IdCompra}");
                }
                else
                {
                    return Redirect("/");
                }

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //public IActionResult ReportePDF()
        //{
        //    PdfDocument document = new PdfDocument();
        //    PdfPage page = document.AddPage();
        //    XGraphics gfx = XGraphics.FromPdfPage(page);
        //    XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

        //    gfx.DrawString("Hello, World!", font, XBrushes.Black,
        //        new XRect(0, 0, page.Width, page.Height),
        //        XStringFormat.Center);
        //    string filename = "HelloWorld.pdf";
        //    document.Save(filename);
        //    return View(document);
        //}

        //public async Task<IActionResult> Delete(string id)
        //{
        //    var compra = await context.Compras.FindAsync(long.Parse(id));
        //    if (compra == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        context.Compras.Remove(compra);
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //}

        public async Task<IActionResult> Delete(string id)
        {
            var compraOrdenes = await context.Compras
                .Include(v => v.OrdenCompras)
                .FirstOrDefaultAsync(v => v.IdCompra == long.Parse(id));
            if (compraOrdenes == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var ordenes = compraOrdenes.OrdenCompras.ToList();
                if (compraOrdenes.OrdenCompras.Count() > 0)
                {
                    foreach (var orden in ordenes)
                    {
                        context.OrdenCompras.Remove(orden);
                        context.SaveChanges();
                    }
                    context.Compras.Remove(compraOrdenes);
                    context.SaveChanges();
                }
                else
                {
                    context.Compras.Remove(compraOrdenes);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}
