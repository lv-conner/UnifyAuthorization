using System.Data.Entity.ModelConfiguration;
using UnifyAuthorization.Domain;

namespace UnifyAuthorization.EntityMap
{
    public class UserMap:EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User").HasKey(p => p.Id);
            ConfigureProperty();
            ConfigureRelationShip();
        }
        private void ConfigureProperty()
        {
            Property(p => p.CompanyId).IsRequired();
            Property(p => p.Department).HasMaxLength(50).IsUnicode();
            Property(p => p.UserNo).IsRequired().HasMaxLength(20);
            Property(p => p.UserName).IsRequired().HasMaxLength(20).IsUnicode();
            Property(p => p.Duty).HasMaxLength(20).IsUnicode();
            Property(p => p.Password).IsRequired().HasMaxLength(18);
            Property(p => p.EmailAddress).HasMaxLength(255);
            Property(p => p.PhoneNo).HasMaxLength(50);
        }
        private void ConfigureRelationShip()
        {
            HasMany(p => p.RoleUsers).WithRequired(p => p.User).HasForeignKey(p => p.UserId).WillCascadeOnDelete(true);
            HasMany(p => p.UserPermission).WithRequired(p => p.User).HasForeignKey(p => p.UserId).WillCascadeOnDelete(true);
        }
    }
}