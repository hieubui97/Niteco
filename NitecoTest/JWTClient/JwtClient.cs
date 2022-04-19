using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using NitecoTest.Persistence;

namespace NitecoTest.JWTClient
{
    public class JwtClient : IJwtClient
    {
        private readonly IPersistenceFactory _persistenceFactory;
        public JwtClient(IPersistenceFactory persistenceFactory)
        {
            _persistenceFactory = persistenceFactory;
        }

        public Task<string> GetJwtToken(string email)
        {
            return GenerateJwtToken(email);
        }

        //FOR INTERNAL PROCESS
        private async Task<string> GenerateJwtToken(string email)
        {
            using (var jwtConfigClient = _persistenceFactory.GetJwtConfig())
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secreteKey = Encoding.UTF8.GetBytes(jwtConfigClient.GetJwtConfig().Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Email, email ?? ""),
                    }),
                    Issuer = jwtConfigClient.GetJwtConfig().Issuer,
                    Audience = jwtConfigClient.GetJwtConfig().Audience,
                    Expires = DateTime.UtcNow.AddSeconds(jwtConfigClient.GetJwtConfig().TimeLife),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secreteKey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}
