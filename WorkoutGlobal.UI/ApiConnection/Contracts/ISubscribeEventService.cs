using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface ISubscribeEventService : IApiData
    {
        [Post("/api/subscribeEvents")]
        public Task CreateSubscribeEventAsync([Body] SubscribeEvent subscribeEvent);

        [Put("/api/subscribeEvents/{subscribeEventId}")]
        public Task UpdateSubscribeEventAsync(Guid subscribeEventId, [Body] SubscribeEvent subscribeEvent);

        [Delete("/api/subscribeEvents/{subscribeEventId}")]
        public Task DeleteSubscribeEventAsync(Guid subscribeEventId);

        [Get("/api/subscribeEvents")]
        public Task<IEnumerable<SubscribeEvent>> GetAllSubscribeEventsAsync();

        [Get("/api/subscribeEvents/{subscribeEventId}")]
        public Task<SubscribeEvent> GetSubscribeEventAsync(Guid subscribeEventId);
    }
}
