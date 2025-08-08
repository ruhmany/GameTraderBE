using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IRepositories
{
    public interface IAccountRepository
    {
        Task<(bool result, string message)> CreateAccount();
    }
}
