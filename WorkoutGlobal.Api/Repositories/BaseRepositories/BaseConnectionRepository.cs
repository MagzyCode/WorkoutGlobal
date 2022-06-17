using WorkoutGlobal.Api.Context;

namespace WorkoutGlobal.Api.Repositories
{
    /// <summary>
    /// Represents base class for project database connection and configuration.
    /// </summary>
    public abstract class BaseConnectionRepository
    {
        private protected WorkoutGlobalContext _workoutGlobalContext;
        private protected IConfiguration _configuration;

        /// <summary>
        /// Ctor for base connection class.
        /// </summary>
        /// <param name="workoutGlobalContext">Database context.</param>
        /// <param name="configurationManager">Project configuration.</param>
        public BaseConnectionRepository(
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
