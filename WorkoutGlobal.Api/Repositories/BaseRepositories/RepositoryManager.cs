using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;

namespace WorkoutGlobal.Api.Repositories.BaseRepositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IHealthRepository _healthRepository;

        public RepositoryManager(
            IHealthRepository healthRepository)
        {
            _healthRepository = healthRepository;
        }

        public IHealthRepository HealthRepository => _healthRepository;
    }
}
