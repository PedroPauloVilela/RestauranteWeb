namespace RestauranteWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prato",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomePrato = c.String(nullable: false, maxLength: 30),
                        PreçoPrato = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Restaurante",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomeRestaurante = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RestaurantePrato",
                c => new
                    {
                        Restaurante_ID = c.Int(nullable: false),
                        Prato_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Restaurante_ID, t.Prato_ID })
                .ForeignKey("dbo.Restaurante", t => t.Restaurante_ID, cascadeDelete: true)
                .ForeignKey("dbo.Prato", t => t.Prato_ID, cascadeDelete: true)
                .Index(t => t.Restaurante_ID)
                .Index(t => t.Prato_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantePrato", "Prato_ID", "dbo.Prato");
            DropForeignKey("dbo.RestaurantePrato", "Restaurante_ID", "dbo.Restaurante");
            DropIndex("dbo.RestaurantePrato", new[] { "Prato_ID" });
            DropIndex("dbo.RestaurantePrato", new[] { "Restaurante_ID" });
            DropTable("dbo.RestaurantePrato");
            DropTable("dbo.Restaurante");
            DropTable("dbo.Prato");
        }
    }
}
