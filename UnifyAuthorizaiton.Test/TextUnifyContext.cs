using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnifyAuthorization;
using UnifyAuthorization.Domain;
using System.Linq;

namespace UnifyAuthorizaiton.Test
{
    [TestClass]
    public class TestUnifyContext
    {
        [TestMethod]
        public void TestContextCreate()
        {
            using (var db = new UnifyContext())
            {
                db.Database.CreateIfNotExists();
                Assert.IsTrue(db.Database.Exists());
            }
        }
        [TestMethod]
        public void TestInsert()
        {
            using (var db = new UnifyContext())
            {
                db.Database.CreateIfNotExists();
                var Company = new Company("LFG", "LGPMS", "https://www.bing.com", "Test");
                db.Set<Company>().Add(Company);
                //var Company = db.Set<Company>().First();
                var User = new User(Company.Id, "IT", "12345678", "Tim", "123456", "Programming", "15234592255", "94545159867@qq.com", null);
                db.Set<User>().Add(User);
                //var User = db.Set<User>().First();
                var Role = new Role(Company.Id, "Admin", "Admin", User.Id);
                db.Set<Role>().Add(Role);
                //var Role = db.Set<Role>().First();
                var Function = new Function("Pms", "Project", "项目", "View", "View", User.Id);
                db.Set<Function>().Add(Function);
                var RoleUser = new RoleUser(Role.Id, User.Id, User.Id);
                db.Set<RoleUser>().Add(RoleUser);
                var RolePermission = new RolePermission(Role.Id, Function.Id, User.Id);
                db.Set<RolePermission>().Add(RolePermission);
                var UserPermission = new UserPermission(User.Id, Function.Id, User.Id);
                db.Set<UserPermission>().Add(UserPermission);
                Assert.IsTrue(db.SaveChanges() > 1);
                var Company1 = db.Set<Company>().Where(p => p.Id == Company.Id).First();
                db.Set<Company>().Remove(Company);
                db.SaveChanges();
                var Role1 = db.Set<Role>().Find(Role.Id);
                Assert.IsNull(Role1);
            }
        }
    }
}
