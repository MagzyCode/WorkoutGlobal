using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            : base(workoutGlobalContext, configurationManager)
        {
        }

        public async Task<Guid> CreateCategoryAsync(Category category)
        {
            await CreateAsync(category);
            await SaveChangesAsync();

            return category.Id;
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            Delete(category);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await GetAll().ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategoryAsync(Guid categoryId)
        {
            var model = await GetModelAsync(categoryId);

            return model;
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var model = await GetAll().Where(x => x.CategoryName == name).FirstOrDefaultAsync();

            return model;
        }

        public async Task<IEnumerable<Course>> GetCategoryCoursesAsync(Guid categoryId)
        {
            var courses = await Context.Courses
                .Where(model => model.CategoryId == categoryId)
                .ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<Video>> GetCategoryVideosAsync(Guid categoryId)
        {
            var videos = await Context.Videos
                .Where(model => model.CategoryId == categoryId && model.IsPublic == true)
                .ToListAsync();

            return videos;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            Update(category);
            await SaveChangesAsync();
        }
    }
}
