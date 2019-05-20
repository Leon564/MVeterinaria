namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class segu : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Boletas", "FechaActual");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Boletas", "FechaActual", c => c.DateTime(nullable: false));
        }
    }
}
