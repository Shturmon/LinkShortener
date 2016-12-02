namespace LinkShortener.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<LinkShortenerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LinkShortenerDbContext context)
        {
        }
    }
}
