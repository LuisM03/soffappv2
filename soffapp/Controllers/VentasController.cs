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
    }
}
