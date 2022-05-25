using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class CommentService : BaseService<ICommentService>, ICommentService
    {
        public CommentService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreateCommentAsync(Comment comment)
            => await Service.CreateCommentAsync(comment);
    }
}
