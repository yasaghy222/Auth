using System.Security.Cryptography;

namespace Auth.Shared.Extensions
{
    public interface IHashService
    {
        string HashString(string str);
        bool VerifyHashString(string hashedStr, string str);
    }

    public class HashService : IHashService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public string HashString(string str)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(str, salt, Iterations, Algorithm, HashSize);

            // Combine salt and hash for storage or transmission
            return Convert.ToBase64String(salt.Concat(hash).ToArray());
        }

        public bool VerifyHashString(string hashedStr, string str)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedStr);

            byte[] salt = hashBytes[..SaltSize];
            byte[] hash = hashBytes[SaltSize..];

            byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(str, salt, Iterations, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
        }
    }
}
