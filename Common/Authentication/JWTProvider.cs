using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authenticate.Entities;
using Authenticate.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Authenticate
{

    public sealed class JWTProvider(IConfiguration configuration) : IJWTProvider
    {

        private readonly IConfiguration _configuration = configuration;

        public string Generate(User user)
        {
            JWTOptions Options = new();

            Claim[] claims =
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim("userId", user.Id.ToString()),
                new Claim("roleId", user.RoleId?.ToString() ?? ""),
                new Claim("organizationId", user.OrganizationId.ToString())
            ];

            byte[] secKeyAsByte = Encoding.UTF8.GetBytes(Options.SecretKey);
            SymmetricSecurityKey secKey = new(secKeyAsByte);
            SigningCredentials signingCredentials = new(secKey, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.UtcNow.AddMonths(1);

            JwtSecurityToken token = new(
                Options.Issueer,
                Options.Audience,
                claims,
                expires: expires,
                signingCredentials: signingCredentials);

            JwtSecurityTokenHandler handler = new();
            string tokenValue = handler.WriteToken(token);

            return tokenValue;
        }
    }

}