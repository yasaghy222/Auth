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
        private readonly IValidator<UserDto> SignInValidator;
        private readonly IValidator<CreateUserDto> SignUpValidator;

        public UserController(IJWTProvider provider, AuthenticateContextDb db, IValidator<UserDto> signInValidator, IValidator<CreateUserDto> signUpValidator)
        {
            var tools = new ToolsService(provider, db);
            Service = new UserService(tools);
            SignUpValidator = signUpValidator;
            SignInValidator = signInValidator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            _ = new Result();
            var check = SignUpValidator.Validate(dto);
            Result result;
            if (!check.IsValid)
            {
                result = CustomErrors.InvalidUserData(check.Errors);
                return StatusCode(result.StatusCode, result);
            }

            result = await Service.CreateUser(dto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        public IActionResult GenerateToken(UserDto dto)
        {
            _ = new Result();
            var check = SignInValidator.Validate(dto);
            Result result;
            if (!check.IsValid)
            {
                result = CustomErrors.InvalidUserData(check.Errors);
                return StatusCode(result.StatusCode, result);
            }

            result = Service.GenerateToken(dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
