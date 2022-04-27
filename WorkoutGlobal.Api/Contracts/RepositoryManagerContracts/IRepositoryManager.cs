using WorkoutGlobal.Api.Contracts.RepositoryContracts;

namespace WorkoutGlobal.Api.Contracts.RepositoryManagerContracts
{
    public interface IRepositoryManager
    {
        public IHealthRepository HealthRepository { get; }
    }
}
