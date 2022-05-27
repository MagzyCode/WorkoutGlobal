using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> GetBlockCommentsAsync(Guid commentsBlockId);

        public Task CreateCommentAsync(Comment comment);

        public Task<IEnumerable<Comment>> GetCreatorCommentsAsync(Guid creatorId);
    }
}
