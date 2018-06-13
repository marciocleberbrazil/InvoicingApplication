namespace InvoicingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addattributediscountinordersline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLines", "Discount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderLines", "Discount");
        }
    }
}
