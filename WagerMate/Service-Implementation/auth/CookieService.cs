using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WagerMate.Services.auth;
using WagerMate.Services.user;

namespace WagerMate.Service_Implementation.auth;

public class CookieService : ICookieService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IUserService _userService;

    public CookieService(IJSRuntime jsRuntime, IUserService userService)
    {
        _jsRuntime = jsRuntime;
        _userService = userService;
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
        if (!_userService.DoesUserPasswordExist(value))
        {
            navigation.NavigateTo("/login");
        }
    }
}