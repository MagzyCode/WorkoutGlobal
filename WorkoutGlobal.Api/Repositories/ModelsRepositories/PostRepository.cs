using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager) 
            : base(workoutGlobalContext, configurationManager)
        {
        }

        public async Task CreatePostAsync(Post post)
        {
            await CreateAsync(post);
            await SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
            Delete(post);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var models = await Context.Posts.ToListAsync();

            return models;
        }

        public async Task<Post> GetPostAsync(Guid postId)
        {
           var model = await GetModelAsync(postId);

            return model;
        }

        public async Task UpdatePostAsync(Post post)
        {
            Update(post);
            await SaveChangesAsync();
        }
    }
}
