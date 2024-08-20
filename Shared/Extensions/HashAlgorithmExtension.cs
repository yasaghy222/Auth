using System.Security.Cryptography;

namespace Auth.Shared.Extensions
{
    public static class HashAlgorithmExtension
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA3_512;

        public static string ToHashString(this string str)
        {
            byte[] salt = RandomNumberGenerator.GetBytes();
            RandomNumberGenerator.Fill(salt);

            using Rfc2898DeriveBytes pbkdf2 = new(str, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(KeySize);
            byte[] hash = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hash, 0, SaltSize);
            Array.Copy(key, 0, hash, SaltSize, KeySize);

            return Convert.ToBase64String(hash);
        }

        public static bool VerifyHashString(this string hashedStr, string str)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedStr);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using Rfc2898DeriveBytes pbkdf2 = new(str, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(KeySize);
            for (int i = 0; i < KeySize; i++)
            {
                if (hashBytes[i + SaltSize] != key[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}