using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Domain;

namespace Lfg.UnifyAuthorization.EntityMap
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
            HasRequired(p => p.User).WithMany(p => p.RoleUsers).HasForeignKey(p => p.UserId).WillCascadeOnDelete(true);
            HasRequired(p => p.Role).WithMany(p => p.RoleUsers).HasForeignKey(p => p.RoleId).WillCascadeOnDelete(false);
        }
    }
}
