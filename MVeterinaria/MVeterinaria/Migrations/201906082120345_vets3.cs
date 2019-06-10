namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vets3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.veterinarios",
                c => new
                    {
                        VeterinarioId = c.Int(nullable: false, identity: true),
                        ClientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VeterinarioId)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.veterinarios", "ClientId", "dbo.AspNetUsers");
            DropIndex("dbo.veterinarios", new[] { "ClientId" });
            DropTable("dbo.veterinarios");
        }
    }
}
