using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using Core.JwtAuth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestfullService.Services.AuthService.Handlers
{
    public sealed class GetJwtHandler(IOptions<JwtOptions> options)
                : IHandler<GetJwtRequest, GetJwtResponse>
    {
        private readonly JwtOptions _options = options.Value;

        public GetJwtResponse Handle(GetJwtRequest _)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey));

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var roles = new[] { "Admin", "Manager", "User" };

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, "user-alpi-001"),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64),
                new(JwtRegisteredClaimNames.Azp, "Candidate"),
                new(ClaimTypes.Email, "aleksandrspicugins@icloud.com")
            };

            claims.AddRange(
                roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
                signingCredentials: creds
            );

            return new GetJwtResponse(
                new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
