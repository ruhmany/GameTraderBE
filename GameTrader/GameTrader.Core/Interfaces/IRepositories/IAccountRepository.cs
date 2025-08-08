using GameTrader.Core.DTOs.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IRepositories
{
    public interface IAccountRepository
    {
        Task<(bool result, string message)> CreateAccount(AddAccountDTO accountDTO);
        Task<(bool result, string message)> UpdateAsync(EditAccountDTO account);
    }
}
