

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


            ViewData["Insumos"] = insumos;


            return View();
        //}
    }
}

