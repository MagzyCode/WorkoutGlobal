using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts
{
    public interface ICommentsBlockRepository
    {
        public Task<CommentsBlock> GetCommentsBlockAsync(Guid commentBlockId);
        public Task<IEnumerable<Comment>> GetCommentsBlockCommentsAsync(Guid commentBlockId);
        public Task CreateCommentBlockAsync(CommentsBlock commentsBlock);
    }
}
