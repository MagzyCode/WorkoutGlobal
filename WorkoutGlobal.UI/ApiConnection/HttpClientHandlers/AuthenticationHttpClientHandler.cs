using System.Net.Http.Headers;
using WorkoutGlobal.UI.ApiConnection.Contracts;

namespace WorkoutGlobal.UI.ApiConnection.HttpClientHandlers
{
    /// <summary>
    /// Http client handler for adding access token in request headers.
    /// </summary>
    public class AuthenticationHttpClientHandler : HttpClientHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Sets http instances.
        /// </summary>
        /// <param name="httpContextAccessor">Http context accessor instance.</param>
        /// <param name="tokenService">Token service instance.</param>
        public AuthenticationHttpClientHandler(
            IHttpContextAccessor httpContextAccessor,
            ITokenService tokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Append authorization header to request.
        /// </summary>
        /// <param name="request">Api request.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
            {
                var token = _tokenService.GetToken(_httpContextAccessor.HttpContext).Replace("\"", "");

                if (token != string.Empty)
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
