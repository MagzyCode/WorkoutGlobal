using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        //private readonly IUserCredentialsRepository _userCredentialsRepository;
        //private readonly ICourseRepository _courseRepository;
        //private readonly IVideoRepository _videoRepository;
        //private readonly ICommentRepository _commentRepository;
        //private readonly ISportEventRepository _sportEventRepository;
        //private readonly IStoreVideoRepository _storeVideoRepository;
        //private readonly ISubscribeCourseRepository _subscribeCourseRepository;
        //private readonly ISubscribeEventRepository _subscribeEventRepository;

        public UserRepository
            (WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager)
            //IUserCredentialsRepository userCredentialsRepository,
            //ICourseRepository courseRepository,
            //IVideoRepository videoRepository,
            //ICommentRepository commentRepository,
            //ISportEventRepository sportEventRepository,
            //IStoreVideoRepository storeVideoRepository,
            //ISubscribeCourseRepository subscribeCourseRepository,
            //ISubscribeEventRepository subscribeEventRepository) 
            : base(workoutGlobalContext, configurationManager)
        {
            //_userCredentialsRepository = userCredentialsRepository;
            //_courseRepository = courseRepository;
            //_videoRepository = videoRepository;
            //_commentRepository = commentRepository;
            //_sportEventRepository = sportEventRepository;
            //_storeVideoRepository = storeVideoRepository;
            //_subscribeCourseRepository = subscribeCourseRepository;
            //_subscribeEventRepository = subscribeEventRepository;
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
            var courses = await Context.Courses
                .Where(model => model.CreatorId == trainerId)
                .ToListAsync();
                //await _courseRepository.GetCreatorCoursesAsync(trainerId);

            return courses;
        }

        public async Task<IEnumerable<SportEvent>> GetTrainerCreatedEventsAsync(Guid trainerId)
        {
            var events = await Context.SportEvents
                .Where(model => model.TrainerId == trainerId)
                .ToListAsync();
                // await _sportEventRepository.GetCreatorEventsAsync(trainerId);

            return events;
        }

        public async Task<IEnumerable<Video>> GetTrainerCreatedVideosAsync(Guid trainerId)
        {
            var videos = await Context.Videos
                .Where(model => model.UserId == trainerId)
                .ToListAsync();
                // await _videoRepository.GetCreatorVideosAsync(trainerId);

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
                // _userCredentialsRepository.GetUserCredentialsByUserName(username);

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
                //await _commentRepository.GetCreatorCommentsAsync(userId);

            return comments;
        }

        public async Task<UserCredentials> GetUserCredentialsAsync(Guid userId)
        {
            var user = await GetModelAsync(userId);
            var userCredentials = await Context.Users
                .Where(model => model.Id == user.UserCredentialsId)
                .FirstOrDefaultAsync();
                
                // await _userCredentialsRepository.GetUserCredentialsAsync(user.UserCredentialsId);

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
            // var videos = await _storeVideoRepository.GetUserVideosAsync(userId);

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
            // var courses = await _subscribeCourseRepository.GetUserSubscribeCoursesAsync(userId);

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

        public async Task<IEnumerable<SportEvent>> GetUserSubscribeEventsAsync(Guid userId)
        {
            // var events = await _subscribeEventRepository.GetUserSubscribeEventsAsync(userId);
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
            await UpdateUserAsync(user);
            await SaveChangesAsync();
        }
    }
}
