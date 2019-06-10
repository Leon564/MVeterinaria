namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userRol2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "ApplicationUserRol_Id", "dbo.ApplicationUserRols");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUserRol_Id", "dbo.ApplicationUserRols");
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUserRol_Id", "dbo.ApplicationUserRols");
            DropIndex("dbo.AspNetUserClaims", new[] { "ApplicationUserRol_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUserRol_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUserRol_Id" });
            AddColumn("dbo.AspNetUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUserClaims", "ApplicationUserRol_Id");
            DropColumn("dbo.AspNetUserLogins", "ApplicationUserRol_Id");
            DropColumn("dbo.AspNetUserRoles", "ApplicationUserRol_Id");
            DropTable("dbo.ApplicationUserRols");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserRols",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUserRoles", "ApplicationUserRol_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserLogins", "ApplicationUserRol_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserClaims", "ApplicationUserRol_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUserRoles", "Discriminator");
            CreateIndex("dbo.AspNetUserRoles", "ApplicationUserRol_Id");
            CreateIndex("dbo.AspNetUserLogins", "ApplicationUserRol_Id");
            CreateIndex("dbo.AspNetUserClaims", "ApplicationUserRol_Id");
            AddForeignKey("dbo.AspNetUserRoles", "ApplicationUserRol_Id", "dbo.ApplicationUserRols", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "ApplicationUserRol_Id", "dbo.ApplicationUserRols", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "ApplicationUserRol_Id", "dbo.ApplicationUserRols", "Id");
        }
    }
}
