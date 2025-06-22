using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.DomainModels
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
