using Blazored.SessionStorage;

namespace WagerMate.Services.impl;

public class SessionStorage : ISessionStorage
{
    public async Task HandleSession(string key, string data, ISessionStorageService sessionStorage)
    {
        await sessionStorage.SetItemAsync(key, data);
    }
}