using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class CourseVideosRepository : BaseRepository<CourseVideos>, ICourseVideosRepository
    {
        public CourseVideosRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager) 
            : base(workoutGlobalContext, configurationManager)
        {
        }

        public async Task<IEnumerable<CourseVideos>> GetAllCourseVideosAsync()
        {
            var courseVideos = await GetAll().ToListAsync();

            return courseVideos;
        }

        public async Task<IEnumerable<CourseVideos>> GetCourseVideosByCourseIdAsync(Guid courseId)
        {
            var courseVideos = await GetAll().Where(x => x.CourseId == courseId).ToListAsync();

            return courseVideos;
        }
    }
}
