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

        [Url("item/ add-item")]
        [Roles([RoleEnum.SuperAdmin, RoleEnum.User])]
        AddItem = 2,
        [Url("item/categories")]
        [Roles([RoleEnum.SuperAdmin, RoleEnum.User])]
        GetCategories = 3,
        [Url("item/update-item")]
        [Roles([RoleEnum.SuperAdmin, RoleEnum.User])]
        UpdateItem = 4,
        [Url("item/add-category")]
        [Roles([RoleEnum.SuperAdmin, RoleEnum.User])]
        AddCategory = 5,
    }
}
