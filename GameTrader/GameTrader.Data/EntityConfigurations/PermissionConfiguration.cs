using GameTrader.Core.Enums;
using GameTrader.Core.Helpers;
using GameTrader.Data.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.EntityConfigurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(SeedPermissions());
        }

        private List<Permission> SeedPermissions()
        {
            var permissionsList = new List<Permission>();
            foreach (PermissionEnum permission in Enum.GetValues(typeof(PermissionEnum)))
            {
                permissionsList.Add(new Permission
                {
                    Id = (int)permission,
                    Name = permission.ToString(),
                    URL = permission.GetURL()
                });
            }
            return permissionsList;
        }
    }
}
