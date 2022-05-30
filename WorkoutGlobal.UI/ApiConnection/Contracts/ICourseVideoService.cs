using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface ICourseVideoService : IApiData
    {
        [Post("/api/courseVideos")]
        public Task<Guid> CreateCourseVideoAsync(CourseVideo courseVideo);

        [Get("/api/courseVideos")]
        public Task<IEnumerable<CourseVideo>> GetAllCourseVideoAsync();
    }
}
