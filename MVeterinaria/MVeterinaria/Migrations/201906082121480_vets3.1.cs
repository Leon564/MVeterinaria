namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vets31 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.veterinarios", name: "ClientId", newName: "UsId");
            RenameIndex(table: "dbo.veterinarios", name: "IX_ClientId", newName: "IX_UsId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.veterinarios", name: "IX_UsId", newName: "IX_ClientId");
            RenameColumn(table: "dbo.veterinarios", name: "UsId", newName: "ClientId");
        }
    }
}
