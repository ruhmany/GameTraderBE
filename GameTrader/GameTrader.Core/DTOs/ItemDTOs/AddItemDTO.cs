using GameTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.DTOs.ItemDTOs
{
    public class AddItemDTO
    {
        public decimal UnitPrice { get; set; }
        public int UnitCount { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
