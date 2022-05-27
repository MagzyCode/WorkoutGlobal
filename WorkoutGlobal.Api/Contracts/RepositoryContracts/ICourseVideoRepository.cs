using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface ICourseVideoRepository
    {
        public Task<IEnumerable<CourseVideo>> GetAllCourseVideosAsync();
        public Task<IEnumerable<CourseVideo>> GetCourseVideosByCourseIdAsync(Guid courseId);
        public Task<IEnumerable<CourseVideo>> GetOrderedCourseVideosAsync(Guid courseId);

    }
}
