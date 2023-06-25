using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using soffapp.Models;
using System.Diagnostics;

namespace soffapp.Controllers
{
    public class VentasController : Controller
    {
        private SoffDataContext context = new SoffDataContext();

        public VentasController(SoffDataContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var ventas = context.Venta
                .Join(
                  context.OrdenVenta,
                  venta => venta.IdVenta,
                  orden => orden.IdVenta,
                  (venta, orden) => new { Venta = venta, OrdenVenta = orden }
              ).ToList();
            var ventasConOrdenes = ventas
                .GroupBy(
                    v => v.Venta,
                    v => v.OrdenVenta,
                    (venta, ordenes) =>
                    {
                        venta.OrdenVenta = ordenes.ToList();
                        return new { 
                            venta.IdVenta, 
                            CantidadOrdenes  = venta.OrdenVenta.Count(),
                            venta.Metodo,
                            venta.TipoVenta,
                            venta.FechaVenta,
                            venta.Total
                        };
                    }
                ).ToList();
            ViewBag.Ventas = ventasConOrdenes;
            return View();
        }

        public async Task<IActionResult> Create(Ventum venta)
        {
            if (!ModelState.IsValid)
            {
                venta.Total = 0;
                venta.Metodo = "";
                venta.FechaVenta = DateTime.Parse(DateTime.Today.ToString("D"));
                venta.TipoVenta = "";
                context.Add(venta);
                context.SaveChanges();
                return Redirect($"/OrdenVenta/Create/{venta.IdVenta}");
            }else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult ReportePDF()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            gfx.DrawString("Hello, World!", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormat.Center);
            string filename = "HelloWorld.pdf";
            document.Save(filename);
            return View(document);
        }

        public async Task<IActionResult> Delete(string id) 
        {
            var ventaOrdenes = await context.Venta
                .Include(v => v.OrdenVenta)
                .FirstOrDefaultAsync(v => v.IdVenta == long.Parse(id));
            if (ventaOrdenes == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var ordenes = ventaOrdenes.OrdenVenta.ToList();
                if (ventaOrdenes.OrdenVenta.Count() > 0)
                {
                    foreach (var orden in ordenes)
                    {
                        context.OrdenVenta.Remove(orden);
                        context.SaveChanges();
                    }
                    context.Venta.Remove(ventaOrdenes);
                    context.SaveChanges();
                }
                else
                {
                    context.Venta.Remove(ventaOrdenes);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}
