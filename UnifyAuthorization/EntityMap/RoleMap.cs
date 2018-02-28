using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyAuthorization.Domain;

namespace UnifyAuthorization.EntityMap
{
    public class RoleMap: EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Role").HasKey(p => p.Id);
            ConfigureProperty();
            ConfigureRelationShip();
        }
        private void ConfigureProperty()
        {
            Property(p => p.RoleName).HasMaxLength(20);
            Property(p => p.Description).HasMaxLength(50);
            Property(p => p.RowVersion).IsRowVersion();
        }
        private void ConfigureRelationShip()
        {
            HasMany(p => p.RoleUsers).WithRequired(p=>p.Role).HasForeignKey(p=>p.RoleId).WillCascadeOnDelete(false);
            HasMany(p => p.RolePermissions).WithRequired(p=>p.Role).HasForeignKey(p=>p.RoleId).WillCascadeOnDelete(true);
        }
    }
}
