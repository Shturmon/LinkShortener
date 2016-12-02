using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.BLL.Business.Base;
using LinkShortener.BLL.Contracts;
using LinkShortener.BLL.Models;
using LinkShortener.Data.Entities;
using LinkShortener.DAL.Contracts;

namespace LinkShortener.BLL.Business
{
    public class LinkBusiness : BaseBusiness, ILinkBusiness
    {
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public LinkBusiness(IUnitOfWork unitOfWork, ITokenGeneratorService tokenGeneratorService)
            : base(unitOfWork)
        {
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<List<LinkModel>> GetLinks()
        {
            return await UnitOfWork.LinkRepository.All.Select(l => new LinkModel
            {
                Id = l.Id,
                OriginalUrl = l.OriginalUrl,
                Token = l.Token,
                CreatedDate = l.CreatedDate,
                ClickOnLinkCounter = l.ClickOnLinkCounter
            }).ToListAsync();
        }

        public async Task<string> AddLink(string url)
        {
            var link = new Link
            {
                CreatedDate = DateTime.Now,
                OriginalUrl = url,
            };

            bool tokenExists;
            string token;
            do
            {
                token = _tokenGeneratorService.GenerateToken(5);
                tokenExists = await UnitOfWork.LinkRepository.All.AnyAsync(l => l.Token == token);
            } while (tokenExists);

            link.Token = token;

            UnitOfWork.LinkRepository.Add(link);
            await UnitOfWork.SaveChangesAsync();

            return token;
        }

        public async Task<string> GetOriginalLink(string token)
        {
            var link = await UnitOfWork.LinkRepository.All.FirstOrDefaultAsync(l => l.Token == token);
            if (link == null)
                return string.Empty;

            await SaveLinkClicked(link);
            return link.OriginalUrl;
        }

        private async Task SaveLinkClicked(Link link)
        {
            link.ClickOnLinkCounter++;

            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    UnitOfWork.LinkRepository.Update(link);
                    await UnitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);
        }
    }
}
