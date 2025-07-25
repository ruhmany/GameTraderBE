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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role).ToUpper());
            builder.HasData(SeedRoles());

        }
        private Role[] SeedRoles()
        {
            return new Role[]
            {
                new Role{
                    Id = "c9809a45-1681-49bf-9765-b64d015abfd0",
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Description = "Administrator role with full permissions"
                },
                new Role{
                    Id = "c9809a45-1681-49bf-9765-b64d015abfd2",
                    Name = "User",
                    NormalizedName = "USER",
                    Description = "Administrator role with full permissions"
                },
            };
        }
    }
}
