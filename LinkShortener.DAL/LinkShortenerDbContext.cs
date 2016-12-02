using System.Data.Entity;
using LinkShortener.Data.Entities;
using LinkShortener.DAL.Contracts;
using LinkShortener.DAL.Maps;

namespace LinkShortener.DAL
{
    public class LinkShortenerDbContext : DbContext, ILinkShortenerDbContext
    {
        public LinkShortenerDbContext()
            : base("LinkShortenerDbContext")
        {
        }

        public DbContext DbContext => this;

        public static LinkShortenerDbContext Create()
        {
            return new LinkShortenerDbContext();
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new LinkMap());
        }
    }
}
