using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class StoreVideoService : BaseService<IStoreVideoService>, IStoreVideoService
    {
        public StoreVideoService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreateStoreVideoAsync(StoreVideo storeVideo)
            => await Service.CreateStoreVideoAsync(storeVideo);

        public async Task DeleteStoreVideoAsync(Guid storeVideoId)
            => await Service.DeleteStoreVideoAsync(storeVideoId);

        public async Task<IEnumerable<StoreVideo>> GetAllStoreVideosAsync()
            => await Service.GetAllStoreVideosAsync();

        public async Task<StoreVideo> GetStoreVideoAsync(Guid storeVideoId)
            => await Service.GetStoreVideoAsync(storeVideoId);

        public async Task UpdateStoreVideoAsync(Guid storeVideoId, StoreVideo storeVideo)
            => await Service.UpdateStoreVideoAsync(storeVideoId, storeVideo);
    }
}
