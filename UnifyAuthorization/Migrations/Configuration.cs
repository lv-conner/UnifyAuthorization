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
            var User = new User(Company.Id, "IT", "12345678", "Tim", "123456", "Programming", "15234592255", "94545159867@qq.com", null);
            Company.SetAdministrator(User.Id);
            context.Set<Company>().AddOrUpdate(Company);
            context.Set<User>().AddOrUpdate(User);
            var Role = new Role(Company.Id, "Admin", "Admin", User.Id);
            context.Set<Role>().AddOrUpdate(Role);
            var Function = new Function("Pms", "Project", "ÏîÄ¿", "View", "View", User.Id);
            context.Set<Function>().Add(Function);
            var RoleUser = new RoleUser(Role.Id, User.Id, User.Id);
            context.Set<RoleUser>().AddOrUpdate(RoleUser);
            var RolePermission = new RolePermission(Role.Id, Function.Id, User.Id);
            context.Set<RolePermission>().AddOrUpdate(RolePermission);
            var UserPermission = new UserPermission(User.Id, Function.Id, User.Id);
            context.Set<UserPermission>().AddOrUpdate(UserPermission);
            context.SaveChanges();
        }
    }
}
