using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface IStoreVideoRepository
    {
        public Task CreateStoreVideoAsync(StoreVideo storeVideo);

        public Task UpdateStoreVideoAsync(StoreVideo storeVideo);

        public Task DeleteStoreVideoAsync(StoreVideo storeVideo);

        public Task<StoreVideo> GetStoreVideoAsync(Guid storeVideoId);

        public Task<IEnumerable<StoreVideo>> GetAllStoreVideosAsync();

        // public Task<IEnumerable<Video>> GetUserVideosAsync(Guid userId);
    }
}
