using ErrorOr;
using FastEndpoints.Security;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Services;

namespace Auth.Shared.RequestPipeline
{
    public class TokenValidationMiddleware(
        ILogger<TokenValidationMiddleware> logger,
        ITokenService tokenService,
        ISessionService sessionService,
        RequestDelegate next)
        : JwtRevocationMiddleware(next)
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly ISessionService _sessionService = sessionService;
        private readonly ILogger<TokenValidationMiddleware> _logger = logger;

        protected override async Task SendTokenRevokedResponseAsync(
            HttpContext ctx, CancellationToken ct)
        {
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await ctx.Response.WriteAsync("", ct);
        }

        protected override async Task<bool> JwtTokenIsValidAsync(string jwtToken, CancellationToken ct)
        {
            ErrorOr<Ulid> isTokenValid = await _tokenService.ValidateAccessToken(jwtToken);

            return await isTokenValid.MatchAsync(
                async valid =>
                {
                    bool isSessionValid = await _sessionService
                        .ValidateSessionAsync(isTokenValid.Value.ToString());

                    return isSessionValid;
                },
                async error =>
                {
                    _logger.LogWarning(GlobalErrors.InvalidTokenLogMsg, jwtToken);
                    return await Task.FromResult(false);
                });
        }
    }
}