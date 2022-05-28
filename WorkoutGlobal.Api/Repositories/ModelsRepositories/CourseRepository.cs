using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        //private readonly ICourseVideoRepository _courseVideosRepository;
        //private readonly IVideoRepository _videoRepository;
        //private readonly ISubscribeCourseRepository _subscribeCourseRepository;

        public CourseRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            //ICourseVideoRepository courseVideosRepository,
            //IVideoRepository videoRepository,
            //ISubscribeCourseRepository subscribeCourseRepository) 
            : base(workoutGlobalContext, configurationManager)
        {
            //_courseVideosRepository = courseVideosRepository;
            //_videoRepository = videoRepository;
            //_subscribeCourseRepository = subscribeCourseRepository;
        }

        public async Task CreateCourseAsync(Course course)
        {
            await CreateAsync(course);
            await SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(Course course)
        {
            Delete(course);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var courses = await GetAll().ToListAsync();

            return courses;
        }

        public async Task<Course> GetCourseAsync(Guid courseId)
        {
            var model = await GetModelAsync(courseId);

            return model;
        }

        public async Task<IEnumerable<User>> GetCourseSubscribersAsync(Guid courseId)
        {
            var subscribersIds = await Context.SubscribeCourses
                .Where(model => model.SubscribeCourseId == courseId)
                .Select(model => model.SubscriberId)
                .ToListAsync();

            var users = new List<User>();

            foreach (var subscriberId in subscribersIds)
            {
                var user = await Context.UserAccounts.FindAsync(subscriberId);
                users.Add(user);
            }

            return users;
        }

        public async Task<IEnumerable<Video>> GetCourseVideosAsync(Guid courseId)
        {
            var videosIds = await Context.CourseVideos
                .Where(model => model.CourseId == courseId)
                .Select(model => model.VideoId)
                .ToListAsync();
                
                // await _courseVideosRepository.GetCourseVideosByCourseIdAsync(courseId);

            var videos = new List<Video>();

            foreach (var videoId in videosIds)
            {
                var video = await Context.Videos.FindAsync(videoId);
                videos.Add(video);
            }

            return videos;
        }

        //public async Task<IEnumerable<Course>> GetCreatorCoursesAsync(Guid creatorId)
        //{
        //    var courses = await GetAll().Where(x => x.CreatorId == creatorId).ToListAsync();

        //    return courses;
        //}

        public async Task UpdateCourseAsync(Course course)
        {
            Update(course);
            await SaveChangesAsync();
        }
    }
}
