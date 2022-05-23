using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.Models;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    /// <summary>
    /// Represents authentication service for api connection.
    /// </summary>
    public class AuthenticationService : BaseService<IAuthenticationService>, IAuthenticationService
    {
        /// <summary>
        /// Ctor authentication service.
        /// </summary>
        /// <param name="configuration">Project configuration.</param>
        public AuthenticationService(IConfiguration configuration)
            : base(configuration)
        { }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="authenticationUser">User credentials.</param>
        /// <returns>Access token.</returns>
        public async Task<string> AuthenticateAsync(AuthenticationUser authenticationUser)
            => await Service.AuthenticateAsync(authenticationUser);

        /// <summary>
        /// Registrate user.
        /// </summary>
        /// <param name="userCredentials">User credentials.</param>
        /// <returns>A task that represents asynchronous Registrate operation.</returns>
        public async Task RegistrateAsync(UserCredentials userCredentials)
            => await Service.RegistrateAsync(userCredentials);
    }
}
