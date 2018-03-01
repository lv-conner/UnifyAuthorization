using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.EntityMap;

namespace Lfg.UnifyAuthorization
{
    public class UnifyContext: DbContext
    {
        public UnifyContext():base("UnifyContext")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new FunctionMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RolePermissionMap());
            modelBuilder.Configurations.Add(new RoleUserMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserPermissionMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
