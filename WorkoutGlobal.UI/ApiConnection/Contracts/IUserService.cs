using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface IUserService : IApiData
    {
        [Put("/api/accounts/{accountId}")]
        public Task UpdateUserAsync(Guid accountId, [Body] User user);

        [Delete("/api/accounts/{accountId}")]
        public Task DeleteUserAsync(Guid accountId);

        [Get("/api/accounts/{accountId}")]
        public Task<User> GetUserAsync(Guid accountId);

        [Get("/api/accounts")]
        public Task<IEnumerable<User>> GetAllUsersAsync();

        [Get("/api/accounts/{accountId}/userCredential")]
        public Task<UserCredentialsModel> GetUserCredentialsAsync(Guid accountId);

        [Get("/api/accounts/account/{username}")]
        public Task<User> GetUserByUsernameAsync(string username);

        [Get("/api/accounts/{accountId}/createdVideos")]
        public Task<IEnumerable<Video>> GetTrainerCreatedVideosAsync(Guid accountId);

        [Get("/api/accounts/{accountId}/createdCourses")]
        public Task<IEnumerable<Course>> GetTrainerCreatedCoursesAsync(Guid accountId);

        [Get("/api/accounts/{accountId}/createdEvents")]
        public Task<IEnumerable<SportEvent>> GetTrainerCreatedEventsAsync(Guid accountId);

        [Get("/api/accounts/{accountId}/orders")]
        public Task<IEnumerable<Order>> GetUserOrdersAsync(Guid accountId);

        [Get("/api/accounts/{accountId}/posts")]
        public Task<IEnumerable<Post>> GetUserPostsAsync (Guid accountId);

        [Get("/api/accounts/{accountId}/comments")]
        public Task<IEnumerable<Comment>> GetUserCommentsAsync(Guid accountId);

        [Get("/api/accounts/{accountId}/savedCourses")]
        public Task<IEnumerable<Course>> GetUserSubscribeCoursesAsync(Guid accountId);

        [Get("/api/accounts/{accountId}/savedVideos")]
        public Task<IEnumerable<Video>> GetUserSavedVideosAsync(Guid accountId);

        [Get("/api/accounts/{accountId}/subscribeEvents")]
        public Task<IEnumerable<SportEvent>> GetUserSubscribeEventsAsync(Guid accountId);

        [Post("/api/accounts")]
        public Task CreateUserAsync([Body] User user);

        [Get("/api/accounts/{accountId}/subscriveCourses")]
        public Task<IEnumerable<SubscribeCourse>> GetUserSubscribeCoursesByIdAsync(Guid accountId);
    }
}
