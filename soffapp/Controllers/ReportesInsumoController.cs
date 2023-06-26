

using Microsoft.AspNetCore.Mvc;
using soffapp.Models;

public class ReportesInsumoController : Controller
{
    private readonly SoffDataContext _context;

    public ReportesInsumoController(SoffDataContext context)
    {
        _context = context;
    }

    public IActionResult Index()


    {

        //if (!User.Identity.IsAuthenticated)
        //{
        //    return Redirect("/");
        //}
        //else
        //{

            var insumos = _context.Insumos.ToList();
            //var productos = _context.Productos.ToList();
            //var ventas = _context.Ventas.ToList();

            ViewData["Insumos"] = insumos;
            //ViewData["Productos"] = productos;
            //ViewData["Ventas"] = ventas;

            return View();
        //}
    }
}

