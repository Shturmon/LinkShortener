namespace LinkShortener.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTokenToIdentityNumber : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Links", new[] { "Token" });
            AddColumn("dbo.Links", "TokenNumber", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Links", "TokenNumber");
            DropColumn("dbo.Links", "Token");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Links", "Token", c => c.String(nullable: false, maxLength: 10));
            DropIndex("dbo.Links", new[] { "TokenNumber" });
            DropColumn("dbo.Links", "TokenNumber");
            CreateIndex("dbo.Links", "Token", unique: true);
        }
    }
}
