using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Utils
{
    class UserUtils
    {
        /// <summary>
        /// Generate SHA512 hash for given string
        /// </summary>
        /// <param name="password"> string (preferrably password) to hash</param>
        /// <returns>String representation of hash.</returns>
        public static string GenerateSHA512(string password)
        {
            SHA512 shaM = SHA512.Create();
            byte[] data = Encoding.UTF8.GetBytes(password);
            byte[] result = shaM.ComputeHash(data);
            return BitConverter.ToString(result);
        }
    }
}
