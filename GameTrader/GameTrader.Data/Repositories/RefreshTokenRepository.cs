using AutoMapper;
using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Data.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameTrader.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _mapperConfig;
        private readonly ApplicationDbContext _dbcontext;

        public RefreshTokenRepository(ApplicationDbContext context,
                              IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
            _mapperConfig = _mapper.ConfigurationProvider;
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
            var token = await _dbcontext.RefreshTokens.FindAsync(id);
            if (token != null)
            {
                _dbcontext.RefreshTokens.Remove(token);
                await _dbcontext.SaveChangesAsync();
            }
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

        public async Task<bool> DeleteUserToken(string id)
        {
            var token = _dbcontext.RefreshTokens.Where(t => t.UserId == id).ToList();
            if (token != null)
            {
                _dbcontext.RefreshTokens.RemoveRange(token);
                return await _dbcontext.SaveChangesAsync() > 0;

            }
            return false;
        }

        public async Task<RefreshTokenDTO> GetByToken(string token) =>
            _mapper.Map<RefreshTokenDTO>(await _dbcontext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token));

        public async Task<bool> IsExist(string id) => await _dbcontext.RefreshTokens.AnyAsync(t => t.Id == Guid.Parse(id));        
    }
}
