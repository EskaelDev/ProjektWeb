using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjektWeb.Data.Models.Database;
using ProjektWeb.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class ElementController : BaseController
    {
        private IElementService _elementService;
        private IRateService _rateService;
        private IImageTransferService _imageService;
        public ElementController(IUserService userService, IHttpContextAccessor httpContextAccessor, IElementService elementService, IRateService rateService, IImageTransferService imageTransferService) : base(userService, httpContextAccessor)
        {
            _elementService = elementService;
            _imageService = imageTransferService;
            _rateService = rateService;
        }

        [AllowAnonymous]
        [HttpGet("all/{page}/{pagesize}")]
        public async Task<ActionResult<List<Object>>> GetAll(int? page, int? pagesize)
        {
            var result = await _elementService.GetMany(page.GetValueOrDefault(1), pagesize.GetValueOrDefault(5));
            var count = await _elementService.GetCount();
            if (result.Count <= 0)
                return NoContent();
            else
                return Ok(new { movies = result, count = count });
        }

        [HttpPost("save")]
        public async Task<ActionResult<Element>> Post([FromBody] ElementViewModel newElement)
        {
            if (!_usersService.IsAdmin())
                return Unauthorized();

            var result = await _elementService.Create(newElement);

            if (result == null)
                return StatusCode(409, $"Element '{newElement.Title}' already exists.");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Element>>> Get(int? id)
        {
            if (id.HasValue)
            {
                var result = await _elementService.Get(id.Value);
                if (result != null)
                    return Ok(result);
            }
            return NoContent();
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<List<Element>>> GetDetails(int? id)
        {
            if (id.HasValue)
            {
                var result = await _elementService.Get(id.Value);
                if (result != null)
                {

                    var retObj = new
                    {
                        Element = result,
                        Rates = await _rateService.GetAllRates(id.Value)
                    };
                    return Ok(retObj);
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!_usersService.IsAdmin())
                return Unauthorized();

            if (id.HasValue)
            {
                var result = await _elementService.Delete(id.Value);
                if (result)
                    return Ok();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Element>>> Put(ElementViewModel updatedElement)
        {
            if (!_usersService.IsAdmin())
                return Unauthorized();

            if (updatedElement != null)
            {
                var result = await _elementService.Update(updatedElement);
                return Ok(result);
            }

            return NoContent();
        }

        [HttpPost("uploadfile")]
        public async Task<ActionResult<string>> UploadFile()
        {
            var result = new { path = await _imageService.SaveFile(HttpContext.Request.Form) };
            return Ok(result);
        }
    }
}
