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
            //HasMany:一个公司下有多个用户，WithRequired：一个用户一定属于一个公司，HasForeignKey：指定用户表中的外键，WillCascadeOnDelete：启用级联删除。
            HasMany(p => p.Users).WithRequired(p => p.Company).HasForeignKey(p=>p.CompanyId).WillCascadeOnDelete(true);
            HasMany(p => p.Roles).WithRequired(p => p.Company).HasForeignKey(p=>p.CompanyId).WillCascadeOnDelete(true);
        }
    }
}
