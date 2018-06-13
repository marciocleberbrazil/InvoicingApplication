namespace InvoicingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressinCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FirstName", c => c.String());
            AddColumn("dbo.Customers", "LastName", c => c.String());
            AddColumn("dbo.Customers", "Address", c => c.String());
            AddColumn("dbo.Customers", "State", c => c.String());
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "PostCode", c => c.String());
            DropColumn("dbo.Customers", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Name", c => c.String());
            DropColumn("dbo.Customers", "PostCode");
            DropColumn("dbo.Customers", "City");
            DropColumn("dbo.Customers", "State");
            DropColumn("dbo.Customers", "Address");
            DropColumn("dbo.Customers", "LastName");
            DropColumn("dbo.Customers", "FirstName");
        }
    }
}
