namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class citasState : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstadoCitas",
                c => new
                    {
                        EstadoCitaId = c.Int(nullable: false, identity: true),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.EstadoCitaId);
            
            AddColumn("dbo.Citas", "EstadoId", c => c.Int(nullable: false));
            AddColumn("dbo.Citas", "Estado_EstadoCitaId", c => c.Int());
            CreateIndex("dbo.Citas", "Estado_EstadoCitaId");
            AddForeignKey("dbo.Citas", "Estado_EstadoCitaId", "dbo.EstadoCitas", "EstadoCitaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Citas", "Estado_EstadoCitaId", "dbo.EstadoCitas");
            DropIndex("dbo.Citas", new[] { "Estado_EstadoCitaId" });
            DropColumn("dbo.Citas", "Estado_EstadoCitaId");
            DropColumn("dbo.Citas", "EstadoId");
            DropTable("dbo.EstadoCitas");
        }
    }
}
