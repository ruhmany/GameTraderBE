using AutoMapper;
using GameTrader.API.Models;
using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Core.Enums;
using GameTrader.Core.Factories;
using GameTrader.Core.Interfaces.IServices;
using GameTrader.Core.StaticData;
using GameTrader.Data.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameTrader.API.Controllers
{
    public sealed class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IProfileService _profileService;

        public UserController(IUserService userService, IMapper mapper, IEmailService emailService, IProfileService profileService) : base()
        {
            _userService = userService;
            _mapper = mapper;
            _emailService = emailService;
            _profileService = profileService;
        }


        [HttpPost("Create")]
        public async Task<ResponseFactory> Create(AddUserModel addUser)
        {
            var userDto = _mapper.Map<AddUserDTO>(addUser);
            var result = await _userService.Create(userDto);
            return result.Succeeded ? OK(ValidationMessages.OperationSucceded) :
                BadRequest(result.Errors.Select(x => x.Description).ToArray());
        }

        [HttpPost("ConfirmEmail")]
        public async Task<ResponseFactory> Create(CheckOTPDTO oTPDTO)
        {
            var result = await _userService.CheckOTP(oTPDTO);
            return result ? OK(ValidationMessages.OperationSucceded) :
                BadRequest(ValidationMessages.OperationFaild);
        }

        [HttpPost("SendEmail")]
        public IActionResult SendEmail(string to, string subject, string content)
        {
            _emailService.EmailSender(to, subject, content);
            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<ResponseFactory> Edit(EditUserModel editUser)
        {
            var userDto = _mapper.Map<EditUserDTO>(editUser);
            var result = await _userService.Edit(userDto, UserRole);
            return result.Succeeded ? OK(ValidationMessages.OperationSucceded) :
                BadRequest(result.Errors.Select(x => x.Description).ToArray());
        }

        [HttpGet("GetDetailsById/{id}")]
        public async Task<ResponseFactory> Details(string id)
        {
            UserDetailsDTO user = await _userService.GetDetailsById(id);
            return user is not null ? OK(user) : BadRequest(ValidationMessages.OperationFaild);
        }

        [HttpGet("GetLoggedUser")]
        public async Task<ResponseFactory> Details()
        {
            if (UserId is null) return BadRequest(ValidationMessages.BadRequest);
            UserDetailsDTO user = await _userService.GetDetailsById(UserId);
            return user is not null ? OK(user) : BadRequest(ValidationMessages.OperationFaild);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ResponseFactory> Login(LoginModel model)
        {
            var result = await _userService.Login(_mapper.Map<LoginDTO>(model));
            return result.Response is not null ? OK(result.Response, ValidationMessages.LoginSuccess) : InternalServerError(result.Massage);
        }

        [HttpPost("logout")]
        public async Task<ResponseFactory> LogOut()
        {
            var result = await _userService.Logout(UserId);
            return result ? OK(ValidationMessages.LogOutSuccess) : InternalServerError(ValidationMessages.LogOutFailed);
        }

        [HttpPut("ChangePassword")]
        public async Task<ResponseFactory> ChangePassword(ChangePasswordDTO changePassword)
        {
            if (UserId is null) return BadRequest(ValidationMessages.BadRequest);
            changePassword.Id = UserId;
            var validationResult = changePassword.Validate();
            if (!string.IsNullOrEmpty(validationResult)) return BadRequest(validationResult);
            var result = await _userService.ChangePassword(changePassword);
            return result.Succeeded ? OK(ValidationMessages.OperationSucceded) :
                BadRequest(result.Errors.Select(x => x.Description).ToArray());
        }
        [HttpGet("get-all")]
        public async Task<ResponseFactory> GetAll(int pageNumber, int pageSize, string? firstName = null, string? lastName = null, string? workbaseName = null, string? sortBy = null, SortTypeEnum? sortType = SortTypeEnum.ASC)
        {

            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest();

            var result = await _userService.GetAll(pageNumber, pageSize, UserRole, firstName, lastName, workbaseName, sortBy, sortType);
            if (result.Items is null)
            {
                return NotFound(ValidationMessages.NotFound);
            }
            return OK(result, ValidationMessages.OperationSucceded);
        }

        [HttpDelete("delete")]
        public async Task<ResponseFactory> Delete(string id)
        {
            var result = await _userService.DeleteAsync(id);
            return result ? OK(result) : BadRequest(ValidationMessages.RequestFalid);
        }


        [HttpPut("ResetPassword")]
        public async Task<ResponseFactory> ResetPassword(string userId)
        {
            var result = await _userService.ResetPassword(userId, UserRole);
            return result.Succeeded ? OK(ValidationMessages.OperationSucceded)
                                     : BadRequest(result.Errors.Select(x => x.Description).ToArray());
        }

        [HttpPost("CreateProfile")]
        public async Task<ResponseFactory> CreateProfileAsync([FromForm] CreateProfileDTO model)
        {
            if (UserId is null) return BadRequest(ValidationMessages.BadRequest);
            var result = await _profileService.CreateProfileAsync(UserId, model.ProfilePic, model.Bio);
            return result ? OK(ValidationMessages.OperationSucceded) : BadRequest(ValidationMessages.OperationFaild);
        }

        [HttpGet("get-all-accounts")]
        public async Task<ResponseFactory> GetAllAccounts()
        {
            var accounts = await _userService.GetAllAccounts();
            if (accounts is null || !accounts.Any())
            {
                return NotFound(ValidationMessages.NotFound);
            }
            return OK(accounts, ValidationMessages.OperationSucceded);
        }
    }
}
