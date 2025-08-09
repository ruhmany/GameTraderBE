using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.DomainModels
{
    public class Request : BaseEntity
    {
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
        public Guid ItemId { get; set; }
        public int Quntity { get; set; }
        public bool IsPaid { get; set; }
        public bool IsAccecpted { get; set; }
        public bool IsDone { get; set; }
    }
}
