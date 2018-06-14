namespace InvoicingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddpriceinOrderLine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLines", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderLines", "Price");
        }
    }
}
