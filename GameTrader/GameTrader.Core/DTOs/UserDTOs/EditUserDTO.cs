using GameTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.DTOs.UserDTOs
{
    public class EditUserDTO : AddUserDTO
    {
        public string UserId { get; set; }
        public RoleEnum Role { get; set; }
    }
}
