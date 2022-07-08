using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models;

namespace WorkoutGlobal.Api.Repositories
{
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
            await _userManager.DeleteAsync(userCredentials); 
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<UserCredentials>> GetAllUserCredentialsAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return users;
        }

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
            await _userManager.UpdateAsync(userCredentials);
            await SaveChangesAsync();
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

        public async Task Purge(UserCredentials userCredentials)
        {
            await _userManager.DeleteAsync(userCredentials);
            await SaveChangesAsync();
        }
    }
}
