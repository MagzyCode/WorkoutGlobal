using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts
{
    public interface ICourseVideoRepository
    {
        public Task<IEnumerable<CourseVideo>> GetAllCourseVideosAsync();
        public Task<IEnumerable<CourseVideo>> GetCourseVideosByCourseIdAsync(Guid courseId);
        public Task<IEnumerable<CourseVideo>> GetOrderedCourseVideosAsync(Guid courseId);
        public Task<CourseVideo> GetCourseVideoAsync(Guid courseVideoId);
        public Task<Guid> CreateCourseVideoAsync(CourseVideo courseVideo);
    }
}
