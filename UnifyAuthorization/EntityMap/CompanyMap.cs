using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyAuthorization.Domain;

namespace UnifyAuthorization.EntityMap
{
    public class CompanyMap: EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            ToTable("Company").HasKey(p => p.Id);
            ConfigureProperty();
            ConfiureRelationship();
        }

        private void ConfigureProperty()
        {
            Property(p => p.Code).IsRequired().HasMaxLength(40);
            Property(p => p.Name).IsRequired().HasMaxLength(40);
            Property(p => p.Url).HasMaxLength(255);
            Property(p => p.RowVersion).IsRowVersion();

        }

        private void ConfiureRelationship()
        {
            HasMany(p => p.Users).WithRequired();
            HasMany(p => p.Roles).WithRequired();
        }
    }
}
