using WagerMate.Data;
namespace WagerMate.Services;

public interface IHashService
{
    public string CreateHash(string email, string pw);
}