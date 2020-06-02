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

    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        private IDatabaseService _databaseService;

        public UserService(IOptions<AppSettings> appSettings, IDatabaseService context)
        {
            _appSettings = appSettings.Value;
            _databaseService = context;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _databaseService.GetUserByEmail(email).FirstOrDefaultAsync();

            if (user == null)
                return null;

    
            string typedPassword = Security.HashPassword(password, user.Salt);
            string userPassword = Security.HashPassword(user.Password, user.Salt);
            if (typedPassword != userPassword)
                return null;

            user.Token = Security.CreateUserToken(user, _appSettings);

            return user;
        }

        public async Task<User> GetById(int id)
        {
            return await _databaseService.GetUserById(id).FirstOrDefaultAsync();
        }

        public async Task<User> Register(User newUser)
        {
            newUser.Salt = Security.CreateSalt();
            newUser.Password = Security.HashPassword(newUser.Password, newUser.Salt);
            newUser.Role = UserRoles.user;
            return await _databaseService.AddUser(newUser).FirstOrDefaultAsync();
        }

    }
}
