using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyAuthorization.Domain;

namespace UnifyAuthorization.EntityMap
{
    public class FunctionModelMap: EntityTypeConfiguration<FunctionModel>
    {
        public FunctionModelMap()
        {
            ToTable("FunctionModel").HasKey(p => p.Id);
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
            HasMany(p => p.RolePermission).WithRequired();
        }
    }
}
