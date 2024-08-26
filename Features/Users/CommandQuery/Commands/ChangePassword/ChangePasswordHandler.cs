using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Repositories;
using Auth.Shared.CustomErrors;
using Auth.Shared.Extensions;
using MediatR;
using ErrorOr;

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
            ChangePasswordRequest request = command.MapToRequest(_hashService);

            bool isSuccess = await _userRepository.ChangePasswordAsync(request, ct);

            if (!isSuccess)
            {
                return UserErrors.NotFound();
            }

            return Result.Updated;
        }
    }
}