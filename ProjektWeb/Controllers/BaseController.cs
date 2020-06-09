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
    public abstract class BaseController : ControllerBase
    {
        protected IUserService _usersService { get; private set; }
        protected IHttpContextAccessor _httpContextAccessor { get; private set; }

        public BaseController(IUserService usersService, IHttpContextAccessor httpContextAccessor)
        {
            _usersService = usersService;
            _httpContextAccessor = httpContextAccessor;
        }


    }
}
