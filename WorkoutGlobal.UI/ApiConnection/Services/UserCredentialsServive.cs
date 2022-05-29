using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    public class UserCredentialsServive : BaseService<IUserCredentialsServive>, IUserCredentialsServive
    {
        public UserCredentialsServive(IConfiguration configuration) : base(configuration)
        { }

        public async Task DeleteUserCredentialAsync(string userCredentialId)
            => await Service.DeleteUserCredentialAsync(userCredentialId);

        public async Task<UserCredentialsModel> GetUserCredentialAsync(string userCredentialId)
            => await Service.GetUserCredentialAsync(userCredentialId);

        public async Task<string> GetUserCredentialRoleAsync(string userCredentialId)
            => await Service.GetUserCredentialRoleAsync(userCredentialId);

        public async Task UpdateUserCredentialAsync(string userCredentialId, UserCredentialsModel userCredentialsModel)
            => await Service.UpdateUserCredentialAsync(userCredentialId, userCredentialsModel);

        public async Task UpdateUserToTrainerAsync(string userCredentialId)
            => await Service.UpdateUserToTrainerAsync(userCredentialId);
    }
}
