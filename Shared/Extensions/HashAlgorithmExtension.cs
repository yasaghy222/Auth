using System.Security.Cryptography;

namespace Auth.Shared.Extensions
{
    public static class HashAlgorithmExtension
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA3_512;

        public static string ToHashString(this string str)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(str, salt, Iterations, Algorithm, HashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public static bool VerifyHashString(this string hashedStr, string str)
        {
            string[] parts = hashedStr.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(str, salt, Iterations, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}