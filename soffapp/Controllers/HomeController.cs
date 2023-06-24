using Microsoft.AspNetCore.Mvc;
using soffapp.Models;
using soffapp.Models.ViewModels;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace soffapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SoffDataContext _dbcontext;

        public HomeController(SoffDataContext context)
        {
            _dbcontext = context;
        }


        //public IActionResult resumenVenta()
        //{
        //    DateTime FechaInicio = DateTime.Now;
        //    FechaInicio = FechaInicio.AddDays(-5);

        //    List<VMVenta> Lista = (from tbventa in _dbcontext.Venta
        //                           where tbventa.FechaVenta.Date >= FechaInicio.Date
        //                           group tbventa by tbventa.FechaVenta.Date into grupo
        //                           select new VMVenta

        //                           {
        //                               fecha = grupo.Key.ToString("dd/MM/yyyy"),
        //                               cantidad = grupo.Count(),
        //                           }).ToList();

        //    return StatusCode(StatusCodes.Status200OK, Lista);
        //}

        //public IActionResult resumenVenta()
        //{
        //    List<VMVenta> Lista = (from tbventa in _dbcontext.OrdenVenta
        //                 join venta in _dbcontext.Venta on tbventa.IdOrden equals venta.IdVenta
        //                           group tbventa by tbventa.Cantidad into grupo
        //                           select new VMVenta
        //                 {
        //                     fecha_venta = venta.FechaVenta,
        //                               cantidad =  grupo.Count(),
        //                 }).ToList();

        //    return StatusCode(StatusCodes.Status200OK, Lista);
        //}


        public IActionResult resumenVenta()
        {

            List<VMVenta> Lista = (from tbventa in _dbcontext.Productos

                                   group tbventa by tbventa.Nombre into grupo
                                   orderby grupo.Count() descending
                                   select new VMVenta
                                   {
                                       nombre = grupo.Key,

                                       precio = grupo.Count(),
                                   }).ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
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
            return View();
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