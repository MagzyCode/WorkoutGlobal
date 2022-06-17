using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface IStoreVideoService : IApiData
    {
        [Post("/api/storeVideos")]
        public Task CreateStoreVideoAsync([Body] StoreVideo storeVideo);

        [Put("/api/storeVideos/{storeVideoId}")]
        public Task UpdateStoreVideoAsync(Guid storeVideoId, [Body] StoreVideo storeVideo);

        [Delete("/api/storeVideos/{storeVideoId}")]
        public Task DeleteStoreVideoAsync(Guid storeVideoId);

        [Get("/api/storeVideos")]
        public Task<IEnumerable<StoreVideo>> GetAllStoreVideosAsync();

        [Get("/api/storeVideos/{storeVideoId}")]
        public Task<StoreVideo> GetStoreVideoAsync(Guid storeVideoId);
    }
}
