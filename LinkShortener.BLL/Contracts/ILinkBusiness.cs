using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkShortener.BLL.Models;

namespace LinkShortener.BLL.Contracts
{
    public interface ILinkBusiness
    {
        Task<List<LinkModel>> GetLinks(Guid userId);
        Task<string> AddLink(string url, Guid userId);
        Task<string> GetOriginalLink(string token);
    }
}
