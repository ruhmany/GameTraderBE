using GameTrader.Core.DTOs.AccountDTOs;
using GameTrader.Core.DTOs.ItemDTOs;
using GameTrader.Data.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.StaticExtensionsMappers
{
    public static class Mapper
    {

        public static Request ToRequestEntity(this BuyItemsDTO dto)
        {
            if (dto == null) return null;

            return new Request
            {
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId,
                ItemId = dto.ItemId,
                Quntity = dto.Quntity,
                IsPaid = false,
                IsAccecpted = false,
                IsDone = false
            };
        }
        public static Account MapToAccount(this AddAccountDTO accountDTO)
        {
            return new Account
            {
                Username = accountDTO.Username,
                GameAccId = accountDTO.GameAccId,
                UserId = accountDTO.UserId,                
            };
        }

        public static void UpdateFromDto(this Account account, EditAccountDTO dto)
        {
            if (dto == null) return;

            account.Username = dto.Username;
            account.GameAccId = dto.GameAccId;
            account.UserId = dto.UserId;
        }

        public static Item MapToItem(this AddItemDTO itemDTO)
        {
            if (itemDTO == null) return null;
            try
            {
                return new Item
                {
                    UnitPrice = itemDTO.UnitPrice,
                    UnitCount = itemDTO.UnitCount,
                    Category = (int)itemDTO.Category,
                    AccountId = Guid.Parse(itemDTO.AccountId)
                };
            }catch(Exception ex)
            {
                Console.WriteLine($"Error mapping AddItemDTO to Item: {ex.Message}");
            }
            return null;
        }
    }
}
