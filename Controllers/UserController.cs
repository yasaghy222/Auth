using Authenticate.Common;
using Authenticate.Common.CustomResponse;
using Authenticate.Context;
using Authenticate.DTOs;
using Authenticate.Interfaces;
using Authenticate.Models;
using Authenticate.Services;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Authenticate.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserService Service;
        private readonly SessionService SessionService;
        private readonly IValidator<GenerateTokenDto> SignInValidator;
        private readonly IValidator<AddUserDto> SignUpValidator;

        public UserController(IJWTProvider provider, AuthenticateContextDb db, IValidator<GenerateTokenDto> signInValidator, IValidator<AddUserDto> signUpValidator)
        {
            ToolsService tools = new(provider, db);
            Service = new UserService(tools);
            SessionService = new SessionService(tools.Db);
            SignUpValidator = signUpValidator;
            SignInValidator = signInValidator;
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddUserDto dto)
        {
            Result result;
            ValidationResult check = SignUpValidator.Validate(dto);

            if (!check.IsValid)
            {
                result = CustomErrors.InvalidUserData(check.Errors.Adapt<List<ValidationError>>());
                return StatusCode(result.StatusCode, result);
            }

            result = await Service.Add(dto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        public async Task<IActionResult> GenerateToken(GenerateTokenDto dto)
        {
            Result result;
            try
            {
                ValidationResult check = SignInValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidUserData(check.Errors.Adapt<List<ValidationError>>());
                    return StatusCode(result.StatusCode, result);
                }

                dto.IP = HttpContext.Connection.RemoteIpAddress?.ToString();

                result = await Service.GenerateToken(dto);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception e)
            {
                result = CustomErrors.InternalServer("Error when Generating Token");
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DestroyToken(string token)
        {
            Result result = new();
            try
            {
                bool isOk = await SessionService.Destroy(token);
                result = isOk ? CustomResults.TokenDestroyed() : CustomErrors.InvalidUserData(token);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception e)
            {
                result = CustomErrors.InternalServer("Error when Destroyed Token");
                return StatusCode(result.StatusCode, result);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CheckToken(string token)
        {
            Result result = new();
            try
            {
                bool isOk = await SessionService.Any(token);
                result = isOk ? CustomResults.TokenValid() : CustomErrors.InvalidUserData(token);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception e)
            {
                result = CustomErrors.InternalServer("Error when Checking Token");
                return StatusCode(result.StatusCode, result);
            }
        }


    }
}
