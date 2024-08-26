namespace WagerMate.Services.auth;

public interface IHashService
{
    public string CreateHash(string email, string pw);
}