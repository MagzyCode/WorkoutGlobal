using WorkoutGlobal.Api.Contracts.AuthenticationManagerContracts;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;

namespace WorkoutGlobal.Api.Repositories.BaseRepositories
{
    /// <summary>
    /// Represents base repository manager class for manage all model repositories.
    /// </summary>
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IHealthRepository _healthRepository;
        private readonly IUserCredentialsRepository _userCredentialsRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentsBlockRepository _commentsBlockRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseVideoRepository _courseVideosRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISportEventRepository _sportEventRepository;
        private readonly IStoreVideoRepository _storeVideoRepository;
        private readonly ISubscribeCourseRepository _subscribeCourseRepository;
        private readonly ISubscribeEventRepository _subscribeEventRepository;

        /// <summary>
        /// Ctor for repository manager.
        /// </summary>
        /// <param name="healthRepository">Health repository instance.</param>
        /// <param name="userCredentialsRepository">User credentials repository instance.</param>
        /// <param name="authenticationRepository">Authentication repository instance.</param>
        /// <param name="videoRepository"></param>
        public RepositoryManager(
            IHealthRepository healthRepository,
            IUserCredentialsRepository userCredentialsRepository,
            IAuthenticationRepository authenticationRepository,
            IVideoRepository videoRepository,
            IUserRepository userRepository,
            ICommentsBlockRepository commentsBlockRepository,
            ICommentRepository commentRepository,
            ICourseRepository courseRepository,
            ICourseVideoRepository courseVideosRepository,
            ISportEventRepository sportEventRepository,
            IStoreVideoRepository storeVideoRepository,
            ISubscribeCourseRepository subscribeCourseRepository,
            ISubscribeEventRepository subscribeEventRepository) 
        {
            _healthRepository = healthRepository;
            _userCredentialsRepository = userCredentialsRepository;
            _authenticationRepository = authenticationRepository;
            _videoRepository = videoRepository;
            _userRepository = userRepository;
            _commentsBlockRepository = commentsBlockRepository;
            _commentRepository = commentRepository;
            _courseRepository = courseRepository;
            _courseVideosRepository = courseVideosRepository;
            _sportEventRepository = sportEventRepository;
            _storeVideoRepository = storeVideoRepository;
            _subscribeCourseRepository = subscribeCourseRepository;
            _subscribeEventRepository = subscribeEventRepository;
        }

        /// <summary>
        /// Health repository instance.
        /// </summary>
        public IHealthRepository HealthRepository => _healthRepository;

        /// <summary>
        /// User credentials repository instance.
        /// </summary>
        public IUserCredentialsRepository UserCredentialRepository => _userCredentialsRepository;

        /// <summary>
        /// Authentication repository instance.
        /// </summary>
        public IAuthenticationRepository AuthenticationRepository => _authenticationRepository;

        public IVideoRepository VideoRepository => _videoRepository;

        public IUserRepository UserRepository => _userRepository;

        public ICommentsBlockRepository CommentsBlockRepository => _commentsBlockRepository;
        public ICommentRepository CommentRepository => _commentRepository;
        public ICourseRepository CourseRepository => _courseRepository;
        public ICourseVideoRepository CourseVideosRepository => _courseVideosRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public ISportEventRepository SportEventRepository => _sportEventRepository;
        public IStoreVideoRepository StoreVideoRepository => _storeVideoRepository;
        public ISubscribeCourseRepository SubscribeCourseRepository => _subscribeCourseRepository;
        public ISubscribeEventRepository SubscribeEventRepository => _subscribeEventRepository;

    }
}
