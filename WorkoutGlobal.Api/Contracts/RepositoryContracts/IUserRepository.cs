using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
    }
}
