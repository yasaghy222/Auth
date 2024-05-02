using Authenticate.Common;
using Authenticate.Context;
using Authenticate.Entities;
using Authenticate.Interfaces;
using Authenticate.Models;
using AuthenticateDTOs;
using FluentValidation;

namespace Authenticate.Services
{
    public class UserService(ToolsService tools, IValidator<UserDto> validator)
    {
        private AuthenticateContextDb Db { get; } = tools.Db;
        private IJWTProvider Jwt { get; } = tools.Jwt;
        public IValidator<UserDto> DataValidator { get; private set; } = validator;

        public async Task<Result> CreateUser(UserDto dto)
        {
            try
            {
                var check = DataValidator.Validate(dto);
                if (!check.IsValid)
                {
                    return CustomErrors.InvalidUserData(check.Errors);
                }

                var founded = Db.Users.FirstOrDefault(i => i.Username == dto.Username);
                if (founded != null)
                    return CustomErrors.UsernameAlreadyExist(dto.Username);

                var user = new User
                {
                    Username = dto.Username,
                    Password = SecretHasher.Hash(dto.Password)
                };
                Db.Add(user);
                await Db.SaveChangesAsync();

                return CustomResults.UserCreated(user.Id);
            }
            catch (Exception ex)
            {
                return CustomErrors.CreateUserFailed();
            }
        }

        public Result GenerateToken(UserDto dto)
        {
            try
            {
                var check = DataValidator.Validate(dto);
                if (!check.IsValid)
                    return CustomErrors.InvalidUserData(check.Errors);

                var founded = Db.Users.FirstOrDefault(i => i.Username == dto.Username);
                if (founded is null)
                    return CustomErrors.InvalidUsernameOrPassword();

                var passIsValid = SecretHasher.Verify(dto.Password, founded.Password);
                if (!passIsValid)
                    return CustomErrors.InvalidUsernameOrPassword();

                string token = Jwt.Generate(founded);

                return CustomResults.TokenGenerated(token);
            }
            catch (Exception ex)
            {
                return CustomErrors.GenerateTokenFailed();
            }

        }
    }
}
