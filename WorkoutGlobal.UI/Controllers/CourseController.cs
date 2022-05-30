using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;
using WorkoutGlobal.UI.Models.Enums;
using WorkoutGlobal.UI.ViewModels;

namespace WorkoutGlobal.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        public CourseController(
            ICourseService courseService,
            IMapper mapper,
            IServiceManager serviceManager)
        {
            _courseService = courseService;
            _mapper = mapper;
            _serviceManager = serviceManager;
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

        public async Task<IActionResult> AddCourse(string username)
        {
            username = User.Identity.Name;
            var categoties = await _serviceManager.CategoryService.GetAllCategoriesAsync();
            var categotiesNames = categoties.Select(x => x.CategoryName).ToList();

            var user = await _serviceManager.UserService.GetUserByUsernameAsync(username);

            var videos = await _serviceManager.UserService.GetTrainerCreatedVideosAsync(user.Id);

            var possibleCourseVideos = new List<(Guid videoId, string videoTitle)>();
            foreach (var video in videos)
                possibleCourseVideos.Add((video.Id, video.Title));

            return View(new CreationCourseViewModel()
            {
                Categories = categotiesNames,
                CourseVideos = possibleCourseVideos
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(string username, CreationCourseViewModel creationCourseViewModel)
        {
            var user = await _serviceManager.UserService.GetUserByUsernameAsync(username);
            creationCourseViewModel.CreatorId = user.Id;

            var category = await _serviceManager.CategoryService.GetCategoryByNameAsync(creationCourseViewModel.CategoryName);

            var course = _mapper.Map<Course>(creationCourseViewModel);
            course.CategoryId = category.Id;

            var id = await _serviceManager.CourseService.CreateCourseAsync(course);

            for (int i = 0; i < creationCourseViewModel.SelectedVideos.Count; i++)
            {
                _ = Guid.TryParse(creationCourseViewModel.SelectedVideos[i], out Guid guid);
                if (guid != Guid.Empty)
                {
                    await _serviceManager.CourseVideoService.CreateCourseVideoAsync(
                        new CourseVideo()
                        {
                            VideoId = guid,
                            CourseId = id,
                            SequenceNumber = i
                        });
                }
            }
            
            return RedirectToAction("CoursesList", "Course");
        }

        public async Task<IActionResult> ShowCourse(CourseViewModel courseViewModel)
        {
            var user = await _serviceManager.UserService.GetUserByUsernameAsync(User.Identity.Name);

            var userSubscribeCourses = await _serviceManager.UserService.GetUserSubscribeCoursesByIdAsync(user.Id);

            var isCourseInSubscription = userSubscribeCourses.Any(x => x.SubscribeCourseId == courseViewModel.Id);

            if (!isCourseInSubscription)
                await _serviceManager.SubscribeCourseService.CreateSubscribeCourseAsync(new SubscribeCourse()
                {
                    SubscriberId = user.Id,
                    SubscribeCourseId = courseViewModel.Id,
                    CourseCompletionRate = (int)CourseCompletionRate.InProgress
                });

            var courseVideos = await _serviceManager.CourseService.GetCourseVideosAsync(courseViewModel.Id);

            var coursesVideosViewModel = _mapper.Map<IEnumerable<VideoViewModel>>(courseVideos);

            return View(coursesVideosViewModel.ToList());
        }
    }
}
