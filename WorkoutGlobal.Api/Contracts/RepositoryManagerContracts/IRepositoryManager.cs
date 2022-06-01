namespace WorkoutGlobal.Api.Contracts
{
    /// <summary>
    /// Represents structure of model repositories manager.
    /// </summary>
    public interface IRepositoryManager
    {
        /// <summary>
        /// Health repository instance.
        /// </summary>
        public IHealthRepository HealthRepository { get; }

        /// <summary>
        /// User credential repository instance.
        /// </summary>
        public IUserCredentialsRepository UserCredentialRepository { get; }

        /// <summary>
        /// Authentication repository instance.
        /// </summary>
        public IAuthenticationRepository AuthenticationRepository { get; }

        public IVideoRepository VideoRepository { get; }

        public IUserRepository UserRepository { get; }
        public ICommentsBlockRepository CommentsBlockRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ICourseRepository CourseRepository { get; }
        public ICourseVideoRepository CourseVideosRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ISportEventRepository SportEventRepository { get; }
        public IStoreVideoRepository StoreVideoRepository { get; }
        public ISubscribeCourseRepository SubscribeCourseRepository { get; }
        public ISubscribeEventRepository SubscribeEventRepository { get; }
    }
}
