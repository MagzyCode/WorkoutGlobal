using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    public interface IUserCredentialsServive : IApiData
    {
        [Get("/api/userCredentials/{userCredentialId}")]
        public Task<UserCredentials> GetUserCredentialAsync(string userCredentialId);

        [Put("/api/userCredentials/{userCredentialId}")]
        public Task UpdateUserCredentialAsync(string userCredentialId, [Body] UserCredentials userCredentialsModel);

        [Delete("/api/userCredentials/{userCredentialId}")]
        public Task DeleteUserCredentialAsync(string userCredentialId);

        [Get("/api/userCredentials/{userCredentialId}/role")]
        public Task<string> GetUserCredentialRoleAsync(string userCredentialId);

        [Put("/api/userCredentials/{userCredentialId}/raising")]
        public Task UpdateUserToTrainerAsync(string userCredentialId);
    }
}
