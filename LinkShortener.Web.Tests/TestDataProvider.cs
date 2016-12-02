using System;
using System.Collections.Generic;
using LinkShortener.Data.Entities;

namespace LinkShortener.Web.Tests
{
    public class TestDataProvider
    {
        private const string TokenForTest = "3nfdO";
        private const string UrlForTest = "http://longurl1.com/";

        public List<Link> GetLinks()
        {
            return new List <Link>
            {
                new Link
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.AddDays(-3),
                    OriginalUrl = UrlForTest,
                    Token = TokenForTest,
                    ClickOnLinkCounter = 100
                },
                new Link
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    OriginalUrl = "http://longurl2.com/",
                    Token = "beE32",
                    ClickOnLinkCounter = 3
                },
                new Link
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.AddDays(-1),
                    OriginalUrl = "http://longurl3.com/",
                    Token = "Ui2Na",
                    ClickOnLinkCounter = 11
                },
                new Link
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.AddDays(-2),
                    OriginalUrl = "http://longurl4.com/",
                    Token = "O3dIq",
                    ClickOnLinkCounter = 24
                }
            };
        }

        public string GetToken()
        {
            return TokenForTest;
        }

        public string GetUrl()
        {
            return UrlForTest;
        }
    }
}
