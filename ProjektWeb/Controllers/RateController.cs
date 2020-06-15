using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjektWeb.Data.Models;
using ProjektWeb.Data.Models.Database;
using ProjektWeb.Services;

namespace ProjektWeb.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    [ApiController]
    public class RateController : BaseController
    {
        private IRateService _rateService;
        public RateController(IUserService userService, IHttpContextAccessor httpContextAccessor, IRateService rateService) : base(userService, httpContextAccessor)
        {
            _rateService = rateService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<Rate>> Post([FromBody] RateViewModel newRate)
        {
            if (newRate != null) { 
                var result = await _rateService.Create(newRate);

                if (result == null)
                {
                    result = await _rateService.Update(newRate);
                    if (result != null)
                        return Ok(result);
                }
                else { 
                    return Ok(result);
                }
            }

            return StatusCode(409, $"Rate for this element already exists.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Element>>> Get(int? id)
        {
            if (id.HasValue)
            {
                var result = await _rateService.Get(id.Value);
                if (result != null)
                    return Ok(result);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var result = await _rateService.Delete(id.Value);
                if (result != null)
                    return Ok();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Element>>> Put(RateViewModel rateViewModel)
        {
            if (rateViewModel != null)
            {
                var result = await _rateService.Update(rateViewModel);
                if(result != null)
                    return Ok(result);
            }

            return NoContent();
        }
    }
}