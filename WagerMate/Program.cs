using dotenv.net;
using WagerMate.Components;
using WagerMate.Service_Implementation.auth;
using WagerMate.Service_Implementation.betting;
using WagerMate.Service_Implementation.database;
using WagerMate.Service_Implementation.user;
using WagerMate.Services.auth;
using WagerMate.Services.betting;
using WagerMate.Services.database;
using WagerMate.Services.user;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
// Retrieve the connection string from the environment variable
var connectionString = Environment.GetEnvironmentVariable("CON_STR");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException(
        "The connection string is not set. Please configure the environment variable CON_STR");

builder.Configuration.AddEnvironmentVariables();

builder.Configuration["ConnectionStrings:Wagerdb"] = connectionString;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register the IDbConnection service for Dapper
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBetService, BetService>();
builder.Services.AddScoped<IUserBetService, UserBetService>();
builder.Services.AddScoped<ICaseService, CaseService>();
builder.Services.AddScoped<IWinnerService, WinnerService>();

builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<IHashService, HashService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();