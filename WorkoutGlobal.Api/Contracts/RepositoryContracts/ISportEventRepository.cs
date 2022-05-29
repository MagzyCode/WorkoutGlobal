using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface ISportEventRepository
    {
        public Task CreateEventAsync(SportEvent sportEvent);

        public Task UpdateEventAsync(SportEvent sportEvent);

        public Task DeleteEventAsync(SportEvent sportEvent);

        public Task<IEnumerable<SportEvent>> GetAllEventsAsync();

        public Task<SportEvent> GetEventAsync(Guid eventId); 

        public Task<IEnumerable<User>> GetEventSubscribersAsync(Guid eventId);
        // public Task<IEnumerable<SportEvent>> GetCreatorEventsAsync(Guid creatorId);
    }
}
