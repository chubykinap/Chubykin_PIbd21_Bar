namespace BarService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cocktails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CocktailName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ElementRequirements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CocktailID = c.Int(nullable: false),
                        ElementID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cocktails", t => t.CocktailID, cascadeDelete: true)
                .ForeignKey("dbo.Elements", t => t.ElementID, cascadeDelete: true)
                .Index(t => t.CocktailID)
                .Index(t => t.ElementID);
            
            CreateTable(
                "dbo.Elements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ElementName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ElementStorages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StorageID = c.Int(nullable: false),
                        ElementID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Elements", t => t.ElementID, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageID, cascadeDelete: true)
                .Index(t => t.StorageID)
                .Index(t => t.ElementID);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StorageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        CocktailID = c.Int(nullable: false),
                        ExecutorID = c.Int(),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cocktails", t => t.CocktailID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Executors", t => t.ExecutorID)
                .Index(t => t.CustomerID)
                .Index(t => t.CocktailID)
                .Index(t => t.ExecutorID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                        Mail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Executors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExecutorFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ExecutorID", "dbo.Executors");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.MessageInfoes", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CocktailID", "dbo.Cocktails");
            DropForeignKey("dbo.ElementStorages", "StorageID", "dbo.Storages");
            DropForeignKey("dbo.ElementStorages", "ElementID", "dbo.Elements");
            DropForeignKey("dbo.ElementRequirements", "ElementID", "dbo.Elements");
            DropForeignKey("dbo.ElementRequirements", "CocktailID", "dbo.Cocktails");
            DropIndex("dbo.MessageInfoes", new[] { "CustomerID" });
            DropIndex("dbo.Orders", new[] { "ExecutorID" });
            DropIndex("dbo.Orders", new[] { "CocktailID" });
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropIndex("dbo.ElementStorages", new[] { "ElementID" });
            DropIndex("dbo.ElementStorages", new[] { "StorageID" });
            DropIndex("dbo.ElementRequirements", new[] { "ElementID" });
            DropIndex("dbo.ElementRequirements", new[] { "CocktailID" });
            DropTable("dbo.Executors");
            DropTable("dbo.MessageInfoes");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.Storages");
            DropTable("dbo.ElementStorages");
            DropTable("dbo.Elements");
            DropTable("dbo.ElementRequirements");
            DropTable("dbo.Cocktails");
        }
    }
}
