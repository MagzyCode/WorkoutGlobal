using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;
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

        public IActionResult AddCourse()
        {
            return View(new CourseViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseViewModel courseViewModel)
        {
            courseViewModel.CreatorId = Guid.Parse("4e14c6d2-cae1-43ba-bd32-0f2dc2225f5a");
            
            if (courseViewModel.CourseImageForm != null)
            {
                using var binaryReader = new BinaryReader(courseViewModel.CourseImageForm?.OpenReadStream());
                courseViewModel.CourseImage = binaryReader.ReadBytes((int)courseViewModel.CourseImageForm?.Length);
            }
           
            var course = _mapper.Map<Course>(courseViewModel);

            await _courseService.CreateCourseAsync(course);

            return RedirectToAction("CoursesList", "Course");
        }
    }
}
