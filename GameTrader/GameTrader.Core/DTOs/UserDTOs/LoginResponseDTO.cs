using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.DTOs.UserDTOs
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
        public string RefreshToken { get; set; }
        public string SessionId { get; set; }
        public bool IsFirstLogin { get; set; } = false;
        public bool IsResetPassword { get; set; } = false;
        public bool IsPasswordExpired { get; set; }
    }
}
