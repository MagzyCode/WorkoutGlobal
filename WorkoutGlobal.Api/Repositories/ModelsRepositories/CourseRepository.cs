using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            : base(workoutGlobalContext, configurationManager)
        { }

        public async Task<Guid> CreateCourseAsync(Course course)
        {
            await CreateAsync(course);
            await SaveChangesAsync();
            return course.Id;
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

            var videos = new List<Video>();

            foreach (var videoId in videosIds)
            {
                var video = await Context.Videos.FindAsync(videoId);
                videos.Add(video);
            }

            return videos;
        }

        public async Task UpdateCourseAsync(Course course)
        {
            Update(course);
            await SaveChangesAsync();
        }
    }
}
