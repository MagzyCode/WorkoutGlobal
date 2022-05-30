using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.ViewModels;

namespace WorkoutGlobal.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        public CategoryController(
            IMapper mapper,
            IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> SelectCategory(Guid categoryId)
        {
            var categories = await _serviceManager.CategoryService.GetAllCategoriesAsync();
            var categoryNames = categories.Select(x => x.CategoryName).ToList();

            return View(new CategorySelectViewModel()
            {
                CategoryNames = categoryNames
            });
        }

        public async Task<IActionResult> ShowCategory(CategorySelectViewModel categorySelectViewModel)
        {
            var category = await _serviceManager.CategoryService.GetCategoryByNameAsync(categorySelectViewModel.CategoryName);

            var videos = await _serviceManager.CategoryService.GetCategoryVideosAsync(category.Id);
            var videosViewModel = _mapper.Map<IEnumerable<VideoViewModel>>(videos).ToList();

            var courses = await _serviceManager.CategoryService.GetCategoryCoursesAsync(category.Id);
            var coursesViewModel = _mapper.Map<IEnumerable<CourseViewModel>>(courses).ToList();

            return View(new CategoryVideosAndCoursesViewModel()
            {
                Videos = videosViewModel,
                Courses = coursesViewModel
            });
        }
    }
}
