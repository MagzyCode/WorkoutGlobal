using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class SportEventService : BaseService<ISportEventService>, ISportEventService
    {
        public SportEventService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreateSportEventAsync(SportEvent sportEvent)
            => await Service.CreateSportEventAsync(sportEvent);

        public async Task DeleteSportEventAsync(Guid sportEventId)
            => await Service.DeleteSportEventAsync(sportEventId);

        public async Task<IEnumerable<SportEvent>> GetAllSportEventsAsync()
            => await Service.GetAllSportEventsAsync();

        public async Task<SportEvent> GetSportEventAsync(Guid sportEventId)
            => await Service.GetSportEventAsync(sportEventId);

        public async Task<IEnumerable<User>> GetSportEventSubscribersAsync(Guid sportEventId)
            => await Service.GetSportEventSubscribersAsync(sportEventId);

        public async Task UpdateSportEventAsync(Guid sportEventId, SportEvent sportEvent)
            => await Service.UpdateSportEventAsync(sportEventId, sportEvent);
    }
}
