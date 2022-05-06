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
        
        /// <summary>
        /// Sets logger of controller.
        /// </summary>
        public HomeController()
        {

        }

        /// <summary>
        /// Represents action-method for user log in.
        /// </summary>
        /// <returns>Login view.</returns>
        public IActionResult Login()
        {
            return View();
        }
    }
}