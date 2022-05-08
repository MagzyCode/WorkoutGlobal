using Refit;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.ApiConnection.HttpClientHandlers;

namespace WorkoutGlobal.UI.ApiConnection.Services
{
    /// <summary>
    /// Base api service.
    /// </summary>
    public abstract class BaseService<T>
        where T : IApiData
    {
        private protected readonly IConfiguration _configuration;
        private protected readonly IHttpContextAccessor _httpContextAccessor;
        private protected readonly ITokenService _tokenService;
        private protected readonly T _service;

        /// <summary>
        /// Sets base configuration.
        /// </summary>
        /// <param name="configuration">Project configuration.</param>
        public BaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            _service = RestService.For<T>(_configuration["ApiBaseUrl"]);
        }

        /// <summary>
        /// Sets base api connection configurations.
        /// </summary>
        /// <param name="configuration">Api connection configuration.</param>
        /// <param name="httpContextAccessor">Http context accessor instance.</param>
        /// <param name="tokenService">Token service instance.</param>
        public BaseService(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ITokenService tokenService)
        {   
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
            _service = RestService.For<T>(
                new HttpClient(
                    new AuthenticationHttpClientHandler(httpContextAccessor, tokenService))
                {
                    BaseAddress = new Uri(_configuration["ApiBaseUrl"])
                });
        }
    }
}
