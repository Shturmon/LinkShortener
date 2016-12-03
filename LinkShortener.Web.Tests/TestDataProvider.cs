using System;
using System.Collections.Generic;
using LinkShortener.Data.Entities;

namespace LinkShortener.Web.Tests
{
    public class TestDataProvider
    {
        private const string TokenForTest = "b";
        private const string UrlForTest = "http://longurl1.com/";
        private readonly Guid _userIdTest = Guid.Parse("701238aa-f3fc-46ec-a189-89c39b617c42");

        public List<Link> GetLinks()
        {
            return new List <Link>
            {
                new Link
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.AddDays(-3),
                    OriginalUrl = UrlForTest,
                    TokenNumber = 1,
                    ClickOnLinkCounter = 100,
                    UserId = _userIdTest
                },
                new Link
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    OriginalUrl = "http://longurl2.com/",
                    TokenNumber = 2,
                    ClickOnLinkCounter = 3,
                    UserId = _userIdTest
                },
                new Link
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.AddDays(-1),
                    OriginalUrl = "http://longurl3.com/",
                    TokenNumber = 3,
                    ClickOnLinkCounter = 11,
                    UserId = _userIdTest
                },
                new Link
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.AddDays(-2),
                    OriginalUrl = "http://longurl4.com/",
                    TokenNumber = 4,
                    ClickOnLinkCounter = 24,
                    UserId = _userIdTest
                }
            };
        }

        public Guid GetUserId()
        {
            return _userIdTest;
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
