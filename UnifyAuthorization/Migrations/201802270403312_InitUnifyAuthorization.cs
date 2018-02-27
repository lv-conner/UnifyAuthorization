namespace UnifyAuthorization.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitUnifyAuthorization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 40),
                        Name = c.String(nullable: false, maxLength: 40),
                        Url = c.String(maxLength: 255),
                        Remark = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Administrator = c.Guid(nullable: false),
                        CreatorTime = c.DateTime(),
                        LastModifyUserId = c.Guid(),
                        LastModifyTime = c.DateTime(),
                        DeleteId = c.Guid(),
                        DeleteTime = c.DateTime(),
                        DeleteStaus = c.Boolean(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        RoleName = c.String(maxLength: 20),
                        Description = c.String(maxLength: 50),
                        CreateUserId = c.Guid(),
                        CreateTime = c.DateTime(),
                        LastModifyUserId = c.Guid(),
                        LastModifyTime = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.RoleUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreateUserId = c.Guid(),
                        CreateTime = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        Department = c.String(maxLength: 50),
                        UserNo = c.String(nullable: false, maxLength: 20),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Duty = c.String(maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 18),
                        EmailAddress = c.String(maxLength: 255),
                        PhoneNo = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        ActiveDate = c.DateTime(),
                        AccessFailedCount = c.Int(nullable: false),
                        AccessFailedDateTime = c.DateTime(),
                        LastLoginTime = c.DateTime(),
                        CreateUserId = c.Guid(),
                        CreateTime = c.DateTime(),
                        LastModifyUserId = c.Guid(),
                        LastModifyTime = c.DateTime(),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        ActionId = c.Guid(nullable: false),
                        CreateUserId = c.Guid(),
                        CreateTime = c.DateTime(),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FunctionModel", t => t.ActionId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ActionId);
            
            CreateTable(
                "dbo.FunctionModel",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SystemNo = c.String(maxLength: 50),
                        ModuleNo = c.String(maxLength: 50),
                        ModuleName = c.String(maxLength: 50),
                        ActionNo = c.String(maxLength: 50),
                        ActionName = c.String(maxLength: 50),
                        CreateUserId = c.Guid(),
                        CreateTime = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolePermission", "RoleId", "dbo.Role");
            DropForeignKey("dbo.RolePermission", "ActionId", "dbo.FunctionModel");
            DropForeignKey("dbo.RoleUser", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.RoleUser", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Role", "CompanyId", "dbo.Company");
            DropIndex("dbo.RolePermission", new[] { "ActionId" });
            DropIndex("dbo.RolePermission", new[] { "RoleId" });
            DropIndex("dbo.User", new[] { "CompanyId" });
            DropIndex("dbo.RoleUser", new[] { "UserId" });
            DropIndex("dbo.RoleUser", new[] { "RoleId" });
            DropIndex("dbo.Role", new[] { "CompanyId" });
            DropTable("dbo.FunctionModel");
            DropTable("dbo.RolePermission");
            DropTable("dbo.User");
            DropTable("dbo.RoleUser");
            DropTable("dbo.Role");
            DropTable("dbo.Company");
        }
    }
}
