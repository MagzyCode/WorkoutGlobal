using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(User user);

        public Task UpdateUserAsync(User user);

        public Task DeleteUserAsync(User user);

        public Task<User> GetUserAsync(Guid userId);

        public Task<IEnumerable<User>> GetAllUsersAsync();

        public Task<UserCredentials> GetUserCredentialsAsync(Guid userId);

        public Task<User> GetUserByUsernameAsync(string username);

        public Task<IEnumerable<Video>> GetTrainerCreatedVideosAsync(Guid trainerId);

        public Task<IEnumerable<Course>> GetTrainerCreatedCoursesAsync(Guid trainerId);

        public Task<IEnumerable<SportEvent>> GetTrainerCreatedEventsAsync(Guid trainerId);

        public Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId);

        public Task<IEnumerable<Post>> GetUserPostsAsync(Guid userId);

        public Task<IEnumerable<Comment>> GetUserCommentsAsync(Guid userId);

        public Task<IEnumerable<Course>> GetUserSubscribeCoursesAsync(Guid userId);

        public Task<IEnumerable<Video>> GetUserSavedVideosAsync(Guid userId);

        public Task<IEnumerable<SportEvent>> GetUserSubscribeEventsAsync(Guid userId);
    }
}
