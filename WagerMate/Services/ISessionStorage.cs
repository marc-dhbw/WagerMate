using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace WagerMate.Services;

public interface ISessionStorage
{
    public Task RedirectToLogin(string key, ISessionStorageService sessionStorage, NavigationManager navigation);
    public Task SetSessionValue(string key, string value, ISessionStorageService sessionStorage);
    public ValueTask<string> GetSessionValue(string key, ISessionStorageService sessionStorage);
}