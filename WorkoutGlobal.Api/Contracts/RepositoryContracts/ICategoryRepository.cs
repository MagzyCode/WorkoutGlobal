using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface ICategoryRepository
    {
        public Task CreateCategoryAsync(Category category);

        public Task UpdateCategoryAsync(Category category);

        public Task DeleteCategoryAsync(Category category);

        public Task<IEnumerable<Category>> GetAllCategoriesAsync();

        public Task<Category> GetCategoryAsync(Guid categoryId);

        public Task<IEnumerable<Video>> GetCategoryVideosAsync(Guid categoryId);

        public Task<IEnumerable<Course>> GetCategoryCoursesAsync(Guid categoryId);

        public Task<Category> GetCategoryByNameAsync(string name);

    }
}
