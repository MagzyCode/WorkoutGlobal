using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class CommentsBlockService : BaseService<ICommentsBlockService>, ICommentsBlockService
    {
        public CommentsBlockService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreateCommentsBlockAsync(CommentsBlock commentsBlock)
            => await Service.CreateCommentsBlockAsync(commentsBlock);

        public async Task<IEnumerable<Comment>> GetBlockCommentsAsync(Guid commentsBlockId)
            => await Service.GetBlockCommentsAsync(commentsBlockId);

        public async Task<CommentsBlock> GetCommentsBlockAsync(Guid commentsBlockId)
            => await Service.GetCommentsBlockAsync(commentsBlockId);
    }
}
