using System.Security.Cryptography;
using System.Text;
using WagerMate.Data;
using WagerMate.Services.auth;


namespace WagerMate.Services.impl;

public class HashService : IHashService
{
    public string CreateHash(string email, string pw)
    {
        string inputString = email + pw;
        StringBuilder sb = new StringBuilder();
        using (HashAlgorithm algorithm = SHA256.Create())
        {
            var hashbytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            foreach (byte b in hashbytes)
                sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }
}