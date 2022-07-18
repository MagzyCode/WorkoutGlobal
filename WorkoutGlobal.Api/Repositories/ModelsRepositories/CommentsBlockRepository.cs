using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
    public class CommentsBlockRepository : BaseRepository<CommentsBlock>, ICommentsBlockRepository
    {
        public CommentsBlockRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            : base(workoutGlobalContext, configurationManager)
        { }

        public async Task<Guid> CreateCommentBlockAsync(CommentsBlock commentsBlock)
        {
            await CreateAsync(commentsBlock);
            await SaveChangesAsync();

            return commentsBlock.Id;
        }
           

        public async Task<IEnumerable<Comment>> GetCommentsBlockCommentsAsync(Guid commentBlockId)
        {
            var comments = await Context.Comments
                .Where(model => model.CommentsBlockId == commentBlockId)
                .ToListAsync();

            return comments;
        }

        public async Task<CommentsBlock> GetCommentsBlockAsync(Guid commentBlockId)
        {
            var model = await GetModelAsync(commentBlockId);

            return model;
        }
    }
}
