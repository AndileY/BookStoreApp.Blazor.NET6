using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStoreApp.Blazor.WebAssembly.UI.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
        private readonly IJSRuntime jsRuntime;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage, IJSRuntime jsRuntime)
        {
            this.localStorage = localStorage;
            this.jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            this.jsRuntime = jsRuntime;

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            //This protects against calling JS (like localStorage) during server-side prerendering, which would crash otherwise.
            if (jsRuntime is IJSInProcessRuntime == false)
            {
                return new AuthenticationState(user);
            }

            var savedToken = await localStorage.GetItemAsync<string>("accessToken");

            if(savedToken == null)
            {
                return new AuthenticationState(user);
                
            };
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            if(tokenContent.ValidTo <DateTime.UtcNow)
            {
                return new AuthenticationState(user);
            }
            var claims = tokenContent.Claims;
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            return new AuthenticationState(user);


        }

        public async Task LoggedIn()
        {
            
            var claims = await GetClaims();
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);


        }
        public async Task  LoggedOut()
        {
        
            await localStorage.RemoveItemAsync("accessToken");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);

        }

        private async Task<List<Claim>> GetClaims()
        {
            var savedToken = await localStorage.GetItemAsync<string>("accessToken");
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }

        
    }
}
