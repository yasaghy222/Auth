using ErrorOr;
using System.Text;
using System.Security.Claims;
using Auth.Shared.CustomErrors;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.Services
{
    public interface ITokenService
    {
        public TokenResponse GenerateTokens(GenerateTokenRequest request);
        public Task<ErrorOr<Ulid>> ValidateRefreshToken(string refreshToken);
    }

    public class TokenService : ITokenService
    {
        private readonly int _refreshTokenExpiryDuration;
        private readonly int _accessTokenExpiryDuration = 1;
        private readonly SymmetricSecurityKey _secKey;
        private readonly SigningCredentials _signingCredentials;


        public TokenService(IConfiguration configuration)
        {
            string? defSecretKey = configuration["Settings:SecretKey"];

            string secretKey = defSecretKey ?? Environment
                .GetEnvironmentVariable("Settings_SecretKey") ?? "";

            byte[] secKeyAsByte = Encoding.UTF8.GetBytes(secretKey);
            _secKey = new(secKeyAsByte);

            string? defRefreshTokenExpiryDuration =
                           configuration["Settings:RefreshTokenExpiryDuration"];

            _refreshTokenExpiryDuration = int.Parse(defRefreshTokenExpiryDuration
                ?? Environment.GetEnvironmentVariable("Settings_SecretKey") ?? "7");

            _signingCredentials = new(_secKey, SecurityAlgorithms.HmacSha256);
        }


        public TokenResponse GenerateTokens(GenerateTokenRequest request)
        {
            string accessToken = GenerateAccessToken(request);
            string refreshToken = GenerateRefreshToken(request.LoginOrganizationTitle, request.SessionId);

            return new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddDays(_accessTokenExpiryDuration),
                RefreshTokenExpiry = DateTime.UtcNow.AddDays(_refreshTokenExpiryDuration)
            };
        }

        private string GenerateAccessToken(GenerateTokenRequest request)
        {
            JwtSecurityToken token = new(
                issuer: "Auth.Service",
                audience: request.LoginOrganizationTitle,
                claims: request.MapToClaims(),
                expires: DateTime.UtcNow.AddDays(_accessTokenExpiryDuration),
                signingCredentials: _signingCredentials
            );

            JwtSecurityTokenHandler handler = new();
            string tokenValue = handler.WriteToken(token);

            return tokenValue;
        }

        private string GenerateRefreshToken(string loginOrganizationTitle, Ulid sessionId)
        {
            JwtSecurityToken token = new(
                issuer: "Auth.Service",
                audience: loginOrganizationTitle,
                claims: [new Claim(UserClaimNames.SessionId, sessionId.ToString())],
                expires: DateTime.UtcNow.AddDays(_refreshTokenExpiryDuration),
                signingCredentials: _signingCredentials
            );

            JwtSecurityTokenHandler handler = new();
            string tokenValue = handler.WriteToken(token);

            return tokenValue;
        }

        public async Task<ErrorOr<Ulid>> ValidateRefreshToken(string refreshToken)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _secKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new();

            TokenValidationResult result = await tokenHandler
                .ValidateTokenAsync(refreshToken, tokenValidationParameters);

            if (result.IsValid)
            {
                return GlobalErrors.InvalidToken();
            }

            KeyValuePair<string, object> getSessionId = result.Claims
                .FirstOrDefault(x => x.Key == UserClaimNames.SessionId);

            Ulid sessionId = Ulid.Parse(getSessionId.Value.ToString());

            return sessionId;
        }
    }
}