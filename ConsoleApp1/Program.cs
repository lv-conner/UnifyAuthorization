using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization;
using Lfg.UnifyAuthorization.Domain;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            using (var db = new UnifyContext())
            {
                //Console.WriteLine(db.Database.CreateIfNotExists());
                var User = db.Set<User>().Include("Company").First();
                var Role = db.Set<Role>().First();
                var company = db.Set<Company>().First();
                db.Set<Company>().Remove(company);
                db.SaveChanges();
                Console.ReadKey();
            }
        }

        static void Init()
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
                db.SaveChanges();
                Console.WriteLine("Save successful");
                Console.ReadLine();
            }

        }
    }
}
