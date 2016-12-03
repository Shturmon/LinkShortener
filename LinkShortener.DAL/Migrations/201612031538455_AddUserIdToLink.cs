namespace LinkShortener.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "UserId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Links", "UserId");
        }
    }
}
