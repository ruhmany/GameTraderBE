using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Core.Enums;
using GameTrader.Core.ServiceModels.PagedList;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<IdentityResult> Create(AddUserDTO addUser);
        Task<IdentityResult> ChangePassword(ChangePasswordDTO changePassword);
        Task<(LoginResponseDTO Response, string Massage)> Login(LoginDTO model);
        Task<bool> DeleteAsync(string id);
        Task<(bool Result, string Message)> ToggleAsync(string id, string loggedInUserRole);
        Task<PagedListModel<UserPageDTO>> GetAll(int pageIndex, int pageSize, string currentUserRole, string? firstName = null, string? lastName = null, string? workbase = null, string? sortBy = null, SortTypeEnum? sortType = null);
        Task<AuthorizationDTO> Authorize(TokenValidationDTO validationDTO);
        Task<AuthorizationDTO> Authenticate(TokenValidationDTO validationDTO);
        Task<IdentityResult> Edit(EditUserDTO userDto, string loggedInUserRole);
        Task<UserDetailsDTO> GetDetailsById(string id);
        Task<IdentityResult> ResetPassword(string userId, string actorUserRole);
        Task<bool> Logout(string userId);
        Task<bool> CheckOTP(CheckOTPDTO oTPDTO);
    }
}
