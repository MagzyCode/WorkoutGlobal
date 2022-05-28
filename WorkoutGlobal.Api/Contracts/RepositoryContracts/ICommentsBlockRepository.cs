using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.CommentsBlockDTOs;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface ICommentsBlockRepository
    {
        public Task<CommentsBlock> GetCommentsBlockAsync(Guid commentBlockId);
        // public Task<CommentsBlock> GetCommentsBlockByVideoIdAsync(Guid videoId);
        public Task<IEnumerable<Comment>> GetCommentsBlockCommentsAsync(Guid commentBlockId);
        public Task CreateCommentBlockAsync(CommentsBlock commentsBlock);
    }
}
