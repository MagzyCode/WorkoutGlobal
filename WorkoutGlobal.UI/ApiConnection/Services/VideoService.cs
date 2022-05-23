using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;
using WorkoutGlobal.UI.RequestParameters;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class VideoService : BaseService<IVideoService>, IVideoService
    {
        public VideoService(IConfiguration configuration) : base(configuration)
        { }

        public async Task AddVideoAsync(Video video)
            => await Service.AddVideoAsync(video);

        public async Task<IEnumerable<Video>> GetAllVideosAsync(VideoParameters videoParameters = null)
            => await Service.GetAllVideosAsync(videoParameters);

        public async Task<Video> GetVideoAsync(Guid videoId)
            => await Service.GetVideoAsync(videoId);
    }
}
