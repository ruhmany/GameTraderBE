using GameTrader.Core.Interfaces.IServices;
using GameTrader.Core.ServiceModels.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Business.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly JWTConfigurationModel _configuration;


        public TokenGeneratorService(JWTConfigurationModel configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken()
        {
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.RefreshTokenExpirationDurationMinutes());

            return GenerateToken(
                _configuration.RefreshTokenSecret(),
                _configuration.Issuer(),
                _configuration.Audience(),
                expirationTime);
        }
        public string GenerateToken(string secritKey, string issuer, string audience, DateTime expirationDate, IEnumerable<Claim>? claims = null)
        {
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secritKey));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new(issuer, audience, claims, DateTime.Now, expirationDate, signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
