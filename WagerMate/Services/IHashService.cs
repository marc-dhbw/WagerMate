using WagerMate.Data;
namespace WagerMate.Services;

public interface IHashService
{
    public byte[] GenerateSalt();
    public string HashPassword(string password, string username, byte[] salt);
    public string HashNewPassword(string password, string username);
    public bool ComparePassword(string username, string enteredPassword, User user);
}