using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.DTOs.ItemDTOs
{
    public class BuyItemsDTO
    {
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
        public Guid ItemId { get; set; }
        public int Quntity { get; set; }
    }
}
