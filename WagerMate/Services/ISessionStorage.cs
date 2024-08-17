using Blazored.SessionStorage;

namespace WagerMate.Services;

public interface ISessionStorage
{
    public Task HandleSession(string key, string data, ISessionStorageService sessionStorage);
}