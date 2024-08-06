using System.Data;
using dotenv.net;
using Npgsql;
using WagerMate.Components;
using WagerMate.Data;
using WagerMate.Services;
using WagerMate.Services.impl;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
// Retrieve the connection string from the environment variable
var connectionString = Environment.GetEnvironmentVariable("CON_STR");
Console.WriteLine(connectionString);

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("The connection string is not set. Please configure the environment variable CON_STR");
}


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // The path for the login page
        // Doesn't exist yet
        // options.LogoutPath = "/logout"; // The path for the logout page
        options.Cookie.Name = "LoginUserCookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });
 
// Authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeLoggedIn", policy =>
        policy.RequireAuthenticatedUser());
});

// Register the IDbConnection service for Dapper
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();