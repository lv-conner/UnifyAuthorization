using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Domain;

namespace Lfg.UnifyAuthorization.EntityMap
{
    public class UserPermissionMap:EntityTypeConfiguration<UserPermission>
    {
        public UserPermissionMap()
        {
            ToTable("UserPermission").HasKey(p => p.Id);
            ConfigureProperty();
            ConfigureRelationShip();
        }
        public void ConfigureProperty()
        {
            Property(p => p.RowVersion).IsRowVersion();
        }
        private void ConfigureRelationShip()
        {
            HasRequired(p => p.User).WithMany(p => p.UserPermission).HasForeignKey(p => p.UserId).WillCascadeOnDelete(true);
            HasRequired(p => p.Function).WithMany(p => p.UserPermissions).HasForeignKey(p => p.FunctionId).WillCascadeOnDelete(true);
        }
    }
}
