namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixvet2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Citas", "VetId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Citas", "VetId");
            AddForeignKey("dbo.Citas", "VetId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Citas", "VetId", "dbo.AspNetUsers");
            DropIndex("dbo.Citas", new[] { "VetId" });
            DropColumn("dbo.Citas", "VetId");
        }
    }
}
