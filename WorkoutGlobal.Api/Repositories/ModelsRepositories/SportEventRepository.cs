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

        public async Task UpdateEventAsync(SportEvent sportEvent)
        {
            Update(sportEvent);
            await SaveChangesAsync();
        }
    }
}
