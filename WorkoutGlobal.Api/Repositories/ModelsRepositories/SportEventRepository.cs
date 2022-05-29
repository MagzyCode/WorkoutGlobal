using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class SportEventRepository : BaseRepository<SportEvent>, ISportEventRepository
    {
        public SportEventRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager) 
            : base(workoutGlobalContext, configurationManager)
        {
        }

        public async Task CreateEventAsync(SportEvent sportEvent)
        {
            await CreateAsync(sportEvent);
            await SaveChangesAsync();
        }

        public async Task DeleteEventAsync(SportEvent sportEvent)
        {
            Delete(sportEvent);
            await SaveChangesAsync(); 
        }

        public async Task<IEnumerable<SportEvent>> GetAllEventsAsync()
        {
            var events = await GetAll().ToListAsync();

            return events;
        }

        //public async Task<IEnumerable<SportEvent>> GetCreatorEventsAsync(Guid creatorId)
        //{
        //    var events = await GetAll().Where(x => x.TrainerId == creatorId).ToListAsync();

        //    return events;
        //}

        public async Task<SportEvent> GetEventAsync(Guid eventId)
        {
            var sportEvent = await GetAll().Where(x => x.Id == eventId).FirstOrDefaultAsync();

            return sportEvent;
        }

        public async Task<IEnumerable<User>> GetEventSubscribersAsync(Guid eventId)
        {
            var usersIds = await Context.SubscribeEvents
                .Where(model => model.UserId == eventId)
                .Select(model => model.UserId)
                .ToListAsync();

            var users = new List<User>();

            foreach (var userId in usersIds)
            {
                var user = await Context.UserAccounts.FindAsync(userId);
                users.Add(user);
            }

            return users;
        }

        public async Task UpdateEventAsync(SportEvent sportEvent)
        {
            Update(sportEvent);
            await SaveChangesAsync();
        }
    }
}
