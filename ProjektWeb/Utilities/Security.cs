using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjektWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWeb.Helpers
{
    public static class Security
    {
        private static JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        private static readonly int _saltSize = 16;
        public static string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password: password,
                                                                salt: salt,
                                                                prf: KeyDerivationPrf.HMACSHA512,
                                                                iterationCount: 10000,
                                                                numBytesRequested: 32));
        }

        public static string CreateUserToken(User user, AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return _tokenHandler.WriteToken(token);
        }

        public static byte[] CreateSalt()
        {
            Random rnd = new Random();
            byte[] b = new Byte[_saltSize];
            rnd.NextBytes(b);
            return b;
        }
    }
}
