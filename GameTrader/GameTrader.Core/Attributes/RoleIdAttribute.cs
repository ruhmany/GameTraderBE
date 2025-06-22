namespace GameTrader.Core.Attributes
{
    public class RoleIdAttribute : Attribute
    {
        public RoleIdAttribute(string roleId) => RoleId = roleId;
        public string RoleId { get; set; }
    }
}
