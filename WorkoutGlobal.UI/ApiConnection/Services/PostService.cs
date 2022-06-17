using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class PostService : BaseService<IPostService>, IPostService
    {
        public PostService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreatePostAsync(Post post)
            => await Service.CreatePostAsync(post);

        public async Task DeletePostAsync(Guid postId)
            => await Service.DeletePostAsync(postId);

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
            => await Service.GetAllPostsAsync();

        public async Task<Post> GetPostAsync(Guid postId)
            => await Service.GetPostAsync(postId);

        public async Task UpdatePostAsync(Guid postId, Post post)
            => await UpdatePostAsync(postId, post);
    }
}
