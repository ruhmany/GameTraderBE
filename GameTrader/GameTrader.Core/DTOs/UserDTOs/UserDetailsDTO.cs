using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.DTOs.UserDTOs
{
    public class UserDetailsDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public string WorkbaseName { get; set; }
        public byte WorkbaseId { get; set; }
        public byte RoleId { get; set; }
        public string RoleName { get; set; }
        public string Image { get; set; }
        public bool IsResetPassword { get; set; } = false;
    }
}
