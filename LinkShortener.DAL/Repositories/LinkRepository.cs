using LinkShortener.Data.Contracts;
using LinkShortener.Data.Entities;
using LinkShortener.DAL.Contracts;

namespace LinkShortener.DAL.Repositories
{
    public class LinkRepository : BaseRepository<Link>, ILinkRepository
    {
        public LinkRepository(ILinkShortenerDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
