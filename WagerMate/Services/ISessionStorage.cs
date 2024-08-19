using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace WagerMate.Services;

public interface ISessionStorage
{
    public Task RedirectToLogin(string key, ICookieService cookieService, NavigationManager navigation);
    public Task SetSessionValue(string key, string value, ICookieService cookieService);
    public ValueTask<string> GetSessionValue(string key, ICookieService cookieService);
}