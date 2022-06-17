using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface ISubscribeCourseService : IApiData
    {
        [Post("/api/subscribeCourses")]
        public Task CreateSubscribeCourseAsync([Body] SubscribeCourse subscribeCourse);

        [Put("/api/subscribeCourses/{subscribeCourseId}")]
        public Task UpdateSubscribeCourseAsync(Guid subscribeCourseId, [Body] SubscribeCourse subscribeCourse);

        [Delete("/api/subscribeCourses/{subscribeCourseId}")]
        public Task DeleteSubscribeCourseAsync(Guid subscribeCourseId);

        [Get("/api/subscribeCourses")]
        public Task<IEnumerable<SubscribeCourse>> GetAllSubscribeCoursesAsync();
        
        [Get("/api/subscribeCourses/{subscribeCourseId}")]
        public Task<SubscribeCourse> GetSubscribeCourseAsync(Guid subscribeCourseId);
    }
}
