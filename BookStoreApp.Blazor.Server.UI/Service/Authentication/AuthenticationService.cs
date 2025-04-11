using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Service.Base;
//using BookStoreApp.Blazor.Server.UI.Service.Base;
//using BookStoreApp.Blazor.Server.UI.Service.UI;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace BookStoreApp.Blazor.Server.UI.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

     

      
        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
            //////// Make the login request
            var response = await httpClient.LoginAsync(loginModel);

            //store token
            await localStorage.SetItemAsStringAsync("accessToken", response.Token);

            //change auth state of app
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;

            


        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();

        }
    }
}