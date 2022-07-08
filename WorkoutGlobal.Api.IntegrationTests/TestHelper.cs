using System.Net.Http;

namespace WorkoutGlobal.Api.IntegrationTests
{
    internal class TestHelper
    {
        private HttpClient _apiClient = new();
        private string? _url;
        private HttpMethod? _method;

        public HttpClient ApiHttpClient
        {
            get => _apiClient;
            set => _apiClient = value;
        }

        public string? Url
        {
            get => _url;
            set => _url = value;
        }

        public HttpMethod? Method
        {
            get => _method;
            set => _method = value;
        }

        public HttpRequestMessage Request
        {
            get => new(_method!, _url);
        }
    }
}
