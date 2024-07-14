using Authenticate.Common;
using Authenticate.Context;
using Authenticate.DTOs;
using Authenticate.Entities;
using Authenticate.Interfaces;
using Authenticate.Models;
using Mapster;

namespace Authenticate.Services
{
    public class UserService(ToolsService tools)
    {
        private AuthenticateContextDb Db { get => tools.Db; }
        private readonly SessionService SessionService = new(tools.Db);
        private IJWTProvider Jwt { get => tools.Jwt; }

        private bool HasValidOrganization(Guid OrganizationId)
        {
            Organization? organization = Db.Organizations.FirstOrDefault(i => i.Id == OrganizationId);
            return organization == null;
        }

        private User? Find(string Username) => Db.Users.FirstOrDefault(i => i.Username == Username);

        public async Task<Result> Add(AddUserDto dto)
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
                    Username = dto.Username ?? dto.Phone,
                    Email = dto.Email,
                    Phone = dto.Phone,
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

        public async Task<Result> GenerateToken(GenerateTokenDto dto)
        {
            User? found = Db.Users.FirstOrDefault(i => i.Username == dto.Username && i.OrganizationId == dto.OrganizationId);
            if (found is null)
                return CustomErrors.InvalidUsernameOrPassword();

            bool passIsValid = SecretHasher.Verify(dto.Password, found.Password);
            if (!passIsValid)
                return CustomErrors.InvalidUsernameOrPassword();

            SessionDto sessionDto = dto.Adapt<SessionDto>();
            sessionDto.UserId = found.Id;

            Session? session = await SessionService.Find(sessionDto);
            if (session != null)
                return CustomResults.TokenGenerated(session.Token);

            sessionDto.Token = Jwt.Generate(found);
            await SessionService.Add(sessionDto);

            return CustomResults.TokenGenerated(sessionDto.Token);
        }
    }
}
