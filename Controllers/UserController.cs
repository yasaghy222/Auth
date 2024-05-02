using Authenticate.Common;
using Authenticate.Context;
using Authenticate.Entities;
using Authenticate.Interfaces;
using Authenticate.Models;
using Authenticate.Services;
using AuthenticateDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [ApiController]
    [Route("[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserService Service;
        public UserController(IJWTProvider provider, AuthenticateContextDb db, IValidator<UserDto> validator)
        {
            var tools = new ToolsService(provider, db);
            Service = new UserService(tools, validator);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            var result = await Service.CreateUser(dto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        public IActionResult GenerateToken(UserDto dto)
        {
            var result = Service.GenerateToken(dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
