using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class CourseVideoRepository : BaseRepository<CourseVideo>, ICourseVideoRepository
    {
        public CourseVideoRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager) 
            : base(workoutGlobalContext, configurationManager)
        {
        }

        public async Task<IEnumerable<CourseVideo>> GetAllCourseVideosAsync()
        {
            var courseVideos = await GetAll().ToListAsync();

            return courseVideos;
        }

        public Task<CourseVideo> GetCourseVideoAsync(Guid courseVideoId)
        {
            var model = GetModelAsync(courseVideoId);

            return model;
        }

        public async Task<IEnumerable<CourseVideo>> GetCourseVideosByCourseIdAsync(Guid courseId)
        {
            var courseVideos = await GetAll().Where(x => x.CourseId == courseId).ToListAsync();

            return courseVideos;
        }

        public async Task<IEnumerable<CourseVideo>> GetOrderedCourseVideosAsync(Guid courseId)
        {
            var orderedCourseVideos = await GetAll()
                .OrderBy(courseVideo => courseVideo.SequenceNumber)
                .ToListAsync();

            return orderedCourseVideos;
        }
    }
}
