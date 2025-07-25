﻿using GameTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.DTOs.UserDTOs
{
    public class AddUserDTO
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public RoleEnum Role { get; set; }
        public bool Status { get; set; }
        public byte Workbase { get; set; }
        public string? ImageId { get; set; }
    }
}
