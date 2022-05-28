using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class SubscribeEventRepository : BaseRepository<SubscribeEvent>, ISubscribeEventRepository
    {
        // private readonly ISportEventRepository _sportEventRepository;

        public SubscribeEventRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            // ISportEventRepository sportEventRepository) 
            : base(workoutGlobalContext, configurationManager)
        {
            // _sportEventRepository = sportEventRepository;
        }

        public async Task CreateSubscribeEventAsync(SubscribeEvent subscribeEvent)
        {
            await CreateAsync(subscribeEvent);
            await SaveChangesAsync();
        }

        public async Task DeleteSubscribeEventAsync(SubscribeEvent subscribeEvent)
        {
            Delete(subscribeEvent);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<SubscribeEvent>> GetAllSubscribeEventsAsync()
        {
            var models = await GetAll().ToListAsync();

            return models;
        }

        public async Task<SubscribeEvent> GetSubscribeEventAsync(Guid subscribeEventId)
        {
            var model = await GetModelAsync(subscribeEventId);

            return model;
        }

        //public async Task<IEnumerable<SportEvent>> GetUserSubscribeEventsAsync(Guid userId)
        //{
        //    var sportEventsIds = await GetAll()
        //        .Where(x => x.UserId == userId)
        //        .Select(x => x.EventId)
        //        .ToListAsync();

        //    var sportEvents = new List<SportEvent>();

        //    foreach (var sportEventId in sportEventsIds)
        //        sportEvents.Add(await _sportEventRepository.GetEventAsync(sportEventId));

        //    return sportEvents;      
        //}

        public async Task UpdateSubscribeEventAsync(SubscribeEvent subscribeEvent)
        {
            Update(subscribeEvent);
            await SaveChangesAsync();
        }
    }
}
