using GameTrader.Core.Attributes;

namespace GameTrader.Core.Enums
{
    public enum RoleEnum
    {
        [DisplayName("Super Admin")]
        [RoleId("c9809a45-1681-49bf-9765-b64d015abfd0")]
        SuperAdmin = 1,
        [DisplayName("User")]
        [RoleId("c9809a45-1681-49bf-9765-b64d015abfd2")]
        User = 1,
    }
}
