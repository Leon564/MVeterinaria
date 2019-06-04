namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixmasc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mascotas", "ClientId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Mascotas", "ClientId");
            AddForeignKey("dbo.Mascotas", "ClientId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mascotas", "ClientId", "dbo.AspNetUsers");
            DropIndex("dbo.Mascotas", new[] { "ClientId" });
            DropColumn("dbo.Mascotas", "ClientId");
        }
    }
}
