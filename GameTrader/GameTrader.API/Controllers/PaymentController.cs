using GameTrader.Core.DTOs.ItemDTOs;
using GameTrader.Core.Factories;
using GameTrader.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameTrader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Request-To-Buy-Items")]
        public async Task<ResponseFactory> RequestToBuyItems([FromBody] BuyItemsDTO buyItemsDTO)
        {
            var result = await _paymentService.RequestToBuyItemsAsync(buyItemsDTO);
            return result.result 
                ? OK(result.message) 
                : BadRequest(result.message);
        }
    }
}
