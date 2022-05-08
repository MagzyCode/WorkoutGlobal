using WorkoutGlobal.UI.ApiConnection.Contracts;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    /// <summary>
    /// Represents base work with getting token from cookies.
    /// </summary>
    public class TokenService : ITokenService
    {
        /// <summary>
        /// Get access token.
        /// </summary>
        /// <param name="httpContent">Request http context.</param>
        /// <returns>Access token.</returns>
        public string GetToken(HttpContext httpContent)
        {
            var isTokenClaimExists = httpContent.User.Claims?.Any(x => x.Type == "Token");

            if (isTokenClaimExists.HasValue && isTokenClaimExists.Value)
            {
                return httpContent.User.Claims?.First(x => x.Type == "Token")?.Value;
            }

            return string.Empty;
        }
    }
}
