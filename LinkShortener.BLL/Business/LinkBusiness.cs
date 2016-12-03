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
        private readonly INumberEncodingService _numberEncodingService;

        public LinkBusiness(IUnitOfWork unitOfWork, INumberEncodingService numberEncodingService)
            : base(unitOfWork)
        {
            _numberEncodingService = numberEncodingService;
        }

        public async Task<List<LinkModel>> GetLinks(Guid userId)
        {
            var links = await UnitOfWork.LinkRepository.All
                .Where(l => l.UserId == userId)
                .Select(l => new LinkModel
            {
                Id = l.Id,
                OriginalUrl = l.OriginalUrl,
                TokenNumber = l.TokenNumber,
                CreatedDate = l.CreatedDate,
                ClickOnLinkCounter = l.ClickOnLinkCounter
            }).ToListAsync();

            links.ForEach(l => l.Token = _numberEncodingService.Encode(l.TokenNumber));
            return links;
        }

        public async Task<string> AddLink(string url, Guid userId)
        {
            var link = await UnitOfWork.LinkRepository.All.FirstOrDefaultAsync(l => l.OriginalUrl == url);
            if (link != null)
                return _numberEncodingService.Encode(link.TokenNumber);

            link = new Link
            {
                CreatedDate = DateTime.Now,
                OriginalUrl = url,
                UserId = userId
            };

            UnitOfWork.LinkRepository.Add(link);
            await UnitOfWork.SaveChangesAsync();

            return _numberEncodingService.Encode(link.TokenNumber);
        }

        public async Task<string> GetOriginalLink(string token)
        {
            var tokenNumber = _numberEncodingService.Decode(token);
            var link = await UnitOfWork.LinkRepository.All
                    .FirstOrDefaultAsync(l => l.TokenNumber == tokenNumber);
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
