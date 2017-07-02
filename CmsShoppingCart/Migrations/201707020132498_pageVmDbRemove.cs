namespace CmsShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pageVmDbRemove : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PageVMs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PageVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Slug = c.String(),
                        Body = c.String(nullable: false),
                        Sorting = c.Int(nullable: false),
                        HasSidebar = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
