namespace GameTrader.Data.DomainModels
{
    public class RolePermission 
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
