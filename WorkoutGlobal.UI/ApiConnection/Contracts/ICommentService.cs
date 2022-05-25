using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface ICommentService : IApiData 
    {
        [Post("/api/comments")]
        public Task CreateCommentAsync(Comment comment);
    }
}
