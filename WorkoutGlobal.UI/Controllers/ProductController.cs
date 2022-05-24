using Microsoft.AspNetCore.Mvc;

namespace WorkoutGlobal.UI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
