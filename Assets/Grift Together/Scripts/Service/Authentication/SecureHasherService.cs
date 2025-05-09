using System.Collections;
using System.Security.Cryptography;

namespace GriftTogether {

    public class SecureHasherService {
        public void HashPassword(string password, int iterations, out byte[] hash, out byte[] salt) {

            using var rng = new RNGCryptoServiceProvider();
            salt = new byte[16];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            hash = pbkdf2.GetBytes(32);
        }

        public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt, int iterations) {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, iterations, HashAlgorithmName.SHA256);
            var testHash = pbkdf2.GetBytes(32);
            return StructuralComparisons.StructuralEqualityComparer.Equals(testHash, storedHash);
        }
    }
}
