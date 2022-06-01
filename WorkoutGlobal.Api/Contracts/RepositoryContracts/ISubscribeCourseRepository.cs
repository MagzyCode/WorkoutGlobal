using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts
{
    public interface ISubscribeCourseRepository
    {
        public Task CreateSubscribeCourseAsync(SubscribeCourse subscribeCourse);

        public Task UpdateSubscribeCourseAsync(SubscribeCourse subscribeCourse);
        
        public Task DeleteSubscribeCourseAsync(SubscribeCourse subscribeCourseId);

        public Task<SubscribeCourse> GetSubscribeCourseAsync(Guid subscribeCourseId);

        public Task<IEnumerable<SubscribeCourse>> GetAllSubscribeCourseAsync();

        public Task<bool> IsCourseSubscriptionExists(Guid userId, Guid courseId);
    }
}
