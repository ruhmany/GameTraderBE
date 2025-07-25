using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.ServiceModels.Configuration
{
    public class JWTConfigurationModel
    {
        IConfiguration _configuration;

        public JWTConfigurationModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AccessTokenSecret() =>
            _configuration["JWT:AccessTokenSecret"];
        public string RefreshTokenSecret() =>
            _configuration["JWT:RefreshTokenSecret"];
        
        public int AccessTokenExpirationDurationMinutes() =>
            Convert.ToInt32(_configuration["JWT:AccessTokenExpirationDurationMinutes"]);
        public int RefreshTokenExpirationDurationMinutes() =>
            Convert.ToInt32(_configuration["JWT:RefreshTokenExpirationDurationMinutes"]);
        public string Issuer() =>
            _configuration["JWT:Issuer"];
        public string Audience() =>
            _configuration["JWT:Audience"];
    }
}
