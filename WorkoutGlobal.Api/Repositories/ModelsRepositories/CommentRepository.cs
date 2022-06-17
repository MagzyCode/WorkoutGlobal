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

        public async Task CreateCommentAsync(Comment comment)
        {
            await CreateAsync(comment);
            await SaveChangesAsync();
        }

        public async Task<Comment> GetCommentAsync(Guid commentId)
        {
            var model = await GetModelAsync(commentId);

            return model;
        }
    }
}
