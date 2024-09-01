using Auth.Features.Users.Services;
using Auth.Shared.CustomErrors;
using ErrorOr;
using FastEndpoints.Security;

namespace Auth.Shared.RequestPipeline
{
    public class TokenValidation : JwtRevocationMiddleware
    {
        private readonly ILogger<TokenValidation> _logger;
        private readonly ITokenService _tokenService;
        private readonly ISessionService _sessionService;

        public TokenValidation(
            ILogger<TokenValidation> logger,
            WebApplication app,
            RequestDelegate next) : base(next)
        {
            _logger = logger;
            using IServiceScope scope = app.Services.CreateScope();

            _tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
            _sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
        }

        protected override async Task<bool> JwtTokenIsValidAsync(string jwtToken, CancellationToken ct)
        {
            ErrorOr<Ulid> isTokenValid = await _tokenService.ValidateRefreshToken(jwtToken);

            return await isTokenValid.MatchAsync(
                async valid => await Task.FromResult(true),
                async error =>
                {
                    _logger.LogWarning(GlobalErrors.InvalidTokenLogMsg, jwtToken);
                    return await Task.FromResult(false);
                });
        }
    }
}