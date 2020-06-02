using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Helpers
{
    public static class Security
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA512.Create())
            {
                return BitConverter.ToString(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
