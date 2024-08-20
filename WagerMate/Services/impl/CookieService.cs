using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace WagerMate.Services.impl;

public class CookieService : ICookieService
{
    private readonly IJSRuntime _jsRuntime;

    public CookieService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }


    public ValueTask<string> GetCookieByName(string name)
    {
        return  _jsRuntime.InvokeAsync<string>("GetCookie", name);
    }

    public ValueTask SetCookieByNameAndValue(string name, string value, int expirationDate)
    {
        return  _jsRuntime.InvokeVoidAsync("SetCookie", name, value, expirationDate);
    }

    public ValueTask DeleteCookie(string name)
    {
        return _jsRuntime.InvokeVoidAsync("DeleteCookie", name);
    }
    
    public async Task RedirectToLogin(string key, NavigationManager navigation)
    {
        var value = await GetCookieByName(key);
        if (string.IsNullOrEmpty(value))
        {
            navigation.NavigateTo("/login");
        }
    }
}