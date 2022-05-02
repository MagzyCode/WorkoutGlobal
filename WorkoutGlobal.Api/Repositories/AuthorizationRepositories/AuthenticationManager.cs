using WorkoutGlobal.Api.Contracts.AuthenticationManagerContracts;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;

namespace WorkoutGlobal.Api.Repositories.AuthorizationRepositories
{
    /// <summary>
    /// Represents authorization manager for log in.
    /// </summary>
    public class AuthenticationManager : IAuthenticationManager
    {
        /// <summary>
        /// Create JWT-token for user.
        /// </summary>
        /// <returns>JWT-token.</returns>
        public Task<string> CreateToken()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks the user's data when log in.
        /// </summary>
        /// <param name="userAuthorizationDto">User credentials.</param>
        /// <returns>If user credentials exists in system, return true,
        /// otherwise return false.</returns>
        public Task<bool> ValidateUser(UserAuthorizationDto userAuthorizationDto)
        {
            throw new NotImplementedException();
        }
    }
}
