using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class CategoryService : BaseService<ICategoryService>, ICategoryService
    {
        public CategoryService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreateCategoryAsync(Category category)
            => await Service.CreateCategoryAsync(category);

        public async Task DeleteCategoryAsync(Guid categoryId)
            => await Service.DeleteCategoryAsync(categoryId);

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
            => await Service.GetAllCategoriesAsync();

        public async Task<Category> GetCategoryAsync(Guid categoryId)
            => await Service.GetCategoryAsync(categoryId);

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
            => await Service.GetCategoryByNameAsync(categoryName);

        public async Task<IEnumerable<Course>> GetCategoryCoursesAsync(Guid categoryId)
            => await Service.GetCategoryCoursesAsync(categoryId);

        public async Task<IEnumerable<Video>> GetCategoryVideosAsync(Guid categoryId)
            => await Service.GetCategoryVideosAsync(categoryId);

        public async Task UpdateCategoryAsync(Guid categoryId, Category category)
            => await Service.UpdateCategoryAsync(categoryId, category);
    }
}
