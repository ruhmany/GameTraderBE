using GameTrader.Core.DTOs.AccountDTOs;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Core.StaticData;
using GameTrader.Data.StaticExtensionsMappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool result, string message)> CreateAccount(AddAccountDTO accountDTO)
        {
            var account = accountDTO.MapToAccount();
            await _context.Accounts.AddAsync(account);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult > 0 
                ? (true, ValidationMessages.AccountCreatedSuccessfully) 
                : (false, ValidationMessages.AccountCreationFailed);
        }

        public async Task<(bool result, string message)> UpdateAsync(EditAccountDTO account)
        {
            var accountEntity = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == account.Id);  
            if (accountEntity == null)
            {
                return (false, ValidationMessages.AccountNotFound);
            }
            else
            {
                accountEntity.UpdateFromDto(account);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult > 0 
                    ? (true, ValidationMessages.AccountUpdatedSuccessfully) 
                    : (false, ValidationMessages.AccountUpdateFailed);
            }
        }
    }
}
