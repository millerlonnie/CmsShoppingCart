namespace CmsShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ProductVMs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryName = c.String(),
                        CategoryId = c.Int(nullable: false),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
