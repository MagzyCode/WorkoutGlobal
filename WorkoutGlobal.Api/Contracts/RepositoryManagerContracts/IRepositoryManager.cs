using WorkoutGlobal.Api.Contracts.RepositoryContracts;

namespace WorkoutGlobal.Api.Contracts.RepositoryManagerContracts
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
    }
}
