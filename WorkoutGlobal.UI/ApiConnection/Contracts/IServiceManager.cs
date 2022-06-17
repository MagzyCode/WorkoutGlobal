namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface IServiceManager
    {
        public ICategoryService CategoryService { get; }
        public ICommentsBlockService CommentsBlockService { get; }
        public ICommentService CommentService { get; }
        public ICourseService CourseService { get; }
        public ISportEventService SportEventService { get; }
        public IStoreVideoService StoreVideoService { get; }
        public ISubscribeCourseService SubscribeCourseService { get; }
        public ISubscribeEventService SubscribeEventService { get; }
        public IUserCredentialsServive UserCredentialsServive { get; }
        public IUserService UserService { get; }
        public IVideoService VideoService { get; }
        public ICourseVideoService CourseVideoService { get; }
        public IAuthenticationService AuthenticationService { get; }
    }
}
