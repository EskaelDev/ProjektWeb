using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjektWeb.Data.Models.Database;
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
    public class ElementController : BaseController
    {
        private IElementService _elementService;
        public ElementController(IUserService userService, IHttpContextAccessor httpContextAccessor, IElementService elementService) : base(userService, httpContextAccessor)
        {
            _elementService = elementService;
        }

        [AllowAnonymous]
        [HttpGet("all/{page}")]
        public async Task<ActionResult<List<Element>>> GetAllElements(int? page)
        {
            var result = await _elementService.GetElements(page);
            if (result.Count > 0)
                return Ok(result);
            else
                return NoContent();
        }


        [HttpPost("save")]
        public async Task<ActionResult<Element>> SaveElement([FromBody] ElementViewModel newElement)
        {
            if (GetCurrentUser().Result.Role != Data.Entities.UserRoles.admin)
                return Unauthorized();

            var element = new Element { Title = newElement.Title};
            var result = await _elementService.SaveElement(element);

            return Ok(result);
                        
        }

    }
}
