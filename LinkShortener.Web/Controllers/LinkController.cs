using System.Threading.Tasks;
using System.Web.Http;
using LinkShortener.BLL.Contracts;

namespace LinkShortener.Web.Controllers
{
    [RoutePrefix("api")]
    public class LinkController : ApiController
    {
        private readonly ILinkBusiness _linkBusiness;

        public LinkController(ILinkBusiness linkBusiness)
        {
            _linkBusiness = linkBusiness;
        }

        [HttpGet]
        [Route("short-link")]
        public async Task<IHttpActionResult> ShortLink(string url)
        {
            var token = await _linkBusiness.AddLink(url);
            return Ok(FormatUrl(token));
        }

        [HttpGet]
        [Route("links")]
        public async Task<IHttpActionResult> Links()
        {
            var links = await _linkBusiness.GetLinks();
            links.ForEach(l => l.ShortUrl = FormatUrl(l.Token));
            return Ok(links);
        }

        [HttpGet]
        public async Task<IHttpActionResult> RedirectLink(string token)
        {
            var url = await _linkBusiness.GetOriginalLink(token);
            return Redirect(url);
        }

        private string FormatUrl(string token)
        {
            return Url.Link("RedirectApiLink", new { token = token });
        }
    }
}
