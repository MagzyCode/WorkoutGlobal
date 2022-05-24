using Microsoft.AspNetCore.Mvc;

namespace WorkoutGlobal.UI.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
