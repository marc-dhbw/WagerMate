namespace WagerMate.Services;

public interface ICookieService
{
    ValueTask<string> GetCookieByName(string name);
    ValueTask SetCookieByNameAndValue(string name, string value, int expirationDate);
    ValueTask DeleteCookie(string name);
}