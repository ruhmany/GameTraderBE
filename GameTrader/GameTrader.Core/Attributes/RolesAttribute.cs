using GameTrader.Core.Enums;

namespace GameTrader.Core.Attributes
{
    public class RolesAttribute : Attribute
    {
        public RolesAttribute(params RoleEnum[] roles) => Roles = roles;
        public RoleEnum[] Roles { get; set; }
    }
}
