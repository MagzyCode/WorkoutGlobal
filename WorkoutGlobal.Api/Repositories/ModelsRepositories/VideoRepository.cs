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

        public async Task<IEnumerable<Video>> GetAllVideosAsync(bool isPublic = true)
        {
            var videos = await GetAll().Where(video => video.IsPublic == isPublic).ToListAsync();

            return videos;
        }

        public async Task<Video> GetVideoAsync(Guid id)
        {
            var video = await Context.Videos.FirstOrDefaultAsync(video => video.Id == id);

            return video;
        }

        public async Task<IEnumerable<Video>> GetPageVideosAsync(VideoParameters parameters, bool isPublic = true)
        {
            var pageVideos = isPublic
                ? await GetAll()
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync()
                : await GetAll()
                    .Where(video => video.IsPublic == false)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();

            return pageVideos;
        }

        public async Task CreateVideoAsync(Video video)
        {
            await CreateAsync(video);
            await SaveChangesAsync();
        }
    }
}
