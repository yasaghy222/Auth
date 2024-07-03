using System.Security.Cryptography;

namespace Authenticate.Common
{
    public static class SecretHasher
    {
        private static readonly int _saltSize = 16; // اندازه salt به بایت
        private static readonly int _iterations = 50000; // تعداد تکرارها
        private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256; // الگوریتم
        private static readonly int _keySize = 32; // اندازه کلید به بایت (برای SHA-256 برابر 32 است)
        private static readonly char segmentDelimiter = ':'; // جداکننده بخش‌ها

        public static string Hash(string input)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                _iterations,
                _algorithm,
                _keySize
            );
            return string.Join(
                segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                _iterations,
                _algorithm.Name
            );
        }

        public static bool Verify(string input, string hashString, char segmentDelimiter = ':')
        {
            try
            {
                string[] segments = hashString.Split(segmentDelimiter);

                // بررسی تعداد بخش‌ها
                if (segments.Length != 4)
                {
                    throw new ArgumentException("Invalid hashString format.");
                }

                byte[] hash = Convert.FromHexString(segments[0]);
                byte[] salt = Convert.FromHexString(segments[1]);

                // بررسی طول هش و salt
                if (hash.Length == 0 || salt.Length == 0)
                {
                    throw new ArgumentException("Invalid hash or salt length.");
                }

                if (!int.TryParse(segments[2], out int iterations))
                {
                    throw new ArgumentException("Invalid iterations count.");
                }

                HashAlgorithmName algorithm = new(segments[3]);

                byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                    input,
                    salt,
                    iterations,
                    algorithm,
                    hash.Length
                );

                return CryptographicOperations.FixedTimeEquals(inputHash, hash);
            }
            catch (Exception ex)
            {
                // اینجا می‌توانید خطا را مدیریت کنید یا لاگ بگیرید
                Console.WriteLine($"Verification failed: {ex.Message}");
                return false;
            }
        }
    }
}