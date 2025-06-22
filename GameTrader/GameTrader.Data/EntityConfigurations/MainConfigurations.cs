using GameTrader.Data.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.EntityConfigurations
{
    internal static class MainConfigurations
    {
        internal static ModelBuilder AddSchemaConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(role =>
            {
                role.ToTable(nameof(Role).ToUpper());
                role.HasKey(r => r.Id);
                role.Property(r => r.Id)
                    .ValueGeneratedOnAdd();
                role.HasIndex(r => r.NormalizedName).HasName("ROLENAMEINDEX").IsUnique();
                role.Property(r => r.ConcurrencyStamp)
                    .IsConcurrencyToken();
                role.Property(r => r.Name).HasMaxLength(256);
                role.Property(r => r.NormalizedName).HasMaxLength(256);
            });
            modelBuilder.Entity<User>(user =>
            {
                user.ToTable(nameof(User).ToUpper());
            });
            return modelBuilder;
        }

        internal static ModelBuilder AddDomainConfigurations(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType == typeof(Account))
                {
                    entity.SetTableName(nameof(entity).ToUpper());
                    foreach(var property in entity.GetProperties())
                    {
                        entity.SetTableName(nameof(property).ToUpper());
                    }
                }
            }
            return modelBuilder;
        }
    }
}
