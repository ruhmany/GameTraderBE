using GameTrader.Core.DTOs.UserDTOs;

namespace GameTrader.Core.Interfaces.IRepositories
{
    public interface IRefreshTokenRepository
    {
        Task<string> Create(RefreshTokenDTO model);
        Task Delete(Guid id);
        Task<bool> DeleteUserToken(string id);
        Task DeleteAll(string id);
        Task<RefreshTokenDTO> GetByToken(string token);
        Task<bool> IsExist(string id);
    }
}
