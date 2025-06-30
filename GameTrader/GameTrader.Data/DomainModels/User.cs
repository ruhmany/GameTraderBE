using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameTrader.Data.DomainModels
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; }
        public bool IsResetPassword { get; set; } = false;
        public string? LastOldPassword { get; set; }
        public DateTime? PasswordExpirationDate { get; set; } = null;
        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }
        public User()
        {
            CreatedDate = DateTime.Now;
        }
        public virtual ICollection<Account> Acounts { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
