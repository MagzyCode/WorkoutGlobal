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
            IVideoRepository videoRepository) 
        {
            _healthRepository = healthRepository;
            _userCredentialsRepository = userCredentialsRepository;
            _authenticationRepository = authenticationRepository;
            _videoRepository = videoRepository;
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
    }
}
