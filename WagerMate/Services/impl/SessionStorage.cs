using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace WagerMate.Services.impl;

public class SessionStorage : ISessionStorage
{
    
    public async Task RedirectToLogin(string key, ICookieService cookieService, NavigationManager navigation)
    {
        var value = await cookieService.GetCookieByName(key);
        if (string.IsNullOrEmpty(value))
        {
            navigation.NavigateTo("/login");
        }
    }

    public ValueTask<string> GetSessionValue(string key, ICookieService cookieService)
    {
        ValueTask<string> sessionValue = cookieService.GetCookieByName(key);
        return sessionValue;
    }

    public async Task SetSessionValue(string key, string value, ICookieService cookieService)
    {
        await cookieService.SetCookieByNameAndValue(key, value, 7);
    }
}