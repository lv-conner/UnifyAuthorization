using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyAuthorization.Domain;

namespace UnifyAuthorization.EntityMap
{
    public class FunctionMap: EntityTypeConfiguration<Function>
    {
        public FunctionMap()
        {
            ToTable("Function").HasKey(p => p.Id);
            ConfigureProperty();
            ConfiureRelationship();
        }

        private void ConfigureProperty()
        {
            Property(p => p.SystemNo).HasMaxLength(50);
            Property(p => p.ModuleNo).HasMaxLength(50);
            Property(p => p.ModuleName).HasMaxLength(50);
            Property(p => p.ActionNo).HasMaxLength(50);
            Property(p => p.ActionName).HasMaxLength(50);
            Property(p => p.RowVersion).IsRowVersion();
        }

        private void ConfiureRelationship()
        {
            HasMany(p => p.UserPermissions).WithRequired(p => p.Function).HasForeignKey(p => p.FunctionId).WillCascadeOnDelete(true);
            HasMany(p => p.RolePermissions).WithRequired(p => p.Function).HasForeignKey(p => p.FunctionId).WillCascadeOnDelete(true);
        }
    }
}
