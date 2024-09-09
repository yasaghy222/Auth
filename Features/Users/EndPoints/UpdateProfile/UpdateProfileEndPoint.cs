using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.EndPoints.Update;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.CommandQuery.Commands.UpdateProfile;

namespace Auth.Features.Users.EndPoints.UpdateProfile
{
    public class UpdateProfileEndPoint(
        ISender sender,
        IUserClaimsInfo userClaimsInfo,
        ILogger<UpdateProfileEndPoint> logger)
        : Endpoint<UserUpdateProfileDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;
        private readonly ILogger<UpdateProfileEndPoint> _logger = logger;

        public override void Configure()
        {
            Put(UserConstes.Update_Profile_Resource_Url);
            Description(b => b
                .Accepts<UserUpdateProfileDto>("application/json")
                .Produces(200)
                .Produces(401)
                .ProducesProblemFE(400)
                .ProducesProblemFE(500));
        }

        public override async Task<IResult> ExecuteAsync(UserUpdateProfileDto dto, CancellationToken ct)
        {
            UpdateProfileCommand command = dto.MapToCommand(_userClaimsInfo.UserInfo);
            _logger.LogInformation("Command: {command}", command.ToJson());

            ErrorOr<Updated> result = await _sender.Send(command, ct);

            return result.Match(
                created => Results.Ok(),
                errors =>
                {
                    _logger.LogWarning(UserErrors.UpdateLogMsg, errors);
                    return result.ToProblemDetails();
                }
            );
        }

    }
}