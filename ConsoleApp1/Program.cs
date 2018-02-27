using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyAuthorization;
using UnifyAuthorization.Domain;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new UnifyContext())
            {
                var company = db.Set<Company>().First();
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
                var Function = new FunctionModel("Pms", "Project", "项目", "View", "View", User.Id);
                db.Set<FunctionModel>().Add(Function);
                var RoleUser = new RoleUser(Role.Id, User.Id, User.Id);
                db.Set<RoleUser>().Add(RoleUser);
                var RolePermission = new RolePermission(Role.Id, Function.Id, User.Id);
                db.Set<RolePermission>().Add(RolePermission);
                db.SaveChanges();
                Console.WriteLine("Save successful");
                Console.ReadLine();
            }

        }
    }
}
