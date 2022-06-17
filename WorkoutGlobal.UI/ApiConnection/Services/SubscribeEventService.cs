using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class SubscribeEventService : BaseService<ISubscribeEventService>, ISubscribeEventService
    {
        public SubscribeEventService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreateSubscribeEventAsync(SubscribeEvent subscribeEvent)
            => await Service.CreateSubscribeEventAsync(subscribeEvent);

        public async Task DeleteSubscribeEventAsync(Guid subscribeEventId)
            => await Service.DeleteSubscribeEventAsync(subscribeEventId);

        public async Task<IEnumerable<SubscribeEvent>> GetAllSubscribeEventsAsync()
            => await Service.GetAllSubscribeEventsAsync();

        public Task<SubscribeEvent> GetSubscribeEventAsync(Guid subscribeEventId)
            => Service.GetSubscribeEventAsync(subscribeEventId);

        public async Task UpdateSubscribeEventAsync(Guid subscribeEventId, SubscribeEvent subscribeEvent)
            => await Service.UpdateSubscribeEventAsync(subscribeEventId, subscribeEvent);
    }
}
