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
        {
        }

        public void Create(TModel model)
        {
            Context.Set<TModel>().Add(model);
        }

        public void Delete(TModel model)
        {
            Context.Set<TModel>().Remove(model);
        }

        public IQueryable<TModel> GetAll()
        {
            return Context.Set<TModel>();
        }

        public TModel GetModel(Guid id)
        {
            return Context.Set<TModel>().Find(id);
        }

        public void Update(TModel model)
        {
            Context.Set<TModel>().Update(model);
        }

        public void SaveChanges() => Context.SaveChanges();
    }
}
