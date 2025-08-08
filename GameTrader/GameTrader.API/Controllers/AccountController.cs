using GameTrader.Core.DTOs.AccountDTOs;
using GameTrader.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameTrader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAccount([FromBody] AddAccountDTO accountDTO)
        {
            if (accountDTO == null)
            {
                return BadRequest("Account model cannot be null.");
            }
            var result = await _accountService.CreateAccount(accountDTO);

            if (result.result)
            {
                return Ok(result.message);
            }

            return BadRequest(result.message);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAccount([FromBody] EditAccountDTO accountDTO)
        {
            if (accountDTO == null)
            {
                return BadRequest("Account model cannot be null.");
            }
            var result = await _accountService.UpdateAsync(accountDTO);

            if (result.result)
            {
                return Ok(result.message);
            }

            return BadRequest(result.message);
        }
    }
}
