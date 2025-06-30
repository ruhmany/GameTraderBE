using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.ServiceModels.Configuration
{
    public class JWTConfigurationModel
    {
        public string AccessTokenSecret { get; set; }
        public string RefreshTokenSecret { get; set; }
        public int AccessTokenExpirationDurationMinutes { get; set; }
        public int RefreshTokenExpirationDurationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
