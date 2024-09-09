using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Mappings;

namespace Auth.Features.Users.CommandQuery.Commands.Update
{
    public class UpdateHandler(
        IUserRepository userRepository)
        : IRequestHandler<UpdateCommand, ErrorOr<Updated>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        private async Task<ErrorOr<bool>> ValidateData(
            UpdateCommand command, CancellationToken ct)
        {
            Option<Tuple<string, string, string>> existingUser =
             await _userRepository.FindAsync(
                 u => u.Username == command.Username ||
                    u.Phone == command.Phone ||
                    u.Email == command.Email,
                selector: u => Tuple.Create(u.Username, u.Phone, u.Email), ct);

            List<Error> errors = [];
            Tuple<string, string, string> existUser = existingUser.ValueUnsafe();

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

        public async Task<ErrorOr<Updated>> Handle(
            UpdateCommand command, CancellationToken ct)
        {
            ErrorOr<bool> validateData = await ValidateData(command, ct);

            return await validateData.MatchAsync<ErrorOr<Updated>>(
                async value =>
                {
                    UpdateRequest request = command.MapToRequest();
                    await _userRepository.UpdateAsync(request, ct);

                    return Result.Updated;
                },
                async errors => await Task.FromResult(errors)
           );
        }
    }
}