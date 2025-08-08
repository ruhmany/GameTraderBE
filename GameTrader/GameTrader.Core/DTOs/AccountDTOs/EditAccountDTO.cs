using GameTrader.Core.DTOs.ItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.DTOs.AccountDTOs
{
    public class EditAccountDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string GameAccId { get; set; }
        public string UserId { get; set; }
        public List<EditItemDTO> Items { get; set; }
    }
}
