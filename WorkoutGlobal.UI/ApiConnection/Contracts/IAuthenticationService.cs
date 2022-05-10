using Refit;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Contracts
{
    /// <summary>
    /// Represents base structure of authentication endpoints.
    /// </summary>
    public interface IAuthenticationService : IApiData
    {
        /// <summary>
        /// Authenticate user by credentials.
        /// </summary>
        /// <param name="authenticationUser">User credentials.</param>
        /// <returns>Access token.</returns>
        [Post("/api/authentication/login")]
        public Task<string> AuthenticateAsync([Body] AuthenticationUser authenticationUser);

        /// <summary>
        /// Registrate user in system.
        /// </summary>
        /// <param name="userCredentials">User registration info.</param>
        /// <returns>A task that represents asynchronous Registrate operation.</returns>
        [Post("/api/authentication/registration")]
        public Task RegistrateAsync([Body] UserCredentials userCredentials);
    }
}
