namespace LinkShortener.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToLinkToken : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Links", "Token", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.Links", "Token", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Links", new[] { "Token" });
            AlterColumn("dbo.Links", "Token", c => c.String(nullable: false));
        }
    }
}
