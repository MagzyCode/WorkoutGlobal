using Refit;
using WorkoutGlobal.UI.Models;
using WorkoutGlobal.UI.RequestParameters;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface IVideoService : IApiData
    {
        [Get("/api/videos")]
        public Task<IEnumerable<Video>> GetAllVideosAsync([Query] VideoParameters videoParameters = null);

        [Get("/api/videos/{videoId}")]
        public Task<Video> GetVideoAsync(Guid videoId);

        [Post("/api/videos")]
        public Task AddVideoAsync(Video video);
    }
}
