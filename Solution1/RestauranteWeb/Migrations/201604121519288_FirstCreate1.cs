namespace RestauranteWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prato", "PrecoPrato", c => c.Double(nullable: false));
            DropColumn("dbo.Prato", "PreçoPrato");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prato", "PreçoPrato", c => c.Double(nullable: false));
            DropColumn("dbo.Prato", "PrecoPrato");
        }
    }
}
