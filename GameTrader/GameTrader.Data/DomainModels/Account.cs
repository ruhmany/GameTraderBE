using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.DomainModels
{
    public class Account : BaseEntity
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
