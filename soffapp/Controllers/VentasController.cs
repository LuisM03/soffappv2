using Microsoft.AspNetCore.Mvc;
using soffapp.Models;

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

        public async Task<IActionResult> Delete(string id) 
        {
            var venta = await context.Venta.FindAsync(long.Parse(id));
            if (venta == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                context.Venta.Remove(venta);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
