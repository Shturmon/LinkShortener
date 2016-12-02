using System.Threading.Tasks;
using LinkShortener.Data.Contracts;
using LinkShortener.DAL.Contracts;

namespace LinkShortener.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILinkShortenerDbContext Context { get; }

        public ILinkRepository LinkRepository { get; set; }

        public UnitOfWork(ILinkShortenerDbContext context)
        {
            Context = context;
        }

        public async Task SaveChangesAsync()
        {
            await Context.DbContext.SaveChangesAsync();
        }
    }
}
