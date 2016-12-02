using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using LinkShortener.BLL.Business;
using LinkShortener.BLL.Models;
using LinkShortener.BLL.Services;
using LinkShortener.Data.Contracts;
using LinkShortener.Data.Entities;
using LinkShortener.DAL.Contracts;
using LinkShortener.Web.Controllers;
using LinkShortener.Web.Tests.TestDbProviders;
using Moq;
using NUnit.Framework;

namespace LinkShortener.Web.Tests.Controllers
{
    [TestFixture]
    public class LinkControllerTest
    {
        private LinkController _linkController;
        private TestDataProvider _testDataProvider;
        private IQueryable<Link> _links;

        [OneTimeSetUp]
        public void SetUp()
        {
            _testDataProvider = new TestDataProvider();
            _links = _testDataProvider.GetLinks().AsQueryable();

            var linkRepository = new Mock<ILinkRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var urlHepler = new Mock<UrlHelper>();

            urlHepler.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(
                (string name, object values) =>
                {
                    var token = values.GetType().GetProperty("token");
                    return $"http://hostname/{token.GetValue(values, null)}";
                });

            var mockDbSet = MockDbSet();

            linkRepository.Setup(r => r.Add(It.IsAny<Link>()));
            linkRepository.Setup(r => r.Update(It.IsAny<Link>()));
            linkRepository.Setup(r => r.All).Returns(mockDbSet.Object);

            unitOfWork.Setup(u => u.LinkRepository).Returns(linkRepository.Object);
            unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
            
            var tokenGenerator = new TokenGeneratorService();
            var linkBusiness = new LinkBusiness(unitOfWork.Object, tokenGenerator);
            _linkController = new LinkController(linkBusiness) { Url = urlHepler.Object };
        }

        private Mock<DbSet<Link>> MockDbSet()
        {
            // For EF async methods
            var mockDbSet = new Mock<DbSet<Link>>();
            mockDbSet.As<IDbAsyncEnumerable<Link>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Link>(_links.GetEnumerator()));

            mockDbSet.As<IQueryable<Link>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Link>(_links.Provider));

            mockDbSet.As<IQueryable<Link>>().Setup(m => m.Expression).Returns(_links.Expression);
            mockDbSet.As<IQueryable<Link>>().Setup(m => m.ElementType).Returns(_links.ElementType);
            mockDbSet.As<IQueryable<Link>>().Setup(m => m.GetEnumerator()).Returns(_links.GetEnumerator());
            return mockDbSet;
        }

        [Test]
        public async Task ShortLink()
        {
            const string url = "http://very.long.url.ru";

            var result = await _linkController.ShortLink(url);

            var okResult = result as OkNegotiatedContentResult<string>;

            Assert.That(okResult, Is.Not.Null);
        }

        [Test]
        public async Task Should_Return_Links()
        {
            var result = await _linkController.Links();

            var okResult = result as OkNegotiatedContentResult<List<LinkModel>>;

            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Content.Count, Is.EqualTo(4));
        }

        [Test]
        public async Task Redirect()
        {
            var token = _testDataProvider.GetToken();
            var url = _testDataProvider.GetUrl();

            var result = await _linkController.RedirectLink(token);
            var okResult = result as RedirectResult;

            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Location.AbsoluteUri, Is.EqualTo(url));
        }
    }
}
