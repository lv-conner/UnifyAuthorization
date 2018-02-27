namespace UnifyAuthorization.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UnifyAuthorization.Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<UnifyAuthorization.UnifyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UnifyAuthorization.UnifyContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var Company = new Company("LFG", "LGPMS", "https://www.bing.com", "Test");
            context.Set<Company>().AddOrUpdate(Company);
            var User = new User(Company.Id, "IT", "12345678", "Tim", "123456", "Programming", "15234592255", "94545159867@qq.com", null);
            context.Set<User>().AddOrUpdate(User);
            var Role = new Role(Company.Id, "Admin", "Admin", User.Id);
            context.Set<Role>().AddOrUpdate(Role);
            var Function = new FunctionModel("Pms", "Project", "ÏîÄ¿", "View", "View", User.Id);
            context.Set<FunctionModel>().Add(Function);
            var RoleUser = new RoleUser(Role.Id, User.Id, User.Id);
            context.Set<RoleUser>().AddOrUpdate(RoleUser);
            var RolePermission = new RolePermission(Role.Id, Function.Id, User.Id);
            context.Set<RolePermission>().AddOrUpdate(RolePermission);
            context.SaveChanges();
        }
    }
}
