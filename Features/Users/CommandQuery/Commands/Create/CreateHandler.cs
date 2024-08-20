using ErrorOr;
using MediatR;
using Auth.Domain.Entities;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Mappings;

namespace Auth.Features.Users.CommandQuery.Commands.Create
{
    public class CreateHandler(IUserRepository userRepository) :
        IRequestHandler<CreateCommand, ErrorOr<Created>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<Created>> Handle(CreateCommand command, CancellationToken ct)
        {
            bool isExist = await _userRepository
                .AnyAsync(i => i.Username == command.Username, ct);

            if (isExist)
            {
                return UserErrors.DuplicateUserName(command.Username);
            }

            User user = command.MapToEntity();
            await _userRepository.AddAsync(user, ct);

            return Result.Created;
        }
    }
}