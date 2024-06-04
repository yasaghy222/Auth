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

        private User? Find(string Username) => Db.Users.FirstOrDefault(i => i.Username == Username);

        public async Task<Result> CreateUser(CreateUserDto dto)
        {
            try
            {
                if (HasValidOrganization(dto.OrganizationId))
                    return CustomErrors.OrganizationNotFound();

                User? found = Find(dto.Username);
                if (found is not null)
                {
                    return CustomErrors.UsernameAlreadyExist(dto.Username, 200);
                }
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
                var found = Db.Users.FirstOrDefault(i => i.Username == dto.Username && i.OrganizationId == dto.OrganizationId);
                if (found is null)
                    return CustomErrors.InvalidUsernameOrPassword();

                var passIsValid = SecretHasher.Verify(dto.Password, found.Password);
                if (!passIsValid)
                    return CustomErrors.InvalidUsernameOrPassword();

                string token = Jwt.Generate(found);

                return CustomResults.TokenGenerated(token);
            }
            catch (Exception ex)
            {
                return CustomErrors.GenerateTokenFailed();
            }

        }
    }
}
