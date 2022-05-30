using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface ICategoryService : IApiData
    {
        [Post("/api/categories")]
        public Task CreateCategoryAsync([Body] Category category);

        [Put("/api/categories/{categoryId}")]
        public Task UpdateCategoryAsync(Guid categoryId, [Body] Category category);

        [Delete("/api/categories/{categoryId}")]
        public Task DeleteCategoryAsync(Guid categoryId);

        [Get("/api/categories")]
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();

        [Get("/api/categories/{categoryId}")]
        public Task<Category> GetCategoryAsync(Guid categoryId);

        [Get("/api/categories/{categoryId}/videos")]
        public Task<IEnumerable<Video>> GetCategoryVideosAsync(Guid categoryId);

        [Get("/api/categories/{categoryId}/courses")]
        public Task<IEnumerable<Course>> GetCategoryCoursesAsync(Guid categoryId);

        [Get("/api/categories/name/{categoryName}")]
        public Task<Category> GetCategoryByNameAsync(string categoryName);
    }
}
