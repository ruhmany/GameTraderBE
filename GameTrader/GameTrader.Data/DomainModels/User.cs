using Microsoft.AspNetCore.Identity;

namespace GameTrader.Data.DomainModels
{
    public class User : IdentityUser
    {
        public ICollection<Account> Acounts { get; set; }
        public Profile Profile { get; set; }
        public bool IsDeleted { get; set; }
    }
}
