using System.Security.Cryptography;
using System.Text;
using Common.Application.Settings;


namespace Utilities
{
    public static class PasswordUtil
    {
        //Use PBKDF2 algorithm for hashing
        public static string HashPBKDF2(string password, PasswordSetting passwordSetting, out byte[] salt, byte[]? existSalt = null)
        {
            salt = existSalt ?? RandomNumberGenerator.GetBytes(passwordSetting.KeySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                passwordSetting.Iterations,
                passwordSetting.HashAlgorithmName,
                passwordSetting.KeySize);
            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string hash, byte[] salt, PasswordSetting passwordSetting)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, passwordSetting.Iterations, passwordSetting.HashAlgorithmName, passwordSetting.KeySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
