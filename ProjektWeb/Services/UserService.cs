using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.Threading.Tasks;
using ProjektWeb.Data.Entities;
using ProjektWeb.Helpers;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace ProjektWeb.Services
{

    public class UserService : IUserAuthService
    {
        //// users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<User> _users = new List<User>
        //{
        //    new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" },
        //    new User { Id = 66, FirstName = "Admin", LastName = "Admin", Username = "admin", Password = "admin" , Email="admin@admin.admin"}
        //};

        private readonly AppSettings _appSettings;
        private IDatabaseService _context;

        public UserService(IOptions<AppSettings> appSettings, IDatabaseService context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<User> Authenticate(string email, string password)
        {

            var user = await _context.AuthenticateUser(email, password).FirstOrDefaultAsync();

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful - generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public async Task<User> Register(User newUser)
        {
            newUser.Password = Security.HashPassword(newUser.Password);

            return await _context.AddUser(newUser).FirstOrDefaultAsync();
        }

    }
}
