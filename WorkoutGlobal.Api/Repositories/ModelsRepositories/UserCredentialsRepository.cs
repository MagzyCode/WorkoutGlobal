using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    /// <summary>
    /// Repository for main user credential actions.
    /// </summary>
    public class UserCredentialsRepository : BaseRepository<UserCredentials>, IUserCredentialsRepository
    {
        private readonly UserManager<UserCredentials> _userManager;

        public UserCredentialsRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager,
            UserManager<UserCredentials> userManager) 
            : base(workoutGlobalContext, configurationManager)
        {
            _userManager = userManager;
        }

        public async Task DeleteUserCredentialsAsync(UserCredentials userCredentials)
        {
            // TODO: Проверить, работает ли без метода SaveChanges
            await _userManager.DeleteAsync(userCredentials);
        }

        public async Task<IEnumerable<UserCredentials>> GetAllUserCredentialsAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return users;
        }

        /// <summary>
        /// Get user hashed password by real password and existed password salt.
        /// </summary>
        /// <param name="password">User password.</param>
        /// <param name="salt">User pasword salt.</param>
        /// <returns>Password hash.</returns>
        //public async Task<string> GetHashPasswordAsync(string password, string salt)
        //{
        //    if (string.IsNullOrEmpty(password))
        //        throw new ArgumentNullException(nameof(password));

        //    if (string.IsNullOrEmpty(salt))
        //        throw new ArgumentNullException(nameof(salt));

        //    using var sha256 = SHA256.Create();
        //    var hashedBytes = await sha256.ComputeHashAsync(
        //        inputStream : new MemoryStream(Encoding.UTF8.GetBytes(password + salt)));

        //    var hashPassword = BitConverter.ToString(hashedBytes).ToString().ToLower().Replace("-", "");

        //    return hashPassword;
        //}

        public Task<UserCredentials> GetUserCredentialsAsync(string userCredentialsId)
        {
            var model = _userManager.FindByIdAsync(userCredentialsId);

            return model;
        }

        public UserCredentials GetUserCredentialsByUserName(string username)
        {
            var model = _userManager.Users
                .Where(userCredentials => userCredentials.UserName == username)
                .FirstOrDefault();

            return model;
        }

        public string GetUserCredentialsRole(string userCredentialsId)
        {
            var userRole = Context.UserRoles.Where(x => x.UserId == userCredentialsId).FirstOrDefault();

            var role = Context.Roles.Where(x => x.Id == userRole.RoleId).FirstOrDefault();

            return role.Name;
        }

        public async Task UpdateUserCredentialsAsync(UserCredentials userCredentials)
        {
            // TODO: Проверить, работает ли без методы SaveChanges
            await _userManager.UpdateAsync(userCredentials);
        }

        public async Task UpdateUserToTrainerAsync(string userCredentialsId)
        {
            var userCredentials = await Context.Users.FindAsync(userCredentialsId);

            await _userManager.RemoveFromRoleAsync(userCredentials, "User");

            await _userManager.AddToRoleAsync(userCredentials, "Trainer");

            var userAccount = Context.UserAccounts.Where(x => x.UserCredentialsId == userCredentialsId).FirstOrDefault();
            userAccount.IsStatusVerify = true;

            Context.UserAccounts.Update(userAccount);
            await Context.SaveChangesAsync();
        }
    }
}
