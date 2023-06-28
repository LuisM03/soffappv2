using Humanizer;
using Microsoft.AspNetCore.Mvc;
using soffapp.Models;
using soffapp.Models.ViewModels;
using System.Diagnostics;
namespace soffapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SoffDataContext _dbcontext;

        public HomeController(SoffDataContext context)
        {
            _dbcontext = context;
        }


        public IActionResult resumenVenta()
        {


            DateTime fechaActual = DateTime.Now;
            DateTime fechaHace7Dias = fechaActual.AddDays(-7);


            var productoMasVendido = _dbcontext.Productos
                  .Join(
                      _dbcontext.OrdenVenta.Where(ov => ov.IdVentaNavigation.FechaVenta >= fechaHace7Dias && ov.IdVentaNavigation.FechaVenta <= fechaActual),
                      producto => producto.IdProducto,
                      ordenVenta => ordenVenta.IdProducto,
                      (producto, ordenVenta) => new
                      {
                          Producto = producto,
                          Cantidad = ordenVenta.Cantidad
                      })
                  .GroupBy(p => p.Producto)
                  .OrderByDescending(group => group.Sum(p => p.Cantidad))
                  .Select(group => new {
                      group.Key.Nombre,
                      CantidadVecesVendida = group.Sum(p => p.Cantidad),
                  })
                  .FirstOrDefault();


            ViewBag.Fecha = fechaHace7Dias.ToString();
            ViewBag.ProductoMasVendido = productoMasVendido;
            return StatusCode(StatusCodes.Status200OK, productoMasVendido);
        }



        public IActionResult resumenProducto()
        {

            List<VMProducto> Lista = (from tbdetalleventa in _dbcontext.OrdenVenta

                                      group tbdetalleventa by tbdetalleventa.Total into grupo
                                      orderby grupo.Count() descending
                                      select new VMProducto
                                      {
                                          Cantidad = grupo.Key,
                                          total = grupo.Count(),
                                      }).ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }




        public IActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");

            }
            else
            {
                DateTime fechaActual = DateTime.Now;
                DateTime fechaLunes = fechaActual.AddDays(-(int)fechaActual.DayOfWeek + (int)DayOfWeek.Monday);
                int ventasRealizadas = _dbcontext.Venta.Count(v => v.FechaVenta >= fechaLunes && v.FechaVenta <= fechaActual);
                ViewBag.CantidadVentas = ventasRealizadas;


                DateTime fechaHace7Dias = fechaActual.AddDays(-7);


                var productoMasVendido = _dbcontext.Productos
                      .Join(
                          _dbcontext.OrdenVenta.Where(ov => ov.IdVentaNavigation.FechaVenta >= fechaHace7Dias && ov.IdVentaNavigation.FechaVenta <= fechaActual),
                          producto => producto.IdProducto,
                          ordenVenta => ordenVenta.IdProducto,
                          (producto, ordenVenta) => new
                          {
                              Producto = producto,
                              Cantidad = ordenVenta.Cantidad
                          })
                      .GroupBy(p => p.Producto)
                      .OrderByDescending(group => group.Sum(p => p.Cantidad))
                      .Select(group => new {
                          group.Key.Nombre,
                          CantidadVecesVendida = group.Sum(p => p.Cantidad),
                      })
                      .FirstOrDefault();


                ViewBag.Fecha = fechaHace7Dias.ToString();
                ViewBag.ProductoMasVendido = productoMasVendido;


                decimal totalVentas = _dbcontext.Venta.Sum(v => v.Total);
                ViewBag.Total = totalVentas;
                return View();
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}