using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.ViewModels;

namespace WorkoutGlobal.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(
            ICourseService courseService,
            IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CoursesList()
        {
            var courses = await _courseService.GetAllCoursesAsync();

            var coursesViewModel = _mapper.Map<IEnumerable<CourseViewModel>>(courses);

            return View(coursesViewModel);
        }

        public async Task<IActionResult> ShowCourseTitle(Guid courseId)
        {
            var course = await _courseService.GetCourseAsync(courseId);

            var courseViewModel = _mapper.Map<CourseViewModel>(course);

            return View(courseViewModel);
        }
    }
}
