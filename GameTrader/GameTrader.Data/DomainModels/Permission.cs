using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.DomainModels
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
