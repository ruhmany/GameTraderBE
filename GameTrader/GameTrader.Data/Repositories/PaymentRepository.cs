using GameTrader.Core.DTOs.ItemDTOs;
using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Core.StaticData;
using GameTrader.Data.StaticExtensionsMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool result, string message)> RequestToBuyItemsAsync(BuyItemsDTO buyItemsDTO)
        {
            var request = buyItemsDTO.ToRequestEntity();
            await _context.Requests.AddAsync(request);
            var saveResult = await _context.SaveChangesAsync() > 0;
            return saveResult 
                ? (true, ValidationMessages.RequestCreatedSuccessfully) 
                : (false, ValidationMessages.RequestCreationFailed);
        }
    }
}
