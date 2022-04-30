using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Sets logger of controller.
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Index action-method.
        /// </summary>
        /// <returns>Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// Privacy action-method.
        /// </summary>
        /// <returns>Privacy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Error action-method.
        /// </summary>
        /// <returns>View with errors.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}