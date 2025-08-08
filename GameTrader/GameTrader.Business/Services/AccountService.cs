using GameTrader.Core.DTOs.AccountDTOs;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<(bool result, string message)> CreateAccount(AddAccountDTO accountDTO)
        {
            return await _accountRepository.CreateAccount(accountDTO);
        }

        public async Task<(bool result, string message)> UpdateAsync(EditAccountDTO account)
        {
            return await _accountRepository.UpdateAsync(account);
        }
    }
}
