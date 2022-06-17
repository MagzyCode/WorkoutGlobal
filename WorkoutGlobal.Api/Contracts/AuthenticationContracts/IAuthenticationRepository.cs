using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;

namespace WorkoutGlobal.Api.Contracts
{
    /// <summary>
    /// Base interface for authentication manager repository.
    /// </summary>
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// Checks the user's data when log in.
        /// </summary>
        /// <param name="userAuthorizationDto">User credentials.</param>
        /// <returns>If user credentials exists in system, return true,
        /// otherwise return false.</returns>
        public Task<bool> ValidateUserAsync(UserAuthorizationDto userAuthorizationDto);

        /// <summary>
        /// Create JWT-token for user.
        /// </summary>
        /// <param name="userAuthorizationDto">User credentials.</param>
        /// <returns>JWT-token in string format.</returns>
        public string CreateToken(UserAuthorizationDto userAuthorizationDto);

        /// <summary>
        /// Registrate user is system.
        /// </summary>
        /// <param name="userRegistrationDto">User registration credentials. </param>
        /// <returns></returns>
        public Task RegistrateUserAsync(UserRegistrationDto userRegistrationDto);

        /// <summary>
        /// Checks is registration use credentials already exists in system.
        /// </summary>
        /// <param name="userRegistrationDto">User registration credentials.</param>
        /// <returns></returns>
        public bool IsUserExisted(UserRegistrationDto userRegistrationDto);

        /// <summary>
        /// Generate valid user credentials on registration info.
        /// </summary>
        /// <param name="userCredentialsDto">User credentials.</param>
        public Task<UserCredentials> GenerateUserCredentialsAsync(UpdationUserCredentialsDto updationUserCredentialsDto);

        public Task<string> GenerateHashPasswordAsync(string password, string salt);
    }
}
