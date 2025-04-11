using BookStoreApp.Blazor.Server.UI.Service.Base;

namespace BookStoreApp.Blazor.Server.UI.Service.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);

        public Task Logout();

    }
}
