using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IServices
{
    public interface IProfileService
    {
        Task<bool> CreateProfileAsync(string userId, string profilePic, string bio);
    }
}
