using WorkoutGlobal.Api.DatabaseContext;

namespace WorkoutGlobal.Api.Repositories.BaseRepositories
{
    public abstract class BaseConnection
    {
        private protected WorkoutGlobalContext _workoutGlobalContext;
        private protected IConfiguration _configuration;

        public BaseConnection(
            WorkoutGlobalContext workoutGlobalContext,
            IConfiguration configurationManager)
        {
            _workoutGlobalContext = workoutGlobalContext;
            _configuration = configurationManager;
        }

        public WorkoutGlobalContext Context => _workoutGlobalContext;

        public IConfiguration Configuration => _configuration;
    }
}
