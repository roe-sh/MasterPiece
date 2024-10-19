namespace LoveSeedM.DTOs
{
    public class PasswordHelper
    {
        public static (byte[] hash, byte[] salt) HashPassword(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var salt = hmac.Key;  // Generate a new salt
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (hash, salt);
            }
        }

        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);  // Compare the hash
            }
        }
    }

}
