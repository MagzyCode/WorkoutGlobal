using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WorkoutGlobal.Api.DatabaseContext;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface IBaseRepository<TModel>
    {
        public WorkoutGlobalContext Context { get; }
        // public IConfiguration Configuration { get; }
        public void Create(TModel model);
        public void Update(TModel model);
        public void Delete(TModel model);
        public IQueryable<TModel> GetAll();
        public TModel GetModel(Guid id);
        public void SaveChanges();
    }
}
