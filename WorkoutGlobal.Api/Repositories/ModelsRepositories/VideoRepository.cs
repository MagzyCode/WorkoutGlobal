using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;
using WorkoutGlobal.Api.RequestParameters;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    /// <summary>
    /// Base video content repository.
    /// </summary>
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        private readonly ICommentsBlockRepository _commentsBlockRepository;

        /// <summary>
        /// Ctor for video repository.
        /// </summary>
        /// <param name="workoutGlobalContext"></param>
        /// <param name="configurationManager"></param>
        public VideoRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager,
            ICommentsBlockRepository commentsBlockRepository) 
            : base(workoutGlobalContext, configurationManager)
        { 
            _commentsBlockRepository = commentsBlockRepository;
        }

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

        public async Task CreateVideoAsync(Video video)
        {
            await CreateAsync(video);

            await _commentsBlockRepository.CreateCommentBlockAsync(new CommentsBlock() { CommentedVideoId = video.Id });

            await SaveChangesAsync();
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

        //public async Task<IEnumerable<Video>> GetCreatorVideosAsync(Guid creatorId)
        //{
        //    var videos = await GetAll().Where(model => model.UserId == creatorId).ToListAsync();

        //    return videos;
        //}
    }
}
