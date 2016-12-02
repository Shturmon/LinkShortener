using System.Collections.Generic;
using System.Threading.Tasks;
using LinkShortener.BLL.Models;

namespace LinkShortener.BLL.Contracts
{
    public interface ILinkBusiness
    {
        Task<List<LinkModel>> GetLinks();
        Task<string> AddLink(string url);
        Task<string> GetOriginalLink(string token);
    }
}
