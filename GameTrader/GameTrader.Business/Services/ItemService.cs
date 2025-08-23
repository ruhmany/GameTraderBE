using GameTrader.Core.DTOs.ItemDTOs;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Business.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Task<bool> AddNewItem(AddItemDTO itemDTO)
        => _itemRepository.AddNewItem(itemDTO);

        public Task<bool> UpdateItem(EditItemDTO itemDTO)
        => _itemRepository.UpdateItem(itemDTO);
    }
}
