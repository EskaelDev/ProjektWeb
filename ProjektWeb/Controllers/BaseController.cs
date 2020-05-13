using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjektWeb.Data.Entities;
using ProjektWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjektWeb.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IUserService UsersService { get; private set; }
        protected IHttpContextAccessor HttpContextAccessor { get; private set; }

        public BaseController(IUserService usersService, IHttpContextAccessor httpContextAccessor)
        {
            UsersService = usersService;
            HttpContextAccessor = httpContextAccessor;
        }

        protected async Task<User> GetCurrentUser()
        {
            var userId = HttpContextAccessor.HttpContext.User.Identity.Name;
            return await UsersService.FindById(userId);
        }
    }
}
