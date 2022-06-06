using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface IPostService : IApiData
    {
        [Post("/api/posts")]
        public Task CreatePostAsync(Post post);

        [Put("/api/posts/{postId}")]
        public Task UpdatePostAsync(Guid postId, Post post);

        [Delete("/api/posts/{postId}")]
        public Task DeletePostAsync(Guid postId);

        [Get("/api/posts/{postId}")]
        public Task<Post> GetPostAsync(Guid postId);

        [Get("/api/posts")]
        public Task<IEnumerable<Post>> GetAllPostsAsync();
    }
}
