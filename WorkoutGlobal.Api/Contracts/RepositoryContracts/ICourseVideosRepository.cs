using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface ICourseVideosRepository
    {
        public Task<IEnumerable<CourseVideos>> GetAllCourseVideosAsync();
        public Task<IEnumerable<CourseVideos>> GetCourseVideosByCourseIdAsync(Guid courseId);
    }
}
