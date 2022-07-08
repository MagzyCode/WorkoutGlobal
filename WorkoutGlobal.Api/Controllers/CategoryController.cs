using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CategoryController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateCategory([FromBody] CreationCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            var categoryId = await _repositoryManager.CategoryRepository.CreateCategoryAsync(category);

            return Created($"api/videos/{categoryId}", categoryId);
        }

        [HttpPut("{categoryId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] CategoryDto categoryDto)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var updateCategory = _mapper.Map<Category>(categoryDto);

            await _repositoryManager.CategoryRepository.UpdateCategoryAsync(updateCategory);

            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.CategoryRepository.DeleteCategoryAsync(category);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _repositoryManager.CategoryRepository.GetAllCategoriesAsync();

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }


        [HttpGet("{categoryId}/videos")]
        public async Task<IActionResult> GetCategoryVideos(Guid categoryId)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var categoryVideos = await _repositoryManager.CategoryRepository.GetCategoryVideosAsync(categoryId);

            var categoryVideosDto = _mapper.Map<IEnumerable<VideoDto>>(categoryVideos);

            return Ok(categoryVideosDto);
        }

        [HttpGet("{categoryId}/courses")]
        public async Task<IActionResult> GetCategoryCourses(Guid categoryId)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var categoryCourses = await _repositoryManager.CategoryRepository.GetCategoryCoursesAsync(categoryId);

            var categoryCoursesDto = _mapper.Map<IEnumerable<CourseDto>>(categoryCourses);

            return Ok(categoryCoursesDto);
        }

        [HttpGet("category/{categoryName}")]
        public async Task<IActionResult> GetCategoryByName(string categoryName)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByNameAsync(categoryName);

            if (category == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no category with name.",
                    Details = new StackTrace().ToString()
                });

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }

    }
}
