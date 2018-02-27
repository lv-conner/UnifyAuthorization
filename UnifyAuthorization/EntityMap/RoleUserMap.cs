using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyAuthorization.Domain;

namespace UnifyAuthorization.EntityMap
{
    public class RoleUserMap: EntityTypeConfiguration<RoleUser>
    {
        public RoleUserMap()
        {
            ToTable("RoleUser").HasKey(p => p.Id);
            ConfigureProperty();
            ConfigureRelationShip();
        }
        private void ConfigureProperty()
        {
            Property(p => p.RowVersion).IsRowVersion();
        }
        private void ConfigureRelationShip()
        {
            HasRequired(p => p.Role).WithMany(p => p.Members).HasForeignKey(p => p.RoleId);
            HasRequired(p => p.User).WithMany(p => p.Roles).HasForeignKey(p => p.UserId);
        }
    }
}
