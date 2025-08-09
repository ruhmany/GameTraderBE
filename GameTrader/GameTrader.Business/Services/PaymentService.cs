using GameTrader.Core.DTOs.ItemDTOs;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Business.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<(bool result, string message)> RequestToBuyItemsAsync(BuyItemsDTO buyItemsDTO)
        {
            return await _paymentRepository.RequestToBuyItemsAsync(buyItemsDTO);
        }
    }
}
