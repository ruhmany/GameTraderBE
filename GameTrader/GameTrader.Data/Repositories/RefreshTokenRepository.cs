using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Data.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public RefreshTokenRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Create(RefreshTokenDTO model)
        {
            var refreshToken = new RefreshToken
            {
                Id = model.Id,
                Token = model.Token,
                UserId = model.UserId
            };
            await _dbcontext.RefreshTokens.AddAsync(refreshToken);
            await _dbcontext.SaveChangesAsync();
            return refreshToken.Id.ToString();
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAll(string id)
        {
            var tokensQuery = _dbcontext.RefreshTokens.Where(t => t.UserId == id);
            if (tokensQuery.Count() > 0)
            {
                var tokens = tokensQuery.ToList();
                _dbcontext.RefreshTokens.RemoveRange(tokens);
                await _dbcontext.SaveChangesAsync();
            }
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
