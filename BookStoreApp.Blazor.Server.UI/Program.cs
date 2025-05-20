//using BookStoreApp.Blazor.Server.UI.Data;
using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Data.Configuration;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Service;
using BookStoreApp.Blazor.Server.UI.Service.Authentication;
using BookStoreApp.Blazor.Server.UI.Service.Base;
using BookStoreAppApI.Configurations;
//using BookStoreApp.Blazor.Server.UI.Service.UI;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper.Internal;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.SetMinimumLevel(LogLevel.Debug);
        builder.Logging.AddConsole();

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7017"));

        // Register your custom authentication service with ASP.NET Core's Dependency Injection (DI) container
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddScoped<IBookService, BookService>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapMethod = mi => false; // Prevent AutoMapper from scanning extension methods
            cfg.AddProfile<BookStoreApp.Blazor.Server.UI.Data.Configuration.MapperConfig>();
        });

        builder.Services.AddSingleton(config.CreateMapper());



        builder.Services.AddScoped<ApiAuthenticationStateProvider>(); //jwt
        builder.Services.AddScoped<AuthenticationStateProvider>(p
            => p.GetRequiredService<ApiAuthenticationStateProvider>());
        builder.Services.AddServerSideBlazor()
            .AddHubOptions(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(10);
                options.HandshakeTimeout = TimeSpan.FromSeconds(30);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);

            });


        var app = builder.Build();

        var assembliesWithMaxFloat = AppDomain.CurrentDomain.GetAssemblies()
           .SelectMany(asm =>
           {
               try
               {
                   return asm.GetTypes()
                       .Where(t => t.IsSealed && t.IsAbstract && t.IsPublic)
                       .SelectMany(t => t.GetMethods())
                       .Where(m => m.Name.Contains("MaxFloat"));
               }
               catch
               {
                   return Enumerable.Empty<System.Reflection.MethodInfo>(); // skip inaccessible assemblies
               }
           })
   .ToList();

        foreach (var method in assembliesWithMaxFloat)
        {
            Console.WriteLine($"{method.DeclaringType?.FullName} in {method.DeclaringType?.Assembly.Location}");
        }

        // Configure the HTTP request pipeline. 
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();



        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}