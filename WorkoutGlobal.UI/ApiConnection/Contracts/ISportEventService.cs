using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface ISportEventService : IApiData
    {
        [Post("/api/sportEvents")]
        public Task CreateSportEventAsync([Body] SportEvent sportEvent);

        [Put("/api/sportEvents/{sportEventId}")]
        public Task UpdateSportEventAsync(Guid sportEventId, [Body] SportEvent sportEvent);

        [Delete("/api/sportEvents/{sportEventId}")]
        public Task DeleteSportEventAsync(Guid sportEventId);

        [Get("/api/sportEvents")]
        public Task<IEnumerable<SportEvent>> GetAllSportEventsAsync();

        [Get("/api/sportEvents/{sportEventId}")]
        public Task<SportEvent> GetSportEventAsync(Guid sportEventId);

        [Get("/api/sportEvents/{sportEventId}/subscribers")]
        public Task<IEnumerable<User>> GetSportEventSubscribersAsync(Guid sportEventId);



    }
}
