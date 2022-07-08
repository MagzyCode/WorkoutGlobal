using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.RequestParameters;

namespace WorkoutGlobal.Api.Contracts
{
    /// <summary>
    /// Base structure for video repository.
    /// </summary>
    public interface IVideoRepository
    {
        public int Count { get; } 

        public Task<IEnumerable<Video>> GetAllVideosAsync(bool isVideoPublic = true);

        public Task<Video> GetVideoAsync(Guid id);

        public Task<IEnumerable<Video>> GetPageVideosAsync(VideoParameters parameters, bool isPublic);

        public Task<Guid> CreateVideoAsync(Video video);

        public Task UpdateVideoAsync(Video video);

        public Task DeleteVideoAsync(Video video);

        public Task<CommentsBlock> GetVideoCommentsBlockAsync(Guid videoId);

        public Task Purge(Video video);
    }
}
