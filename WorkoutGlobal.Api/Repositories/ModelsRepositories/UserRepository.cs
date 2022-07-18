using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository
            (WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            : base(workoutGlobalContext, configurationManager)
        { }

        public async Task<Guid> CreateUserAsync(User user)
        {
            await CreateAsync(user);
            await SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteUserAsync(User user)
        {
            Delete(user);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await GetAll().ToListAsync();

            return users;
        }

        public async Task<IEnumerable<Course>> GetTrainerCreatedCoursesAsync(Guid trainerId)
        {
            var courses = await Context.Courses
                .Where(model => model.CreatorId == trainerId)
                .ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<SportEvent>> GetTrainerCreatedEventsAsync(Guid trainerId)
        {
            var events = await Context.SportEvents
                .Where(model => model.TrainerId == trainerId)
                .ToListAsync();

            return events;
        }

        public async Task<IEnumerable<Video>> GetTrainerCreatedVideosAsync(Guid trainerId)
        {
            var videos = await Context.Videos
                .Where(model => model.UserId == trainerId)
                .ToListAsync();


            return videos;
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            var user = await GetModelAsync(userId);

            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var userCredentials = await Context.Users
                .Where(model => model.UserName == username)
                .FirstOrDefaultAsync();

            var user = await GetAll()
                .Where(user => user.UserCredentialsId == userCredentials.Id)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<Comment>> GetUserCommentsAsync(Guid userId)
        {
            var comments = await Context.Comments
                .Where(model => model.CommentatorId == userId)
                .ToListAsync();

            return comments;
        }

        public async Task<UserCredentials> GetUserCredentialsAsync(Guid userId)
        {
            var user = await GetModelAsync(userId);
            var userCredentials = await Context.Users
                .Where(model => model.Id == user.UserCredentialsId)
                .FirstOrDefaultAsync();

            return userCredentials;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await Context.Orders
                .Where(model => model.CustomerId == userId)
                .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<Post>> GetUserPostsAsync(Guid userId)
        {
            var posts = await Context.Posts
                .Where(model => model.CreatorId == userId)
                .ToListAsync();

            return posts;
        }

        public async Task<IEnumerable<Video>> GetUserSavedVideosAsync(Guid userId)
        {
            var videosIds = await Context.StoreVideos
                .Where(model => model.UserId == userId)
                .Select(model => model.SavedVideoId)
                .ToListAsync();

            var videos = new List<Video>();

            foreach (var videoId in videosIds)
            {
                var video = await Context.Videos.FindAsync(videoId);
                videos.Add(video);
            }

            return videos;
        }

        public async Task<IEnumerable<Course>> GetUserSubscribeCoursesAsync(Guid userId)
        {
            var coursesIds = await Context.SubscribeCourses
                .Where(model => model.SubscriberId == userId)
                .Select(model => model.SubscribeCourseId)
                .ToListAsync();

            var courses = new List<Course>();

            foreach (var videoId in coursesIds)
            {
                var course = await Context.Courses.FindAsync(videoId);
                courses.Add(course);
            }

            return courses;
        }

        public async Task<IEnumerable<SubscribeCourse>> GetUserSubscribeCoursesByIdAsync(Guid userId)
        {
            var models = await Context.SubscribeCourses
                .Where(x => x.SubscriberId == userId)
                .ToListAsync();

            return models;
        }

        public async Task<IEnumerable<SportEvent>> GetUserSubscribeEventsAsync(Guid userId)
        {
            var sportEventsIds = await Context.SubscribeCourses
                .Where(model => model.SubscriberId == userId)
                .Select(model => model.SubscribeCourseId)
                .ToListAsync();

            var events = new List<SportEvent>();

            foreach (var sportEventIds in sportEventsIds)
            {
                var sportEvent = await Context.SportEvents.FindAsync(sportEventIds);
                events.Add(sportEvent);
            }

            return events;
        }

        public async Task UpdateUserAsync(User user)
        {
            Update(user);
            await SaveChangesAsync();
        }
    }
}
