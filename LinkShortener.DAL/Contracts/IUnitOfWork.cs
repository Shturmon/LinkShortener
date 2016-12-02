using System.Threading.Tasks;
using LinkShortener.Data.Contracts;

namespace LinkShortener.DAL.Contracts
{
    public interface IUnitOfWork
    {
        ILinkShortenerDbContext Context { get; }
        Task SaveChangesAsync();

        ILinkRepository LinkRepository { get; set; }
    }
}
