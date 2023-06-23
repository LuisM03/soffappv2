using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soffapp.Models;

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
            var compras = context.Compras
                .Join(
                  context.OrdenCompras,
                  compra => compra.IdCompra,
                  orden => orden.IdCompra,
                  (compra, orden) => new { Compra = compra, OrdenCompras = orden }
              ).ToList();
            var comprasConOrdenes = compras
                .GroupBy(
                    v => v.Compra,
                    v => v.OrdenCompras,
                    (compra, ordenes) =>
                    {
                        compra.OrdenCompras = ordenes.ToList();
                        return new
                        {
                            compra.IdCompra,
                            CantidadOrdenes = compra.OrdenCompras.Count(),
                            compra.IdProveedor,
                            compra.FechaCompra,
                            compra.Total
                        };
                    }
                ).ToList();
            ViewBag.Compras = comprasConOrdenes;
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

        public async Task<IActionResult> Delete(string id)
        {
            var compra = await context.Compras.FindAsync(long.Parse(id));
            if (compra == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                context.Compras.Remove(compra);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
