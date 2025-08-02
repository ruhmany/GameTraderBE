using GameTrader.Core.Enums;
using GameTrader.Core.Helpers;
using GameTrader.Data.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.EntityConfigurations
{
    internal class RolePermissionConfigurations : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(rp => rp.Id);
            builder.HasData(RolePermissionsSeeding());
        }

        private static List<RolePermission> RolePermissionsSeeding()
        {
            var data = new List<RolePermission>();
            var init = 1;
            foreach (PermissionEnum permission in Enum.GetValues(typeof(PermissionEnum)))
            {
                var roles = permission.GetPermissionRoles();

                foreach (var role in roles)
                {
                    data.Add(
                        new()
                        {
                            Id = init++,
                            PermissionId = (int)permission,
                            RoleId = role.GetId(),
                        }
                    );
                }
            }
            return data;
        }

    }
}
