using AutoMapper;
using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Core.Enums;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Core.ServiceModels.PagedList;
using GameTrader.Data.DomainModels;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PagedList;
using System.Security.Cryptography;
using System.Text;
using X.PagedList.Extensions;

namespace GameTrader.Data.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _mapperConfig;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
            _mapperConfig = _mapper.ConfigurationProvider;
            _roleManager = roleManager;
        }
        public async Task<PagedListModel<UserPageDTO>> GetAll(int pageIndex, int pageSize, string currentUserRole, string? firstName = null, string? lastName = null, string? workbase = null, string? sortBy = null, SortTypeEnum? sortType = null)
        {

            var predicate = PredicateBuilder.New<User>(true);
            if (!string.IsNullOrEmpty(firstName))
                predicate.And(x => x.FirstName.Contains(firstName));
            if (!string.IsNullOrEmpty(lastName))
                predicate.And(x => x.LastName.Contains(lastName));
            predicate.And(x => x.UserName != "SuperAdmin");

            var baseQuery = _context.Users
                .Where(predicate)
                .Join(_context.UserRoles,
                      user => user.Id,
                      userRole => userRole.UserId,
                      (user, userRole) => new { user, userRole.RoleId })
                .Join(_context.Roles,
                      join => join.RoleId,
                      role => role.Id,
                      (join, role) => new
                      {
                          User = join.user,
                          RoleName = role.Name
                      });

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortTypeEnum.ASC)
                {
                    baseQuery = sortBy switch
                    {
                        "firstName" => baseQuery.OrderBy(x => x.User.FirstName),
                        "lastName" => baseQuery.OrderBy(x => x.User.LastName),
                        "status" => baseQuery.OrderBy(x => x.User.Status),
                        _ => throw new ArgumentException("Invalid sort type")
                    };
                }
                else
                {
                    baseQuery = sortBy switch
                    {
                        "firstName" => baseQuery.OrderByDescending(x => x.User.FirstName),
                        "lastName" => baseQuery.OrderByDescending(x => x.User.LastName),
                        "status" => baseQuery.OrderByDescending(x => x.User.Status),
                        _ => throw new ArgumentException("Invalid sort type")
                    };
                }
            }

            var list = baseQuery.AsNoTracking().Select(x => new UserPageDTO
            {
                Id = x.User.Id,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email,
                PhoneNumber = x.User.PhoneNumber,
                Status = x.User.Status,
                CanBeManaged = x.RoleName.ToLower() == RoleEnum.SuperAdmin.ToString().ToLower() || currentUserRole.ToLower() == x.RoleName.ToLower()
                    ? false : true
            }).ToPagedList();
            return new(items: list, metaData: list.GetMetaData());
        }


        public async Task<(IdentityResult, string)> Create(AddUserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            IdentityResult result = null;
            string otp = "";
            try
            {
                user.Id = Guid.NewGuid().ToString();
                result = await _userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    otp = GenerateOtp(6, true);
                    user.OTP = otp;
                    await _userManager.UpdateAsync(user);
                    await _userManager.SetLockoutEnabledAsync(user, false);
                    await _userManager.AddToRoleAsync(user, userDTO.Role.ToString());
                }

            }
            catch (Exception)
            {
                await _userManager.DeleteAsync(user);
            }
            return (result, otp);
        }

        public async Task<IdentityResult> Edit(EditUserDTO userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.UserId);
            if (user is null)
                return IdentityResult.Failed(new IdentityError() { Description = "User not Exist" });

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Any(x => x == userDto.Role.ToString()))
            {
                await _userManager.RemoveFromRolesAsync(user, roles);
                var changeRoleResult = await _userManager.AddToRoleAsync(user, userDto.Role.ToString());
                if (!changeRoleResult.Succeeded)
                    return changeRoleResult;
            }

            user.Email = userDto.Email;
            user.UserName = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Status = userDto.Status;

            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsDeleted = true;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> ToggleAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Status = !user.Status;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<UserDetailsDTO> GetDetailsById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var userDetailsDTO = _mapper.Map<UserDetailsDTO>(user);
            userDetailsDTO.RoleName = userRole;
            userDetailsDTO.RoleId = (byte)((RoleEnum)Enum.Parse(typeof(RoleEnum), userRole));
            return userDetailsDTO;
        }

        public async Task<IdentityResult> ResetPassword(string userId, string actorUserRole, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return IdentityResult.Failed(new IdentityError() { Description = "User not Exist" });

            if ((await _userManager.GetRolesAsync(user)).FirstOrDefault() == actorUserRole)
            {
                return IdentityResult.Failed(new IdentityError() { Description = "You are not allowed to reset password for this user" });
            }

            var oldPassword = user.PasswordHash;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                user.LastOldPassword = oldPassword;
                user.IsResetPassword = true;
                await _userManager.UpdateAsync(user);
                await SendEmailToUserAsync(user.Email, newPassword);
            }

            return result;
        }

        private string GenerateOtp(int length = 6, bool useAlphanumeric = false)
        {
            string Numbers = "0123456789";
            string Alphanumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var chars = useAlphanumeric ? Alphanumeric : Numbers;
            var otp = new StringBuilder();
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[4];

                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(data);
                    int randomIndex = BitConverter.ToInt32(data, 0) % chars.Length;
                    randomIndex = Math.Abs(randomIndex); // ensure non-negative index
                    otp.Append(chars[randomIndex]);
                }
            }
            return otp.ToString();
        }
        private async Task SendEmailToUserAsync(string? email, string newPassword)
        {
            //await SendEmailHelper.SendEmailAsync(email, "New Password", $"Your Password Have been reseted To {newPassword}");
        }
    }
}
