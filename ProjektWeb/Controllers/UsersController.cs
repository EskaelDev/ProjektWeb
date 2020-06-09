using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjektWeb.Data.Entities;
using ProjektWeb.Data.Models;
using ProjektWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : BaseController
    {

        public UsersController(IUserService userService, IHttpContextAccessor httpContextAccessor) : base(userService, httpContextAccessor)
        {
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<User>> Authenticate([FromBody]AuthenticateViewModel model)
        {
            var user = await _usersService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        //[HttpGet]
        //public IActionResult GetAll([FromHeader] string Authorization)
        //{
        //    var users = UsersService.GetAll();
        //    var user = GetCurrentUser();
        //    return Ok(users);
        //}

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody]RegisterModel model)
        {
            var user = new User { 
                Name = model.Name,
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                Password = model.Password
            };

            await _usersService.Register(user);

            return Ok(user);
        }
    }
}
