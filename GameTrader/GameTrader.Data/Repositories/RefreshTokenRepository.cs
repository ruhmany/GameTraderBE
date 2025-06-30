using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public Task<string> Create(RefreshTokenDTO model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAll(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserToken(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshTokenDTO> GetByToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(string id)
        {
            throw new NotImplementedException();
        }
    }
}
