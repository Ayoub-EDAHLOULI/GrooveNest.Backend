using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GrooveNest.Service.Services
{
    public class JwtTokenService(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateJwtToken(string email, string role)
        {
            var key = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(key) || Encoding.UTF8.GetByteCount(key) < 32)
                throw new InvalidOperationException("JWT secret key must be at least 32 bytes.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
