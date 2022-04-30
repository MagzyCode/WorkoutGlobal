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

        /// <summary>
        /// Sets instances of model repositories.
        /// </summary>
        /// <param name="healthRepository"></param>
        public RepositoryManager(
            IHealthRepository healthRepository)
        {
            _healthRepository = healthRepository;
        }

        /// <summary>
        /// Health repositoty instance.
        /// </summary>
        public IHealthRepository HealthRepository => _healthRepository;
    }
}
