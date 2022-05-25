using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private readonly ICourseVideosRepository _courseVideosRepository;
        private readonly IVideoRepository _videoRepository;

        public CourseRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager,
            ICourseVideosRepository courseVideosRepository,
            IVideoRepository videoRepository) 
            : base(workoutGlobalContext, configurationManager)
        {
            _courseVideosRepository = courseVideosRepository;
            _videoRepository = videoRepository;
        }

        public async Task CreateCourseAsync(Course course)
        {
            await CreateAsync(course);
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

        public async Task<IEnumerable<Video>> GetCourseVideosAsync(Guid courseId)
        {
            var courseVideos = await _courseVideosRepository.GetCourseVideosByCourseIdAsync(courseId);

            var videos = new List<Video>();

            foreach (var courseVideo in courseVideos)
                videos.Add(await _videoRepository.GetVideoAsync(courseVideo.VideoId));

            return videos;
        }
    }
}
