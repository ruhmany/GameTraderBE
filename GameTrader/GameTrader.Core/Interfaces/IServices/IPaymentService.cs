using GameTrader.Core.DTOs.ItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IServices
{
    public interface IPaymentService
    {
        Task<(bool result, string message)> RequestToBuyItemsAsync(BuyItemsDTO buyItemsDTO);
    }
}
