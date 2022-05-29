using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface IUserService : IApiData
    {
        [Put("/api/users/{userId}")]
        public Task UpdateUserAsync(Guid userId, [Body] User user);

        [Delete("/api/users/{userId}")]
        public Task DeleteUserAsync(Guid userId);

        [Get("/api/users/{userId}")]
        public Task<User> GetUserAsync(Guid userId);

        [Get("/api/users")]
        public Task<IEnumerable<User>> GetAllUsersAsync();

        [Get("/api/users/{userId}/userCredential")]
        public Task<UserCredentials> GetUserCredentialsAsync(Guid userId);

        [Get("/api/users/{username}")]
        public Task<User> GetUserByUsernameAsync(string username);

        [Get("/api/users/{userId}/createdVideos")]
        public Task<IEnumerable<Video>> GetTrainerCreatedVideosAsync(Guid userId);

        [Get("/api/users/{userId}/createdCourses")]
        public Task<IEnumerable<Course>> GetTrainerCreatedCoursesAsync(Guid userId);

        [Get("/api/users/{userId}/createdEvents")]
        public Task<IEnumerable<SportEvent>> GetTrainerCreatedEventsAsync(Guid userId);

        [Get("/api/users/{userId}/orders")]
        public Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId);

        [Get("/api/users/{userId}/posts")]
        public Task<IEnumerable<Post>> GetUserPostsAsync (Guid userId);

        [Get("/api/users/{userId}/comments")]
        public Task<IEnumerable<Comment>> GetUserCommentsAsync(Guid userId);

        [Get("/api/users/{userId}/subscribeCourses")]
        public Task<IEnumerable<SubscribeCourse>> GetUserSubscribeCoursesAsync(Guid userId);

        [Get("/api/users/{userId}/savedVideos")]
        public Task<IEnumerable<Video>> GetUserSavedVideosAsync(Guid userId);

        [Get("/api/users/{userId}/subscribeEvents")]
        public Task<IEnumerable<SubscribeEvent>> GetUserSubscribeEventsAsync(Guid userId);

        [Post("/api/users")]
        public Task CreateUserAsync([Body] User user);
    }
}
