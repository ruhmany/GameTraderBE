using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Helpers
{
    public class JWTHelper
    {
        public static string ExtractClaimsFromJwt(string jwt, string key)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(jwt);
            if (jwtToken == null)
            {
                throw new ArgumentException("Invalid JWT token.");
            }
            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == key);
            if (claim == null)
            {
                throw new ArgumentException($"Claim with key '{key}' not found in JWT token.");
            }
            return claim.Value;
        }
    }
}
