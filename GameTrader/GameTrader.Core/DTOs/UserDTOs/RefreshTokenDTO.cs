using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.DTOs.UserDTOs
{
    public class RefreshTokenDTO
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public RefreshTokenDTO()
        {

        }
    }
}
