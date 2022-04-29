using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.DatabaseContext;

namespace WorkoutGlobal.Api.Repositories.BaseRepositories
{
    public abstract class BaseRepository<TModel> : BaseConnection, IBaseRepository<TModel>
        where TModel : class
    {
        protected BaseRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager) 
            : base(workoutGlobalContext, configurationManager)
        { }

        public async Task CreateAsync(TModel model)
        {
            await Context.Set<TModel>().AddAsync(model);
        }

        public void Delete(TModel model)
        {
            Context.Set<TModel>().Remove(model);
        }

        public IQueryable<TModel> GetAll()
        {
            var result = Context.Set<TModel>();

            return result;
        }

        public async Task<TModel> GetModelAsync(Guid id)
        {
            var model = await Context.Set<TModel>().FindAsync(); ;

            return model;
        }

        public void Update(TModel model)
        {
            Context.Set<TModel>().Update(model);
        }

        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();
    }
}
