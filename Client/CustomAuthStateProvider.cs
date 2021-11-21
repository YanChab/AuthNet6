using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using AuthNet6.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace AuthNet6.Client

{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private List<User> _Users;
        private User user = new User();
        private readonly HttpClient _Http;

        public CustomAuthStateProvider(ILocalStorageService localStorageService,HttpClient Http)
        {
            _localStorageService = localStorageService;
            _Http=Http;
            
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(new ClaimsPrincipal());

            var token = await _localStorageService.GetItemAsync<string>("AuthToken");
            _Users = await _Http.GetFromJsonAsync<List<User>>("api/user");

            foreach (var authUser in _Users)
            {
                if (token == authUser.Username)
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, authUser.Username),
                    new Claim(ClaimTypes.Role, authUser.Role)
                    }, "test authentication type");
                    var user = new ClaimsPrincipal(identity);
                    state = new AuthenticationState(user);
                    break;
                }
            }




            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }
    }
}