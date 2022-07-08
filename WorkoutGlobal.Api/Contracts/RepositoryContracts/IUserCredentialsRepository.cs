using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Contracts
{
    /// <summary>
    /// Base interface for user credentials repository.
    /// </summary>
    public interface IUserCredentialsRepository
    {
        public Task<UserCredentials> GetUserCredentialsAsync(string userCredentialsId);

        public UserCredentials GetUserCredentialsByUserName(string username);

        public Task UpdateUserCredentialsAsync(UserCredentials userCredentials);

        public Task DeleteUserCredentialsAsync(UserCredentials userCredentials);

        public string GetUserCredentialsRole(string userCredentialsId);

        public Task UpdateUserToTrainerAsync(string userCredentialsId);

        public Task<IEnumerable<UserCredentials>> GetAllUserCredentialsAsync();

        public Task Purge(UserCredentials userCredentials);
    }
}
