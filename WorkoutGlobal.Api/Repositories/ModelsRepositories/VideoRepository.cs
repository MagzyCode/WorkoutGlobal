using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.RequestParameters;

namespace WorkoutGlobal.Api.Repositories
{
    /// <summary>
    /// Base video content repository.
    /// </summary>
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        /// <summary>
        /// Ctor for video repository.
        /// </summary>
        /// <param name="workoutGlobalContext"></param>
        /// <param name="configurationManager"></param>
        public VideoRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager) 
            : base(workoutGlobalContext, configurationManager)
        { }

        public int Count => Context.Videos.Count();

        public async Task<IEnumerable<Video>> GetAllVideosAsync(bool isVideoPublic = true)
        {
            var videos = await GetAll().Where(video => video.IsPublic == isVideoPublic).ToListAsync();

            return videos;
        }

        public async Task<Video> GetVideoAsync(Guid id)
        {
            var video = await Context.Videos.FirstOrDefaultAsync(video => video.Id == id);

            return video;
        }

        public async Task<IEnumerable<Video>> GetPageVideosAsync(VideoParameters parameters, bool isPublic = true)
        {
            var pageVideos = await GetAll()
                    .Where(video => video.IsPublic == isPublic)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();

            return pageVideos;
        }

        public async Task<Guid> CreateVideoAsync(Video video)
        {
            await CreateAsync(video);

            await Context.CommentsBlocks.AddAsync(new CommentsBlock() { CommentedVideoId = video.Id }); 

            await SaveChangesAsync();

            return video.Id;
        }

        public async Task UpdateVideoAsync(Video video)
        {
            Update(video);
            await SaveChangesAsync();
        }

        public async Task DeleteVideoAsync(Video video)
        {
            Delete(video);
            await SaveChangesAsync();
        }

        public async Task<CommentsBlock> GetVideoCommentsBlockAsync(Guid videoId)
        {
            var commentBlock = await Context.CommentsBlocks
                .Where(model => model.CommentedVideoId == videoId)
                .FirstOrDefaultAsync();

            return commentBlock;
        }
    }
}
