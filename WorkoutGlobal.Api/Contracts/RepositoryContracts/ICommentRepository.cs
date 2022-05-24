using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.CommentDTOs;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> GetBlockCommentsAsync(Guid commentsBlockId);

        public Task CreateCommentAsync(Comment comment);
    }
}
