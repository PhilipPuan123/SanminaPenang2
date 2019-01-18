using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace RVISDev
{
    internal class Security
    {
        /// <summary>
        /// Generate salt with prefix value and user id.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static byte[] GenerateSalt(string prefix, string userId)
        {
            return Encoding.ASCII.GetBytes(prefix + userId);
        }

        /// <summary>
        /// Generate Hash in bytes.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }

        /// <summary>
        /// Generate Hash in string.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateHashString(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return Convert.ToBase64String(deriveBytes.GetBytes(length));
            }
        }
    }
}
