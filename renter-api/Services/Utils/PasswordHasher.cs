using System;
using System.Security.Cryptography;
using System.Text;

namespace Services.Utils
{
    public static class PasswordHasher
    {
        private static readonly string _salt = "P&0myWHqC32.";

        public static string CalculateHashedPassword(string clearpwd)
        {
            using (var sha = SHA256.Create())
            {
                var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(clearpwd + _salt));
                return Convert.ToBase64String(computedHash);
            }
        }
    }
}