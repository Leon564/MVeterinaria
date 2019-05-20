namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boletas", "FechaActual", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Boletas", "FechaActual");
        }
    }
}
