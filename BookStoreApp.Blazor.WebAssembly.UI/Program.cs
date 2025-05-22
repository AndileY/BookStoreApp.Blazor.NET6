using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Service.Authentication;
using BookStoreApp.Blazor.WebAssembly.UI;
using BookStoreApp.Blazor.WebAssembly.UI.Providers;
using BookStoreApp.Blazor.WebAssembly.UI.Service;
using BookStoreApp.Blazor.WebAssembly.UI.Service.Authentication;
using BookStoreApp.Blazor.WebAssembly.UI.Service.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7017") });


builder.Services.AddBlazoredLocalStorage();




builder.Services.AddScoped<ApiAuthenticationStateProvider>(); //jwt

builder.Services.AddScoped<AuthenticationStateProvider>(p
    => p.GetRequiredService<ApiAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IClient, Client>();


// Register your custom authentication service with ASP.NET Core's Dependency Injection (DI) container
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

var config = new MapperConfiguration(cfg =>
{
    cfg.ShouldMapMethod = mi => false; // Prevent AutoMapper from scanning extension methods
    cfg.AddProfile<BookStoreApp.Blazor.WebAssembly.UI.Data.Configuration.MapperConfig>();
});



builder.Services.AddSingleton(config.CreateMapper());


await builder.Build().RunAsync();
