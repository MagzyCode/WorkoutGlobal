using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface ICourseService : IApiData
    {
        [Get("/api/courses")]
        public Task<IEnumerable<Course>> GetAllCoursesAsync();

        [Get("/api/courses/{courseId}")]
        public Task<Course> GetCourseAsync(Guid courseId);

        [Get("/api/courses/{courseId}/videos")]
        public Task<IEnumerable<Video>> GetCourseVideosAsync(Guid courseId);

        [Post("/api/courses")]
        public Task<Guid> CreateCourseAsync(Course course);

        [Put("/api/courses/{courseId}")]
        public Task UpdateCourseAsync(Guid courseId, [Body] Course course);

        [Delete("/api/courses/{courseId}")]
        public Task DeleteCourseAsync(Guid courseId, [Body] Course course);

        [Get("/api/courses/{courseId}/users")]
        public Task<IEnumerable<User>> GetCourseSubscribersAsync(Guid courseId);
    }
}
