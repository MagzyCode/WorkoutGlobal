using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface ICommentsBlockService : IApiData
    {
        [Get("/api/commentsBlocks/{commentsBlockId}/comments")]
        public Task<IEnumerable<Comment>> GetBlockCommentsAsync(Guid commentsBlockId);

        [Get("/api/commentsBlocks/{commentsBlockId}")]
        public Task<CommentsBlock> GetCommentsBlockAsync(Guid commentsBlockId);

        [Post("/api/commentsBlocks")]
        public Task CreateCommentsBlockAsync([Body] CommentsBlock commentsBlock);
    }
}
