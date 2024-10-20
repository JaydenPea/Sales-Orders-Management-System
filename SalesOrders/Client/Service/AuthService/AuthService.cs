using Microsoft.AspNetCore.Components.Authorization;
using SalesOrders.Shared.Users.Models;
using SalesOrders.Shared;
using System.Net.Http.Json;
using SalesOrders.Shared.User.Models;

namespace SalesOrders.Client.Service.AuthService
{
    public class AuthService : IAuthService
    {
        #region Injections and Constructors
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        #endregion

        #region Methods
        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/user/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/user/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<List<GetUsersModel>> GetAllUsers()
        {
            var result = await _http.GetAsync("api/user/getUsersList");
            return await result.Content.ReadFromJsonAsync<List<GetUsersModel>>();
        }

        public event Action OnChange;
        #endregion
    }
}
