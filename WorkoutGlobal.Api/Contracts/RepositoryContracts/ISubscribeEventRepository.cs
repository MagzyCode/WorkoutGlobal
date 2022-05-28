using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface ISubscribeEventRepository
    {
        public Task CreateSubscribeEventAsync(SubscribeEvent subscribeEvent);

        public Task UpdateSubscribeEventAsync(SubscribeEvent subscribeEvent);

        public Task DeleteSubscribeEventAsync(SubscribeEvent subscribeEvent);

        public Task<SubscribeEvent> GetSubscribeEventAsync(Guid subscribeEventId);

        public Task<IEnumerable<SubscribeEvent>> GetAllSubscribeEventsAsync();

        // public Task<IEnumerable<SportEvent>> GetUserSubscribeEventsAsync(Guid userId);
    }
}
