using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IServices
{
    public interface ITokenGeneratorService
    {
        string GenerateToken();
        string GenerateToken(string secritKey, string issuer, string audience, DateTime expirationDate, IEnumerable<Claim>? claims = null);
    }

}
