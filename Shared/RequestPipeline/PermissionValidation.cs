using FastEndpoints;
using System.Security.Claims;
using Auth.Shared.Extensions;
using Auth.Features.Users.Contracts.Enums;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Auth.Shared.RequestPipeline
{
    public class PermissionValidation<TRequest> : IPreProcessor<TRequest>
    {

        private string[] _requiredPermissions = [];

        private void ReadAttribute(IPreProcessorContext<TRequest> context)
        {
            Endpoint? endpoint = context.HttpContext.GetEndpoint();

            if (endpoint != null)
            {



                ActionDescriptor? actionDescriptor = endpoint
                    .Metadata.GetMetadata<ActionDescriptor>();

                _requiredPermissions = [];

                // if (actionDescriptor != null)
                // {
                //     if (actionDescriptor..GetCustomAttributes(typeof(PermissionsAttribute), false)
                //                         .FirstOrDefault() is PermissionsAttribute customAttribute)
                //     {
                //         _requiredPermissions = customAttribute._requiredPermissions;
                //     }
                // }
            }
        }

        public Task PreProcessAsync(IPreProcessorContext<TRequest> context, CancellationToken ct)
        {
            ReadAttribute(context);

            ClaimsPrincipal user = context.HttpContext.User;
            if (!user.Identity?.IsAuthenticated ?? false)
            {
                return context.HttpContext.Response.SendForbiddenAsync(cancellation: ct);
            }

            Claim? userPermissionsClaim = user.Claims
                .FirstOrDefault(i => i.Type == UserClaimNames.UserPermissions);

            if (userPermissionsClaim == null)
            {
                return context.HttpContext.Response.SendForbiddenAsync(cancellation: ct);
            }

            IEnumerable<string> userPermissions =
                 userPermissionsClaim.Value.FromJson<IEnumerable<string>>() ?? [];

            bool hasPermission = userPermissions.Any(i => _requiredPermissions.Contains(i));
            if (!hasPermission)
            {
                return context.HttpContext.Response.SendForbiddenAsync(cancellation: ct);
            }

            return Task.CompletedTask;
        }
    }
}