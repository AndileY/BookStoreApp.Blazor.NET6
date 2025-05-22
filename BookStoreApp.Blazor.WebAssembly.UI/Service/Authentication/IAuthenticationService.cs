using BookStoreApp.Blazor.WebAssembly.UI.Service.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Service.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);

        public Task Logout();

    }
}
