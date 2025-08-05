using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Core.Enums;
using GameTrader.Core.ServiceModels.PagedList;
using Microsoft.AspNetCore.Identity;

namespace GameTrader.Core.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<PagedListModel<UserPageDTO>> GetAll(int pageIndex, int pageSize, string currentUserRole, string? fullName = null, string? lastName = null, string? workbase = null, string? sortBy = null, SortTypeEnum? sortType = null);
        Task<(IdentityResult, string)> Create(AddUserDTO userDTO);
        Task<bool> DeleteAsync(string id);
        Task<bool> ToggleAsync(string id);
        Task<IdentityResult> Edit(EditUserDTO userDto);
        Task<UserDetailsDTO> GetDetailsById(string id);
        Task<IdentityResult> ResetPassword(string userId, string actorUserRole, string newPassword);
    }
}
