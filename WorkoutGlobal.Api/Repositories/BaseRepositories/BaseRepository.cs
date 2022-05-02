using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;

namespace WorkoutGlobal.Api.Repositories.BaseRepositories
{
    /// <summary>
    /// Represents base repository for all model repositories.
    /// </summary>
    /// <typeparam name="TModel">Model type.</typeparam>
    public abstract class BaseRepository<TModel> : BaseConnection, IBaseRepository<TModel>
        where TModel : class
    {
        /// <summary>
        /// Sets database context and project configuration.
        /// </summary>
        /// <param name="workoutGlobalContext">Database context.</param>
        /// <param name="configurationManager">Project configuration.</param>
        protected BaseRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager) 
            : base(workoutGlobalContext, configurationManager)
        { }

        /// <summary>
        /// Asynchronous creation of model.
        /// </summary>
        /// <param name="model">Creation model.</param>
        /// <returns>A task that represents asynchronous Create operation.</returns>
        public async Task CreateAsync(TModel model)
        {
            await Context.Set<TModel>().AddAsync(model);
        }

        /// <summary>
        /// Deleting model.
        /// </summary>
        /// <param name="model">Deleting model.</param>
        public void Delete(TModel model)
        {
            Context.Set<TModel>().Remove(model);
        }

        /// <summary>
        /// Get all models.
        /// </summary>
        /// <returns>Collection of all models.</returns>
        public IQueryable<TModel> GetAll()
        {
            var result = Context.Set<TModel>();

            return result;
        }

        /// <summary>
        /// Asynchronous getting of model.
        /// </summary>
        /// <param name="id">Id of getting model.</param>
        /// <returns>Dingle model.</returns>
        public async Task<TModel> GetModelAsync(Guid id)
        {
            var model = await Context.Set<TModel>().FindAsync();

            return model;
        }

        /// <summary>
        /// Update model.
        /// </summary>
        /// <param name="model">Updated model.</param>
        public void Update(TModel model)
        {
            Context.Set<TModel>().Update(model);
        }

        /// <summary>
        /// Asynchronous save changes in database.
        /// </summary>
        /// <returns>A task that represents asynchronous SaveChanges operaion.</returns>
        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();
    }
}
