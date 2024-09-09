using ErrorOr;
using MediatR;
using FastEndpoints;
using Auth.Shared.Constes;
using Auth.Shared.Extensions;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.CommandQuery.Commands.Update;

namespace Auth.Features.Users.EndPoints.Update
{
    public class UpdateEndPoint(
        ISender sender,
        IUserClaimsInfo userClaimsInfo,
        ILogger<UpdateEndPoint> logger)
        : Endpoint<UserUpdateDto, IResult>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<UpdateEndPoint> _logger = logger;
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;

        public override void Configure()
        {
            Put(UserConstes.Update_Resource_Url);
            Permissions(UserConstes.Update_Permission_Id);
            Description(b => b
                .Accepts<UserUpdateDto>("application/json")
                .Produces(200)
                .Produces(401)
                .Produces(403)
                .ProducesProblemFE(400)
                .ProducesProblemFE(500));
        }

        public override async Task<IResult> ExecuteAsync(UserUpdateDto dto, CancellationToken ct)
        {
            UpdateCommand command = dto.MapToCommand(_userClaimsInfo.UserInfo);
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