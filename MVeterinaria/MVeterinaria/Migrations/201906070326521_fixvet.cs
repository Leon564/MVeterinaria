namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixvet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Citas", "VeterinarioId", "dbo.Veterinarios");
            DropIndex("dbo.Citas", new[] { "VeterinarioId" });
            DropColumn("dbo.Citas", "VeterinarioId");
            DropTable("dbo.Veterinarios");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Veterinarios",
                c => new
                    {
                        VeterinarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                    })
                .PrimaryKey(t => t.VeterinarioId);
            
            AddColumn("dbo.Citas", "VeterinarioId", c => c.Int(nullable: false));
            CreateIndex("dbo.Citas", "VeterinarioId");
            AddForeignKey("dbo.Citas", "VeterinarioId", "dbo.Veterinarios", "VeterinarioId", cascadeDelete: true);
        }
    }
}
