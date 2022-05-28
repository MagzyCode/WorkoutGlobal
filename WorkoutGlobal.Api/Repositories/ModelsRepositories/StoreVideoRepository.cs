using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class StoreVideoRepository : BaseRepository<StoreVideo>, IStoreVideoRepository
    {
        // private readonly IVideoRepository _videoRepository;
 
        public StoreVideoRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            // IVideoRepository videoRepository) 
            : base(workoutGlobalContext, configurationManager)
        {
            // _videoRepository = videoRepository;
        }

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

        //public async Task<IEnumerable<Video>> GetUserVideosAsync(Guid userId)
        //{
        //    var videoIds = await GetAll()
        //        .Where(x => x.UserId == userId)
        //        .Select(x => x.SavedVideoId)
        //        .ToListAsync();

        //    var videos = new List<Video>();

        //    foreach (var videoId in videoIds)
        //        videos.Add(await _videoRepository.GetVideoAsync(videoId));

        //    return videos;
        //}

        public async Task UpdateStoreVideoAsync(StoreVideo storeVideo)
        {
            Update(storeVideo);
            await SaveChangesAsync();
        }
    }
}
