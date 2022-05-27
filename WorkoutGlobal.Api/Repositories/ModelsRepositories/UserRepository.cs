using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IUserCredentialsRepository _userCredentialsRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ICommentRepository _commentRepository;

        public UserRepository
            (WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager,
            IUserCredentialsRepository userCredentialsRepository,
            ICourseRepository courseRepository,
            IVideoRepository videoRepository,
            ICommentRepository commentRepository) 
            : base(workoutGlobalContext, configurationManager)
        {
            _userCredentialsRepository = userCredentialsRepository;
            _courseRepository = courseRepository;
            _videoRepository = videoRepository;
            _commentRepository = commentRepository;
        }

        public async Task CreateUserAsync(User user)
        {
            await CreateAsync(user);
            await SaveChangesAsync();
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
            var courses = await _courseRepository.GetCreatorCoursesAsync(trainerId);

            return courses;
        }

        public async Task<IEnumerable<Video>> GetTrainerCreatedVideosAsync(Guid trainerId)
        {
            var videos = await _videoRepository.GetCreatorVideosAsync(trainerId);

            return videos;
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            var user = await GetModelAsync(userId);

            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var userCredentials = _userCredentialsRepository.GetUserCredentialsByUserName(username);

            var user = await GetAll()
                .Where(user => user.UserCredentialsId == userCredentials.Id)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<Comment>> GetUserComments(Guid userId)
        {
            var comments = await _commentRepository.GetCreatorCommentsAsync(userId);

            return comments;
        }

        public async Task<UserCredentials> GetUserCredentialsAsync(Guid userId)
        {
            var user = await GetModelAsync(userId);
            var userCredentials = await _userCredentialsRepository.GetUserCredentialsAsync(user.UserCredentialsId);

            return userCredentials;
        }

        public Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId)
        {
            // TODO: Сначала нужно создать репу OrderRepositoty метод GetCreatorOrdersAsync
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetUserPostsAsync(Guid userId)
        {
            // TODO: Сначала нужно создать репу PostRepository метод GetCreatorPostsAsync
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Video>> GetUserSavedVideosAsync(Guid userId)
        {
            // TODO: Сначала нужно создать репу StoreVideoRepository и метод GetCreatorStoteVideosAsync
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetUserSubscribeCoursesAsync(Guid userId)
        {
            // TODO: Сначала нужно создать репу SubscribeCourseRepository и метод GetCreatorSubscribeCoursesAsync
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SportEvent>> GetUserSubscribeEventsAsync(Guid userId)
        {
            // TODO: Сначала нужно создать репу SubscribeEventRepository и метод GetCreatorSubscribeEventsAsync
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(User user)
        {
            await UpdateUserAsync(user);
            await SaveChangesAsync();
        }
    }
}
