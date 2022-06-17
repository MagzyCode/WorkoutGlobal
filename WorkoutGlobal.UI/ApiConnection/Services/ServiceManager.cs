using WorkoutGlobal.UI.ApiConnection.Contracts;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly ICategoryService _categoryService;
        private readonly ICommentsBlockService _commentsBlockService;
        private readonly ICommentService _commentService;
        private readonly ICourseService _courseService;
        private readonly ISportEventService _sportEventService;
        private readonly IStoreVideoService _storeVideoService;
        private readonly ISubscribeCourseService _subscribeCourseService;
        private readonly ISubscribeEventService _subscribeEventService;
        private readonly IUserCredentialsServive _userCredentialsServive;
        private readonly IUserService _userService;
        private readonly IVideoService _videoService;
        private readonly ICourseVideoService _courseVideoService;
        private readonly IAuthenticationService _authenticationService;

        public ServiceManager(
            ICategoryService categoryService,
            ICommentsBlockService commentsBlockService,
            ICommentService commentService,
            ICourseService courseService,
            ISportEventService sportEventService,
            IStoreVideoService storeVideoService,
            ISubscribeCourseService subscribeCourseService,
            ISubscribeEventService subscribeEventService,
            IUserCredentialsServive userCredentialsServive,
            IUserService userService,
            IVideoService videoService,
            ICourseVideoService courseVideoService,
            IAuthenticationService authenticationService)
        {
            _categoryService = categoryService;
            _commentsBlockService = commentsBlockService;
            _commentService = commentService;
            _courseService = courseService;
            _sportEventService = sportEventService;
            _subscribeCourseService = subscribeCourseService;
            _subscribeEventService = subscribeEventService;
            _userCredentialsServive = userCredentialsServive;
            _userService = userService;
            _videoService = videoService;
            _storeVideoService = storeVideoService;
            _courseVideoService = courseVideoService;
            _authenticationService = authenticationService;
        }

        public ICategoryService CategoryService => _categoryService;

        public ICommentsBlockService CommentsBlockService => _commentsBlockService;

        public ICommentService CommentService => _commentService;

        public ICourseService CourseService => _courseService;

        public ISportEventService SportEventService => _sportEventService;

        public IStoreVideoService StoreVideoService => _storeVideoService;

        public ISubscribeCourseService SubscribeCourseService => _subscribeCourseService;

        public ISubscribeEventService SubscribeEventService => _subscribeEventService;

        public IUserCredentialsServive UserCredentialsServive => _userCredentialsServive;

        public IUserService UserService => _userService;

        public IVideoService VideoService => _videoService;
        public ICourseVideoService CourseVideoService => _courseVideoService;

        public IAuthenticationService AuthenticationService => _authenticationService;
    }
}
