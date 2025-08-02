using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Helpers
{
    public class SMTPHelper 
    {
        private readonly IConfiguration _configuration;

        public SMTPHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSmtpServer()
        {
            return _configuration["SMTP:Server"];
        }

        public string GetSmtpPort()
        {
            return _configuration["SMTP:Port"];
        }

        public string GetSmtpUsername()
        {
            return _configuration["SMTP:Username"];
        }

        public string GetSmtpPassword()
        {
            return _configuration["SMTP:Password"];
        }
    }
}
