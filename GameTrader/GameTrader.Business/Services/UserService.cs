using AutoMapper;
using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Core.Enums;
using GameTrader.Core.Helpers;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Core.Interfaces.IServices;
using GameTrader.Core.ServiceModels.Configuration;
using GameTrader.Core.ServiceModels.Email;
using GameTrader.Core.ServiceModels.PagedList;
using GameTrader.Core.ServiceModels.Token;
using GameTrader.Core.StaticData;
using GameTrader.Data.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace GameTrader.Business.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly string _defaultPassword;
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JWTConfigurationModel _jwtConfiguration;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, ITokenGeneratorService tokenGeneratorService,
          RoleManager<Role> roleManager, JWTConfigurationModel jwtConfiguration
              , IMapper mapper, IRefreshTokenRepository refreshTokenRepository
            , UserManager<User> userManager
, IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenGeneratorService = tokenGeneratorService;
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtConfiguration = jwtConfiguration;
            _emailService = emailService;
        }


        public async Task<bool> CheckOTP(CheckOTPDTO oTPDTO)
        {
            var user = await _userManager.FindByEmailAsync(oTPDTO.Email);
            if (user == null)
                return false;
            if (user.OTP == oTPDTO.OTP)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return true;
            } 
            return false;
        }

        public async Task<(LoginResponseDTO Response, string Massage)> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (!user.Status)
                return (null, Massage: ValidationMessages.UserNotActive);
            if (!user.EmailConfirmed)
                return (null, Message: ValidationMessages.UserNotActive);
            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isCorrectPassword)
            {
                var oldPassVerification = _userManager.PasswordHasher.VerifyHashedPassword(user, user.LastOldPassword, model.Password);
                if (oldPassVerification == PasswordVerificationResult.Success)
                {
                    return (null, ValidationMessages.EnteredOldPassword);
                }
                await _userManager.AccessFailedAsync(user);
            }
            else
            {
                await _userManager.UpdateSecurityStampAsync(user);
                await _userManager.ResetAccessFailedCountAsync(user);
            }

            await _refreshTokenRepository.DeleteAll(user.Id);

            if (user is not null)
            {
                var claims = new List<Claim>();
                var roles = await _userManager.GetRolesAsync(user);
                claims.AddRange(GetPermissions(roles));

                LoginResponseDTO response = await Authenticate(user, claims);

                if (model.Password.Equals("P@$$w0rd") && !user.IsResetPassword)
                    response.IsFirstLogin = true;

                if (user.IsResetPassword)
                    response.IsResetPassword = true;

                if (DateTime.Now > user.PasswordExpirationDate)
                {
                    response.IsPasswordExpired = true;
                }
                return (response, null);
            }
            else
            {
                return (null, Massage: ValidationMessages.InvalidEmailOrPassword);
            }
        }

        private List<Claim> GetPermissions(IList<string> roles)
        {
            var claims = new List<Claim>();
            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role.ToString(), x)));
            foreach (PermissionEnum permission in Enum.GetValues(typeof(PermissionEnum)))
            {
                if (permission.GetPermissionRoles().Any(x => roles.Contains(x.ToString())))
                {
                    claims.Add(new Claim("Permission", permission.ToString()));
                    claims.Add(new Claim("Url", permission.GetURL()));
                }
            }
            return claims;
        }

        public async Task<LoginResponseDTO> Authenticate(User user, List<Claim> claims)
        {
            string refreshToken = _tokenGeneratorService.GenerateToken();
            RefreshTokenDTO refreshTokenDTO = new()
            {
                Token = refreshToken,
                UserId = user.Id
            };
            var loginId = await _refreshTokenRepository.Create(refreshTokenDTO);

            var accessToken = GenerateToken(user, claims, loginId);


            return new LoginResponseDTO()
            {
                AccessToken = accessToken.Value,
                AccessTokenExpirationTime = accessToken.ExpirationDate,
                RefreshToken = refreshToken,
                SessionId = user.Id
            };
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordDTO changePassword)
        {
            var user = await _userManager.FindByIdAsync(changePassword.Id);
            var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (result.Succeeded)
            {
                if (user.IsResetPassword) user.IsResetPassword = false;
                result = await _userManager.UpdateAsync(user);
            }
            foreach (var error in result.Errors)
            {
                error.Description = error.Code == "PasswordMismatch" ? "Current password isn't correct" : error.Description;
            }
            //result.Errors(x => x.Description = x.Code == "PasswordMismatch" ? "Current password isn't correct" : x.Description);
            return result;
        }

        public async Task<IdentityResult> Create(AddUserDTO addUser)
        {
            var result = await _userRepository.Create(addUser);
            if (result.Item1.Succeeded)
            {
                var template = EmailTemplateModels.GetEmailConfirmationTemplate(addUser.FirstName, addUser.LastName, "Pubg Store", addUser.Email, result.Item2);
                await _emailService.EmailSender(addUser.Email,
                    "Email Confirmaion",
                    template
                );
            }
            return result.Item1;
        }

        public async Task<IdentityResult> Edit(EditUserDTO userDto, string loggedInUserRole)
        {
            return await _userRepository.Edit(userDto);
        }

        public AccessTokenModel GenerateToken(User user, List<Claim> permissionsClaims, string loginId)
        {
            List<Claim> claims = new()
        {
            new ("id", user.Id.ToString()),
            new (ClaimTypes.Email, user?.Email??""),
            new (ClaimTypes.Name, user.UserName),
            new ("LoginId", loginId.ToString())
        };
            claims.AddRange(permissionsClaims);

            DateTime expirationTime = DateTime.Now.AddMinutes(_jwtConfiguration.AccessTokenExpirationDurationMinutes());
            string value = _tokenGeneratorService.GenerateToken(
                _jwtConfiguration.AccessTokenSecret(),
                _jwtConfiguration.Issuer(),
                _jwtConfiguration.Audience(),
                expirationTime,
                claims);

            return new()
            {
                Value = value,
                ExpirationDate = expirationTime
            };
        }
        public async Task<PagedListModel<UserPageDTO>> GetAll(int pageIndex, int pageSize, string currentUserRole, string? firstName = null, string? lastName = null, string? workbase = null, string? sortBy = null, SortTypeEnum? sortType = null)
            => await _userRepository.GetAll(pageIndex, pageSize, currentUserRole, firstName, lastName, workbase, sortBy, sortType);


        public async Task<bool> DeleteAsync(string id) => await _userRepository.DeleteAsync(id);
        public async Task<(bool Result, string Message)> ToggleAsync(string id, string loggedInUserRole)
        {
            if (!CanChangeData(id, loggedInUserRole))
                return (false, ValidationMessages.UnauthrizedManager);
            if (await _userRepository.ToggleAsync(id))
                return (true, ValidationMessages.OperationSucceded);
            return (false, ValidationMessages.OperationFaild);
        }

        public async Task<AuthorizationDTO> Authorize(TokenValidationDTO validationDTO)
        {
            if (string.IsNullOrEmpty(validationDTO.Token) || string.IsNullOrEmpty(validationDTO.Url))
                return new AuthorizationDTO(string.Empty, HttpStatusCode.BadRequest);
            try
            {
                SecurityToken validatedToken = ValidateToken(validationDTO.Token);

                var isAllowedLoginId = await VerifyLoginId(((JwtSecurityToken)validatedToken).Claims.Where(c => c.Type == "LoginId").First().Value);

                if (!isAllowedLoginId)
                {
                    return new AuthorizationDTO(string.Empty, HttpStatusCode.Unauthorized);
                }

                var userUrls = ((JwtSecurityToken)validatedToken).Claims.Where(c => c.Type == "Url").Select(c => c.Value.ToLower()).ToList();

                var userPermissions = ((JwtSecurityToken)validatedToken).Claims.Where(c => c.Type == "Permission").Select(c => c.Value.ToLower()).ToList();

                if (userUrls.Count > 0 && userUrls.Contains(validationDTO.Url.ToLower()))
                {
                    string userId = ((JwtSecurityToken)validatedToken).Claims.Where(c => c.Type == "id").Select(c => c.Value).FirstOrDefault();
                    return string.IsNullOrEmpty(userId) ? new AuthorizationDTO(string.Empty, HttpStatusCode.Unauthorized)
                                                         : new AuthorizationDTO(userId, HttpStatusCode.OK);
                }
                return new AuthorizationDTO(string.Empty, HttpStatusCode.Unauthorized);
            }
            catch
            {
                return new AuthorizationDTO(string.Empty, HttpStatusCode.Unauthorized);
            }
        }

        public async Task<AuthorizationDTO> Authenticate(TokenValidationDTO validationDTO)
        {
            if (string.IsNullOrEmpty(validationDTO.Token) || string.IsNullOrEmpty(validationDTO.Url))
                return new AuthorizationDTO(string.Empty, HttpStatusCode.BadRequest);

            try
            {
                SecurityToken validatedToken = ValidateToken(validationDTO.Token);

                string userId = ((JwtSecurityToken)validatedToken).Claims.Where(c => c.Type == "id").Select(c => c.Value).FirstOrDefault();

                var isAllowedLoginId = await VerifyLoginId(((JwtSecurityToken)validatedToken).Claims.Where(c => c.Type == "LoginId").First().Value);

                return isAllowedLoginId ? new AuthorizationDTO(userId, HttpStatusCode.OK) : new AuthorizationDTO(string.Empty, HttpStatusCode.Unauthorized);
            }
            catch
            {
                return new AuthorizationDTO(string.Empty, HttpStatusCode.Unauthorized);
            }
        }

        private async Task<bool> VerifyLoginId(string loginId)
        {
            return await _refreshTokenRepository.IsExist(loginId);
        }

        private SecurityToken ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var accessTokenSecret = Encoding.ASCII.GetBytes(_jwtConfiguration.AccessTokenSecret());

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(accessTokenSecret),
                    ValidIssuer = _jwtConfiguration.Issuer(),
                    ValidAudience = _jwtConfiguration.Audience(),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                return validatedToken;
            }
            catch
            {
                return default;
            }
        }

        public async Task<UserDetailsDTO> GetDetailsById(string id)
        {
            return await _userRepository.GetDetailsById(id);
        }

        public async Task<IdentityResult> ResetPassword(string userId, string actorUserRole)
        {
            var password = PasswordGeneratorHelper.Generate();
            return await _userRepository.ResetPassword(userId, actorUserRole, password);
        }

        public async Task<bool> Logout(string userId) => await _refreshTokenRepository.DeleteUserToken(userId);

        private bool CanChangeData(string userId, string loggedInUserRole)
        {
            var userRole =
                _userRepository.GetDetailsById(userId).Result.RoleName;
            return userRole.ToLower() == RoleEnum.SuperAdmin.ToString().ToLower() || userRole.ToLower() == loggedInUserRole.ToLower() ? false : true;
        }
    }
}
