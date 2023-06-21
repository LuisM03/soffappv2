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
            var ventas = context.Venta.Join(
                  context.OrdenVenta,
                  venta => venta.IdVenta,
                  orden => orden.IdVenta,
                  (venta, orden) => new { venta, orden }
              ).ToList();
            ViewBag.Ventas = ventas;
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
