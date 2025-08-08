using GameTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.DTOs.ItemDTOs
{
    public class EditItemDTO
    {
        public Guid? Id { get; set; } // Null for new items
        public decimal UnitPrice { get; set; }
        public int UnitCount { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
