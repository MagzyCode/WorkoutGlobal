using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts
{
    public interface IPostRepository
    {
        public Task CreatePostAsync(Post post);

        public Task UpdatePostAsync(Post post);

        public Task DeletePostAsync(Post post);

        public Task<Post> GetPostAsync(Guid postId);

        public Task<IEnumerable<Post>> GetAllPostsAsync();
    }
}
