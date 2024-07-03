using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authenticate.Entities;
using Authenticate.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Authenticate
{

    public sealed class JWTProvider : IJWTProvider
    {
        public string Generate(User user)
        {
            JWTOptions Options = new();
            var claims = new Claim[] {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Username)
            };
            var secKeyAsByte = Encoding.UTF8.GetBytes(Options.SecretKey);
            var secKey = new SymmetricSecurityKey(secKeyAsByte);
            var signingCredentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMonths(1);
            var token = new JwtSecurityToken(Options.Issueer, Options.Audience, claims, null, expires, signingCredentials);
            var handler = new JwtSecurityTokenHandler();
            string tokenValue = handler.WriteToken(token);
            return tokenValue;
        }
    }

}