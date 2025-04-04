using BookStoreAppApI.Configurations;
using BookStoreAppApI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("BookStoreAppDbConnection");

builder.Services.AddDbContext<BookStoreAppDboContext>(options => options.UseSqlServer(connString));


//builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
    b => b.AllowAnyMethod()
    .AllowAnyHeader().AllowAnyOrigin());
});

//builder.Services.AddAutoMapper(typeof(MapperConfig));
//builder.Services.AddIdentityCore<IdentityUser>()
//    .AddRoles<IdentityUser>()
//    .AddEntityFrameworkStores<BookStoreAppDboContext>();

// Register ASP.NET Core Identity (with roles)
builder.Services.AddIdentity<ApiUser, IdentityRole>()
    .AddEntityFrameworkStores<BookStoreAppDboContext>()
    .AddDefaultTokenProviders();

// Register AutoMapper and specify the assembly that contains the MapperConfig class
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();
//app.UseRouting();

app.MapControllers();

app.Run();
