using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
    public class StoreVideoRepository : BaseRepository<StoreVideo>, IStoreVideoRepository
    {
        public StoreVideoRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            : base(workoutGlobalContext, configurationManager)
        { }

        public async Task CreateStoreVideoAsync(StoreVideo storeVideo)
        {
            await CreateAsync(storeVideo);
            await SaveChangesAsync();
        }

        public async Task DeleteStoreVideoAsync(StoreVideo storeVideo)
        {
            Delete(storeVideo);
            await SaveChangesAsync(); 
        }

        public async Task<IEnumerable<StoreVideo>> GetAllStoreVideosAsync()
        {
            var models = await GetAll().ToListAsync();

            return models;
        }

        public async Task<StoreVideo> GetStoreVideoAsync(Guid storeVideoId)
        {
            var model = await GetModelAsync(storeVideoId);

            return model;
        }

        public async Task<bool> IsStoreVideoExists(Guid userId, Guid videoId)
        {
            var isExists = await Context.StoreVideos.AnyAsync(model => model.SavedVideoId == videoId &&  model.UserId == userId);

            return isExists;
        }

        public async Task UpdateStoreVideoAsync(StoreVideo storeVideo)
        {
            Update(storeVideo);
            await SaveChangesAsync();
        }
    }
}
