using CRUDCORE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CRUDCORE.Permisos;

namespace CRUDCORE.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            var idUsuario = HttpContext.Session.GetInt32("idUsuario");
            return RedirectToAction("Login", "Mantenedor");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}