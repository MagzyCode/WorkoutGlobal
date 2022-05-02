using WorkoutGlobal.Api.Models.DTOs.UserDTOs;

namespace WorkoutGlobal.Api.Contracts.AuthenticationManagerContracts
{
    /// <summary>
    /// Base interface for authentication manager repository.
    /// </summary>
    public interface IAuthenticationManager
    {
        /// <summary>
        /// Checks the user's data when log in.
        /// </summary>
        /// <param name="userAuthorizationDto">User credentials.</param>
        /// <returns>If user credentials exists in system, return true,
        /// otherwise return false.</returns>
        public Task<bool> ValidateUser(UserAuthorizationDto userAuthorizationDto);

        /// <summary>
        /// Create JWT-token for user.
        /// </summary>
        /// <returns>JWT-token in string format.</returns>
        public Task<string> CreateToken();
    }
}
