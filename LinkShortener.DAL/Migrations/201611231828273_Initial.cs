namespace LinkShortener.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OriginalUrl = c.String(nullable: false),
                        Token = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ClickOnLinkCounter = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Links");
        }
    }
}
