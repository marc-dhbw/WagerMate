using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace WagerMate.Services.impl;

public class SessionStorage : ISessionStorage
{
    public async Task RedirectToLogin(string key, ISessionStorageService sessionStorage, NavigationManager navigation)
    {
        var value = await sessionStorage.GetItemAsync<string>(key);
        if (string.IsNullOrEmpty(value))
        {
            navigation.NavigateTo("/login");
        }
    }

    public ValueTask<string> GetSessionValue(string key, ISessionStorageService sessionStorage)
    {
        ValueTask<string> sessionValue = sessionStorage.GetItemAsync<string>(key);
        return sessionValue;
    }

    public async Task SetSessionValue(string key, string value, ISessionStorageService sessionStorage)
    {
        await sessionStorage.SetItemAsync(key, value);
    }
}