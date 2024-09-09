using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Shared.Extensions;
using System.Linq.Expressions;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Mappings;

namespace Auth.Features.Users.CommandQuery.Commands.UpdateProfile
{
    public class UpdateProfileHandler(
        IUserRepository userRepository,
        IUserClaimsInfo userClaimsInfo)
        : IRequestHandler<UpdateProfileCommand, ErrorOr<Updated>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;

        private Expression<Func<User, bool>>? GetExpression(
            UpdateProfileCommand command)
        {
            Expression<Func<User, bool>>? expression = default;

            if (command.Username != _userClaimsInfo.UserInfo?.Username)
            {
                expression = u => u.Username == command.Username;
            }

            if (command.Phone != _userClaimsInfo.UserInfo?.Phone)
            {
                if (expression != null)
                {
                    expression.OrElse(u => u.Phone == command.Phone);
                }
                else
                {
                    expression = u => u.Phone == command.Phone;
                }
            }

            if (command.Email != _userClaimsInfo.UserInfo?.Email)
            {
                if (expression != null)
                {
                    expression?.OrElse(u => u.Email == command.Email);
                }
                else
                {
                    expression = u => u.Email == command.Email;
                }
            }

            return expression;
        }

        private async Task<ErrorOr<bool>> ValidateData(
            UpdateProfileCommand command, CancellationToken ct)
        {
            Expression<Func<User, bool>>? expression =
                GetExpression(command);

            if (expression == null)
            {
                return true;
            }

            Option<Tuple<string, string, string>> existingUser =
             await _userRepository.FindAsync(
                expression,
                selector: u => Tuple.Create(u.Username, u.Phone, u.Email), ct);

            if (existingUser.IsNone)
            {
                return true;
            }

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
            UpdateProfileCommand command, CancellationToken ct)
        {
            ErrorOr<bool> validateData = await ValidateData(command, ct);

            return await validateData.MatchAsync<ErrorOr<Updated>>(
                async value =>
                {
                    UpdateRequest request = command
                        .MapToRequest(_userClaimsInfo.UserInfo?.Id ?? Ulid.NewUlid());

                    await _userRepository.UpdateAsync(request, ct);

                    return Result.Updated;
                },
                async errors => await Task.FromResult(errors)
           );
        }
    }
}