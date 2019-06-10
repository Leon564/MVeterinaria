namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nose : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boletas",
                c => new
                    {
                        BoletaId = c.Int(nullable: false, identity: true),
                        CitaId = c.Int(nullable: false),
                        Diagnostico = c.String(),
                        Comentarios = c.String(),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BoletaId)
                .ForeignKey("dbo.Citas", t => t.CitaId, cascadeDelete: true)
                .Index(t => t.CitaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boletas", "CitaId", "dbo.Citas");
            DropIndex("dbo.Boletas", new[] { "CitaId" });
            DropTable("dbo.Boletas");
        }
    }
}
