using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface ICommentsBlockService : IApiData
    {
        [Get("/api/videoBlocks/{commentsBlockId}/comments")]
        public Task<IEnumerable<Comment>> GetBlockCommentsAsync(Guid commentsBlockId);

        [Get("/api/videoBlocks/{commentsBlockId}")]
        public Task<CommentsBlock> GetCommentsBlockAsync(Guid commentsBlockId);

        [Get("/api/videoBlocks/videos/{videoId}")]
        public Task<CommentsBlock> GetCommentsBlockByVideoIdAsync(Guid videoId);
    }
}
