namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class citasState2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Citas", "Estado_EstadoCitaId", "dbo.EstadoCitas");
            DropIndex("dbo.Citas", new[] { "Estado_EstadoCitaId" });
            RenameColumn(table: "dbo.Citas", name: "Estado_EstadoCitaId", newName: "EstadoCitaId");
            AlterColumn("dbo.Citas", "EstadoCitaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Citas", "EstadoCitaId");
            AddForeignKey("dbo.Citas", "EstadoCitaId", "dbo.EstadoCitas", "EstadoCitaId", cascadeDelete: true);
            DropColumn("dbo.Citas", "EstadoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Citas", "EstadoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Citas", "EstadoCitaId", "dbo.EstadoCitas");
            DropIndex("dbo.Citas", new[] { "EstadoCitaId" });
            AlterColumn("dbo.Citas", "EstadoCitaId", c => c.Int());
            RenameColumn(table: "dbo.Citas", name: "EstadoCitaId", newName: "Estado_EstadoCitaId");
            CreateIndex("dbo.Citas", "Estado_EstadoCitaId");
            AddForeignKey("dbo.Citas", "Estado_EstadoCitaId", "dbo.EstadoCitas", "EstadoCitaId");
        }
    }
}
