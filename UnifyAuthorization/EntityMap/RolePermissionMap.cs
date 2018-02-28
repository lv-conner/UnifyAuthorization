using System.Data.Entity.ModelConfiguration;
using UnifyAuthorization.Domain;

namespace UnifyAuthorization.EntityMap
{
    public class RolePermissionMap : EntityTypeConfiguration<RolePermission>
    {
        public RolePermissionMap()
        {
            ToTable("RolePermission").HasKey(p => p.Id);
            ConfigureRelationShip();
        }
        public void ConfigureProperty()
        {
            Property(p => p.RowVersion).IsRowVersion();
        }
        private void ConfigureRelationShip()
        {
            HasRequired(p => p.Role).WithMany(p => p.RolePermissions).HasForeignKey(p => p.RoleId).WillCascadeOnDelete(true);
            HasRequired(p => p.Function).WithMany(p => p.RolePermissions).HasForeignKey(p => p.FunctionId).WillCascadeOnDelete(true);
        }
    }
}