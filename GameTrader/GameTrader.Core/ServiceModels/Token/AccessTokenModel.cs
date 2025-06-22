using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.ServiceModels.Token
{
    public class AccessTokenModel
    {
        public string Value { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
