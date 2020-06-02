using ProjektWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public interface IUserAuthService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Register(User newUser);
        
    }
}
