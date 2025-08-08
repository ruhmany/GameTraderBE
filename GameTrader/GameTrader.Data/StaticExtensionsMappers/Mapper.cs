using GameTrader.Core.DTOs.AccountDTOs;
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
        public static Account MapToAccount(this AddAccountDTO accountDTO)
        {
            return new Account
            {
                Username = accountDTO.Username,
                GameAccId = accountDTO.GameAccId,
                UserId = accountDTO.UserId,
                Items = accountDTO.Items?.Select(item => new Item
                {
                    UnitPrice = item.UnitPrice,
                    UnitCount = item.UnitCount,
                    Category = item.Category
                }).ToList() ?? new List<Item>()
            };
        }

        public static void UpdateFromDto(this Account account, EditAccountDTO dto)
        {
            if (dto == null) return;

            account.Username = dto.Username;
            account.GameAccId = dto.GameAccId;
            account.UserId = dto.UserId;

            if (dto.Items != null)
            {
                account.Items = dto.Items.Select(i => new Item
                {
                    Id = i.Id ?? Guid.NewGuid(),
                    UnitPrice = i.UnitPrice,
                    UnitCount = i.UnitCount,
                    Category = i.Category,
                    AccountId = account.Id
                }).ToList();
            }
        }
    }
}
