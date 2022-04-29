using WorkoutGlobal.Api.DatabaseContext;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface IBaseRepository<TModel>
    {
        public WorkoutGlobalContext Context { get; }
        public Task CreateAsync(TModel model);
        public void Update(TModel model);
        public void Delete(TModel model);
        public IQueryable<TModel> GetAll();
        public Task<TModel> GetModelAsync(Guid id);
        public Task SaveChangesAsync();
    }
}
