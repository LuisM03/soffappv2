using Microsoft.AspNetCore.Mvc;
using soffapp.Models;
using System.Diagnostics;
using soffapp.Areas;
namespace soffapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("./Areas/Identity/Pages/Account/Login");

            //}
            //else
            //{
                return View();

            //}

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