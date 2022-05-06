using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.UI.Filters.ActionFilters;
using WorkoutGlobal.UI.Models;
using WorkoutGlobal.UI.ViewModels.Authentication;

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
            return View(new UserAuthorizationViewModel());
        }

        /// <summary>
        /// Represents POST action-method for user log in.
        /// </summary>
        /// <param name="userAuthorizationViewModel"></param>
        /// <returns></returns>
        [ModelValidationFilter]
        public IActionResult Login(UserAuthorizationViewModel userAuthorizationViewModel)
        {


            return RedirectToAction("Index");
        }

        /// <summary>
        /// Represents action-method for home page.
        /// </summary>
        /// <returns>Home page view.</returns>
        public IActionResult Index() 
        { 
            return View();
        }
    }
}