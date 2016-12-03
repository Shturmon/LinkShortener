using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using LinkShortener.BLL.Contracts;
using LinkShortener.Web.Services.Contracts;

namespace LinkShortener.Web.Controllers
{
    [RoutePrefix("api")]
    public class LinkController : ApiController
    {
        private readonly ILinkBusiness _linkBusiness;
        private readonly ICookieService _cookieService;

        public LinkController(ILinkBusiness linkBusiness, ICookieService cookieService)
        {
            _linkBusiness = linkBusiness;
            _cookieService = cookieService;
        }

        [HttpGet]
        [Route("short-link")]
        public async Task<IHttpActionResult> ShortLink(string url)
        {
            if (!IsValidUri(url))
                return BadRequest("Invalid Url");

            var userId = _cookieService.GetUserId(Request);
            if (userId == Guid.Empty)
                userId = Guid.NewGuid();

            var token = await _linkBusiness.AddLink(url, userId);

            var response = CreateResponse(token);
            _cookieService.StoreUserId(response, Request.RequestUri.Host, userId);
            return ResponseMessage(response);
        }

        [HttpGet]
        [Route("links")]
        public async Task<IHttpActionResult> Links()
        {
            var userId = _cookieService.GetUserId(Request);
            var links = await _linkBusiness.GetLinks(userId);
            links.ForEach(l => l.ShortUrl = FormatUrl(l.Token));
            return Ok(links);
        }

        [HttpGet]
        public async Task<IHttpActionResult> RedirectLink(string token)
        {
            var url = await _linkBusiness.GetOriginalLink(token);
            if (string.IsNullOrEmpty(url))
                url = Request.RequestUri.GetLeftPart(UriPartial.Authority);

            return Redirect(url);
        }

        private string FormatUrl(string token)
        {
            return Url.Link("RedirectApiLink", new { token = token });
        }

        private static bool IsValidUri(string uri)
        {
            Uri validatedUri;
            return Uri.TryCreate(uri, UriKind.RelativeOrAbsolute, out validatedUri);
        }

        private HttpResponseMessage CreateResponse(string token)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<string>(FormatUrl(token), new JsonMediaTypeFormatter())
            };
        }
    }
}
