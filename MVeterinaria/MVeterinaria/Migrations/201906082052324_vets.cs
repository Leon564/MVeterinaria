namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Vet", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Vet");
        }
    }
}
