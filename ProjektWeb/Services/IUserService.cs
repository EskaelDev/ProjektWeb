using ProjektWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string normalizedEmail, string password);
        Task<User> Register(User newUser);

        Task<User> GetById(int id);

        bool IsAdmin();
        int GetCurrentUserId();
    }
}
