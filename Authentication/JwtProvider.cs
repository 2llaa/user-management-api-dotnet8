using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public JwtTokenResultDto TokenGenerator(User user , IList<string> roles)
        {
            var expiration = DateTime.UtcNow.AddHours(1);

            var claims = new List<Claim>
{
              new Claim(JwtRegisteredClaimNames.Sub, user.Id),
              new Claim(JwtRegisteredClaimNames.Email, user.Email!),
              new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? ""),
              new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
              );

               var token = new JwtSecurityToken(
                 _options.Issuer,
                 _options.Audience,
                 claims,
                 null,
                 expiration, 
                 signingCredentials
               );



            return new JwtTokenResultDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiration
            };
        }

    
    }
}
