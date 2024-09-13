using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Mappings;

namespace Auth.Features.Users.CommandQuery.Commands.Create
{
    public class CreateHandler(IOrganizationRepository userRepository,
        IHashService hashService) :
        IRequestHandler<CreateCommand, ErrorOr<Created>>
    {
        private readonly IOrganizationRepository _userRepository = userRepository;
        private readonly IHashService _hashService = hashService;

        public async Task<ErrorOr<Created>> Handle(CreateCommand command, CancellationToken ct)
        {
            Option<Tuple<string, string>> existingUser = await _userRepository.FindAsync(
                expression: u => u.Username == command.Username
                    || u.Phone == command.Phone,
                selector: u => Tuple.Create(u.Username, u.Phone), ct);

            if (existingUser.IsSome)
            {
                List<Error> errors = [];
                Tuple<string, string> existUser = existingUser.ValueUnsafe();

                if (existUser.Item1 == command.Username)
                {
                    errors.Add(UserErrors.DuplicateUsername(command.Username));
                }

                if (existUser.Item2 == command.Phone)
                {
                    errors.Add(UserErrors.DuplicatePhone(command.Phone));
                }

                return errors;
            }

            User user = command.MapToEntity(_hashService);
            await _userRepository.AddAsync(user, ct);

            return Result.Created;
        }
    }
}