using System.Security.Cryptography;
using System.Text;
using WagerMate.Data;


namespace WagerMate.Services.impl;

public class HashService : IHashService
{
    private readonly IUserService _userService;

    public byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }

    public string createhash(string email, string pw)
    {
        string inputString = email + pw;
        StringBuilder sb = new StringBuilder();
        using (HashAlgorithm algorithm = SHA256.Create())
        {
            var x = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            foreach (byte b in x)
                sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }

    public string HashPassword(string password, string email, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] emailBytes = Encoding.UTF8.GetBytes(email);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length + emailBytes.Length];

            Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);
            Buffer.BlockCopy(emailBytes, 0, saltedPassword, passwordBytes.Length + salt.Length, emailBytes.Length);

            byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

            for (int i = 0; i < 100; i++)
            {
                hashedBytes = sha256.ComputeHash(hashedBytes);
            }

            byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            return Convert.ToBase64String(hashedPasswordWithSalt);
        }
    }

    public string HashNewPassword(string password, string email)
    {
        byte[] salt = GenerateSalt();
        Console.WriteLine("Hash New Password" + Convert.ToBase64String(salt));
        return HashPassword(password, email, salt);
    }

    public bool ComparePassword(string email, string enteredPassword, User user)
    {
        byte[] existingPasswordBytes = Convert.FromBase64String(user.Password!);
        byte[] salt = new byte[16];

        Buffer.BlockCopy(existingPasswordBytes, 0, salt, 0, salt.Length);
        Console.WriteLine("ComparePassword:");
        Console.WriteLine("enteredPassword:" + enteredPassword + "user email:" + user.Email + "salt:" + salt);

        string generatedPassword = HashPassword(enteredPassword, user.Email!, salt);

        Console.WriteLine(generatedPassword);

        if (user.Password == generatedPassword)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}