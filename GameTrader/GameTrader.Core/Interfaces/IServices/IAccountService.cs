using GameTrader.Core.DTOs.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<(bool result, string message)> CreateAccount(AddAccountDTO accountDTO);
        Task<(bool result, string message)> UpdateAsync(EditAccountDTO account);
    }
}
