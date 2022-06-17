using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class SubscribeCourseService : BaseService<ISubscribeCourseService>, ISubscribeCourseService
    {
        public SubscribeCourseService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreateSubscribeCourseAsync(SubscribeCourse subscribeCourse)
            => await Service.CreateSubscribeCourseAsync(subscribeCourse); 

        public async Task DeleteSubscribeCourseAsync(Guid subscribeCourseId)
            => await Service.DeleteSubscribeCourseAsync(subscribeCourseId);

        public async Task<IEnumerable<SubscribeCourse>> GetAllSubscribeCoursesAsync()
            => await Service.GetAllSubscribeCoursesAsync();

        
        public async Task<SubscribeCourse> GetSubscribeCourseAsync(Guid subscribeCourseId)
            => await Service.GetSubscribeCourseAsync(subscribeCourseId);

        public async Task UpdateSubscribeCourseAsync(Guid subscribeCourseId, SubscribeCourse subscribeCourse)
            => await Service.UpdateSubscribeCourseAsync(subscribeCourseId, subscribeCourse);
    }
}
