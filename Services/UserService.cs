using Authenticate.Common;
using Authenticate.Context;
using Authenticate.Entities;
using Authenticate.Interfaces;
using Authenticate.Models;
using AuthenticateDTOs;
using FluentValidation;

namespace Authenticate.Services
{
    public class UserService(ToolsService tools)
    {
        private AuthenticateContextDb Db { get => tools.Db; }
        private IJWTProvider Jwt { get => tools.Jwt; }

        private bool HasValidOrganization(Guid OrganizationId)
        {
            Organization? organization = Db.Organizations.FirstOrDefault(i => i.Id == OrganizationId);
            return organization == null;
        }

        private bool IsExist(string Username)
        {
            User? founded = Db.Users.FirstOrDefault(i => i.Username == Username);
            return founded != null;
        }

        public async Task<Result> CreateUser(CreateUserDto dto)
        {
            try
            {
                if (HasValidOrganization(dto.OrganizationId))
                    return CustomErrors.OrganizationNotFound();

                if (IsExist(dto.Username))
                    return CustomErrors.UsernameAlreadyExist(dto.Username);

                User user = new()
                {
                    OrganizationId = dto.OrganizationId,
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
