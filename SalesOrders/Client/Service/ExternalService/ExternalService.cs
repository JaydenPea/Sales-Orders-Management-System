using SalesOrders.Shared;
using SalesOrders.Shared.ExternalCalls.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace SalesOrders.Client.Service.ExternalService
{
    public class ExternalService : IExternalService
    {
        #region Injections and Constructors
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public ExternalService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }
        #endregion

        #region Methods
        public async Task<ExternalCallVM> externalCall()
        {
            var result = await _http.GetAsync("api/external/externalCall");
            return await result.Content.ReadFromJsonAsync<ExternalCallVM>();
        }
        #endregion
    }
}
