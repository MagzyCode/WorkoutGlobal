using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts
{
    public interface ICommentRepository
    {
        public Task<Guid> CreateCommentAsync(Comment comment);

        public Task<Comment> GetCommentAsync(Guid commentId);
    }
}
