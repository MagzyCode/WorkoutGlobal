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

        public async Task CreateVideoAsync(Video video)
            => await Service.CreateVideoAsync(video);

        public async Task DeleteVideoAsync(Guid videoId)
            => await Service.DeleteVideoAsync(videoId);

        public async Task<IEnumerable<Video>> GetAllVideosAsync(VideoParameters videoParameters = null)
            => await Service.GetAllVideosAsync(videoParameters);

        public async Task<Video> GetVideoAsync(Guid videoId)
            => await Service.GetVideoAsync(videoId);

        public async Task<CommentsBlock> GetVideoCommentsBlockAsync(Guid videoId)
            => await Service.GetVideoCommentsBlockAsync(videoId);

        public async Task UpdateVideoAsync(Guid videoId, Video video)
            => await Service.UpdateVideoAsync(videoId, video);
    }
}
