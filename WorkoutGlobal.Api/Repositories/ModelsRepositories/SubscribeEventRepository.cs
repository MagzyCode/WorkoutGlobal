using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
    public class SubscribeEventRepository : BaseRepository<SubscribeEvent>, ISubscribeEventRepository
    {
        public SubscribeEventRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            : base(workoutGlobalContext, configurationManager)
        { }

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

        public async Task<bool> IsSportEventSubscriptionExists(Guid userId, Guid eventId)
        {
            var isExisted = await Context.SubscribeEvents.AnyAsync(model => model.UserId == userId && model.EventId == eventId);

            return isExisted;
        }

        public async Task UpdateSubscribeEventAsync(SubscribeEvent subscribeEvent)
        {
            Update(subscribeEvent);
            await SaveChangesAsync();
        }
    }
}
