using GameTrader.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Enums
{
    public enum PermissionEnum
    {
        [Url("User/GetLoggedUser")]
        [Roles([RoleEnum.SuperAdmin, RoleEnum.User])]
        GetLoggedUser = 1,
    }
}
