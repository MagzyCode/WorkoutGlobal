using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        //private readonly ICourseRepository _courseRepository;
        //private readonly IVideoRepository _videoRepository;

        public CategoryRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            //ICourseRepository courseRepository,
            //IVideoRepository videoRepository) 
            : base(workoutGlobalContext, configurationManager)
        {
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await CreateAsync(category);
            await SaveChangesAsync();
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

        public async Task<IEnumerable<Course>> GetCategoryCoursesAsync(Guid categoryId)
        {
            var courses = await Context.Courses
                .Where(model => model.CategoryId == categoryId)
                .ToListAsync();
                
                // _courseRepository.GetAllCoursesAsync();

            //var courses = allCourses.Where(model => model.CategoryId == categoryId).ToList();

            return courses;
        }

        public async Task<IEnumerable<Video>> GetCategoryVideosAsync(Guid categoryId)
        {
            //var allVideos = await _videoRepository.GetAllVideosAsync();

            //var videos = allVideos.Where(model => model.CategoryId == categoryId).ToList();

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
