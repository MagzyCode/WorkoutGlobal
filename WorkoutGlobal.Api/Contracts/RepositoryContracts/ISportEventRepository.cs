using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts
{
    public interface ISportEventRepository
    {
        public Task<Guid> CreateEventAsync(SportEvent sportEvent);

        public Task UpdateEventAsync(SportEvent sportEvent);

        public Task DeleteEventAsync(SportEvent sportEvent);

        public Task<IEnumerable<SportEvent>> GetAllEventsAsync();

        public Task<SportEvent> GetEventAsync(Guid eventId); 

        public Task<IEnumerable<User>> GetEventSubscribersAsync(Guid eventId);
    }
}
