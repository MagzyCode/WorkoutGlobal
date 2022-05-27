using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private readonly ICourseVideoRepository _courseVideosRepository;
        private readonly IVideoRepository _videoRepository;

        public CourseRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager,
            ICourseVideoRepository courseVideosRepository,
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

        public Task<IEnumerable<User>> GetCourseSubscribersAsync(Guid courseId)
        {
            // TODO: Создать когда будет репозиторий для SubscribeCourse
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Video>> GetCourseVideosAsync(Guid courseId)
        {
            var courseVideos = await _courseVideosRepository.GetCourseVideosByCourseIdAsync(courseId);

            var videos = new List<Video>();

            foreach (var courseVideo in courseVideos)
                videos.Add(await _videoRepository.GetVideoAsync(courseVideo.VideoId));

            return videos;
        }

        public async Task<IEnumerable<Course>> GetCreatorCoursesAsync(Guid creatorId)
        {
            var courses = await GetAll().Where(x => x.CreatorId == creatorId).ToListAsync();

            return courses;
        }

        public async Task UpdateCourseAsync(Course course)
        {
            Update(course);
            await SaveChangesAsync();
        }
    }
}
