using System.Security.Cryptography;
using System.Text;

namespace CineScope.Utils;

public class CryptoUtils
{
    private const int SaltLength = 16;
    
    public static string HashPassword(string password)
    {
        var salt = GenerateSalt();

        return HashPassword(password, salt);
    }

    public static string HashPassword(string password, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = new byte[passwordBytes.Length + salt.Length];

        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

        var hashedBytes = SHA256.HashData(saltedPassword);

        var hashedPasswordWithSalt = new byte[salt.Length + hashedBytes.Length];
        Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
        Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

        return Convert.ToBase64String(hashedPasswordWithSalt);
    }

    public static bool VerifyHashedPassword(string password, string hashedPasswordWithSalt)
    {
        var hashedPasswordBytes = Convert.FromBase64String(hashedPasswordWithSalt);
        var salt = hashedPasswordBytes.Take(SaltLength).ToArray();

        return hashedPasswordWithSalt.Equals(HashPassword(password, salt));
    }

    private static byte[] GenerateSalt()
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltLength];
        rng.GetBytes(salt);
        return salt;
    }
}