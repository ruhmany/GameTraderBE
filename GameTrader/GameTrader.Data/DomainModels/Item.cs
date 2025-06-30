using GameTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.DomainModels
{
    public class Item : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int UnitCount { get; set; }
        public CategoryEnum Category { get; set; }
        public virtual Account Account { get; set; }
    }
}
