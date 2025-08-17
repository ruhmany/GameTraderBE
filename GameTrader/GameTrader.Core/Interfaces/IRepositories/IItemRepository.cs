using GameTrader.Core.DTOs.ItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IRepositories
{
    public interface IItemRepository
    {
        Task<bool> AddNewItem(AddItemDTO itemDTO);
        Task<bool> UpdateItem(EditItemDTO itemDTO);
    }
}
