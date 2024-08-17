using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace WagerMate.Services.impl;

public class SessionStorage : ISessionStorage
{
    public async Task HandleSession(string key, string data, ISessionStorageService sessionStorage)
    {
        await sessionStorage.SetItemAsync(key, data);
    }

    public async Task RedirectToLogin(string key, ISessionStorageService sessionStorage, NavigationManager navigation)
    {
        var value = await sessionStorage.GetItemAsync<string>(key);
        if (string.IsNullOrEmpty(value))
        {
            navigation.NavigateTo("/login");
        }
    }
}