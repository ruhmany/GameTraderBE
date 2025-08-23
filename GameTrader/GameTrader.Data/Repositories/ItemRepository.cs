using GameTrader.Core.DTOs.ItemDTOs;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Data.StaticExtensionsMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewItem(AddItemDTO itemDTO)
        {
            var item = itemDTO.MapToItem();
            if (item != null)
            {
                _context.Items.Add(item);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> UpdateItem(EditItemDTO itemDTO)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == itemDTO.Id);
            if (item != null)
            {
                item.UnitPrice = itemDTO.UnitPrice;
                item.UnitCount = itemDTO.UnitCount;
                item.UpdatedAt = DateTime.UtcNow;
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
