using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            : base(workoutGlobalContext, configurationManager)
        { }

        public async Task<Guid> CreateCommentAsync(Comment comment)
        {
            await CreateAsync(comment);
            await SaveChangesAsync();

            return comment.Id;
        }

        public async Task<Comment> GetCommentAsync(Guid commentId)
        {
            var model = await GetModelAsync(commentId);

            return model;
        }
    }
}
