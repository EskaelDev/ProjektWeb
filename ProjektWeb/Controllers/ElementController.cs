﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<List<Element>>> GetAll(int? page)
        {
            var result = await _elementService.GetMany(page);
            if (result.Count <= 0)
                return NoContent();
            else
                return Ok(result);
        }


        [HttpPost("save")]
        public async Task<ActionResult<Element>> Post([FromBody] ElementViewModel newElement)
        {
            if (!_usersService.IsAdmin())
                return Unauthorized();

            var result = await _elementService.Create(newElement);

            return Ok(result);
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<List<Element>>> Get(int? id)
        {
            if (id.HasValue)
            {
                var result = await _elementService.Get(id.Value);
                if (result == null)
                    return Ok(result);
            }
            return NoContent();
        }

        [HttpDelete("/{id}")]
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

        [HttpPut("/{id}")]
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
    }
}