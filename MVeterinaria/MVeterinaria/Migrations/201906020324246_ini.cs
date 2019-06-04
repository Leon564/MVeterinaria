namespace MVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        CitaId = c.Int(nullable: false, identity: true),
                        FechaEmision = c.String(),
                        FechaCita = c.String(),
                        MascotaId = c.Int(nullable: false),
                        VeterinarioId = c.Int(nullable: false),
                        EstadoCitaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.EstadoCitas", t => t.EstadoCitaId, cascadeDelete: true)
                .ForeignKey("dbo.Mascotas", t => t.MascotaId, cascadeDelete: true)
                .ForeignKey("dbo.Veterinarios", t => t.VeterinarioId, cascadeDelete: true)
                .Index(t => t.MascotaId)
                .Index(t => t.VeterinarioId)
                .Index(t => t.EstadoCitaId);
            
            CreateTable(
                "dbo.EstadoCitas",
                c => new
                    {
                        EstadoCitaId = c.Int(nullable: false, identity: true),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.EstadoCitaId);
            
            CreateTable(
                "dbo.Mascotas",
                c => new
                    {
                        MascotaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        RazaId = c.Int(nullable: false),
                        SexoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MascotaId)
                .ForeignKey("dbo.Razas", t => t.RazaId, cascadeDelete: true)
                .ForeignKey("dbo.Sexoes", t => t.SexoId, cascadeDelete: true)
                .Index(t => t.RazaId)
                .Index(t => t.SexoId);
            
            CreateTable(
                "dbo.Razas",
                c => new
                    {
                        RazaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.RazaId);
            
            CreateTable(
                "dbo.Sexoes",
                c => new
                    {
                        SexoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.SexoId);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Citas", "VeterinarioId", "dbo.Veterinarios");
            DropForeignKey("dbo.Citas", "MascotaId", "dbo.Mascotas");
            DropForeignKey("dbo.Mascotas", "SexoId", "dbo.Sexoes");
            DropForeignKey("dbo.Mascotas", "RazaId", "dbo.Razas");
            DropForeignKey("dbo.Citas", "EstadoCitaId", "dbo.EstadoCitas");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Mascotas", new[] { "SexoId" });
            DropIndex("dbo.Mascotas", new[] { "RazaId" });
            DropIndex("dbo.Citas", new[] { "EstadoCitaId" });
            DropIndex("dbo.Citas", new[] { "VeterinarioId" });
            DropIndex("dbo.Citas", new[] { "MascotaId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Veterinarios");
            DropTable("dbo.Sexoes");
            DropTable("dbo.Razas");
            DropTable("dbo.Mascotas");
            DropTable("dbo.EstadoCitas");
            DropTable("dbo.Citas");
        }
    }
}
