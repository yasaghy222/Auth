using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Mappings;

namespace Auth.Features.Users.CommandQuery.Commands.ChangePassword
{
    public class ChangePasswordHandler(
        IUserRepository userRepository,
        IHashService hashService)
        : IRequestHandler<ChangePasswordCommand, ErrorOr<Updated>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHashService _hashService = hashService;

        public async Task<ErrorOr<Updated>> Handle(
            ChangePasswordCommand command, CancellationToken ct)
        {

            Option<string> getOldPassword = await _userRepository.FindAsync(
                expression: i => i.Id == command.Id
                    && i.Status == UserStatus.Active,
                selector: i => i.Password,
                ct);

            if (getOldPassword.IsNone)
                return UserErrors.NotFound();

            string oldPassword = getOldPassword.ValueUnsafe();

            if (!_hashService.VerifyHashString(oldPassword, command.OldPassword))
                return UserErrors.NotFound();

            if (_hashService.VerifyHashString(oldPassword, command.Password))
                return UserErrors.RepeatedPassword();


            ChangePasswordRequest request = command.MapToRequest(_hashService);

            bool isSuccess = await _userRepository
                .ChangePasswordAsync(request, ct);

            if (!isSuccess)
            {
                return UserErrors.NotFound();
            }

            return Result.Updated;
        }
    }
}