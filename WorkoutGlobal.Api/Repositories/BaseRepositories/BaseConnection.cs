using WorkoutGlobal.Api.Context;

namespace WorkoutGlobal.Api.Repositories.BaseRepositories
{
    /// <summary>
    /// Represents base class for project database connection and configuration.
    /// </summary>
    public abstract class BaseConnection
    {
        private protected WorkoutGlobalContext _workoutGlobalContext;
        private protected IConfiguration _configuration;

        /// <summary>
        /// Sets database context and project configuration.
        /// </summary>
        /// <param name="workoutGlobalContext">Database context.</param>
        /// <param name="configurationManager">Project configuration.</param>
        public BaseConnection(
            WorkoutGlobalContext workoutGlobalContext,
            IConfiguration configurationManager)
        {
            _workoutGlobalContext = workoutGlobalContext;
            _configuration = configurationManager;
        }

        /// <summary>
        /// Database context.
        /// </summary>
        public WorkoutGlobalContext Context => _workoutGlobalContext;

        /// <summary>
        /// Project configuration.
        /// </summary>
        public IConfiguration Configuration => _configuration;
    }
}
