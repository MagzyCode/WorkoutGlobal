using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class UserService : BaseService<IUserService>, IUserService
    {
        public UserService(IConfiguration configuration) : base(configuration)
        { }

        public async Task CreateUserAsync([Body] User user)
            => await Service.CreateUserAsync(user);

        public async Task DeleteUserAsync(Guid userId)
            => await Service.DeleteUserAsync(userId);

        public async Task<IEnumerable<User>> GetAllUsersAsync()
            => await Service.GetAllUsersAsync();

        public async Task<IEnumerable<Course>> GetTrainerCreatedCoursesAsync(Guid userId)
            => await Service.GetTrainerCreatedCoursesAsync(userId);

        public async Task<IEnumerable<SportEvent>> GetTrainerCreatedEventsAsync(Guid userId)
            => await Service.GetTrainerCreatedEventsAsync(userId);

        public async Task<IEnumerable<Video>> GetTrainerCreatedVideosAsync(Guid userId)
            => await Service.GetTrainerCreatedVideosAsync(userId);

        public async Task<User> GetUserAsync(Guid userId)
            => await Service.GetUserAsync(userId);

        public async Task<User> GetUserByUsernameAsync(string username)
            => await Service.GetUserByUsernameAsync(username);

        public async Task<IEnumerable<Comment>> GetUserCommentsAsync(Guid userId)
            => await Service.GetUserCommentsAsync(userId);

        public async Task<UserCredentialsModel> GetUserCredentialsAsync(Guid userId)
            => await Service.GetUserCredentialsAsync(userId);

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId)
            => await Service.GetUserOrdersAsync(userId);

        public async Task<IEnumerable<Post>> GetUserPostsAsync(Guid userId)
            => await Service.GetUserPostsAsync(userId);

        public async Task<IEnumerable<Video>> GetUserSavedVideosAsync(Guid userId)
            => await Service.GetUserSavedVideosAsync(userId);

        public async Task<IEnumerable<Course>> GetUserSubscribeCoursesAsync(Guid userId)
            => await Service.GetUserSubscribeCoursesAsync(userId);

        public async Task<IEnumerable<SubscribeCourse>> GetUserSubscribeCoursesByIdAsync(Guid userId)
            => await Service.GetUserSubscribeCoursesByIdAsync(userId);

        public async Task<IEnumerable<SportEvent>> GetUserSubscribeEventsAsync(Guid userId)
            => await Service.GetUserSubscribeEventsAsync(userId);

        public async Task UpdateUserAsync(Guid userId, User user)
            => await Service.UpdateUserAsync(userId, user);
    }
}
