using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using LinkShortener.Web.Services.Contracts;

namespace LinkShortener.Web.Services
{
    public class CookieService : ICookieService
    {
        private const string UserIdCookieName = "userId";

        public Guid GetUserId(HttpRequestMessage request)
        {
            Guid userId;
            var cookie = request.Headers.GetCookies(UserIdCookieName).FirstOrDefault();
            if (cookie != null && Guid.TryParse(cookie[UserIdCookieName].Value, out userId))
                return userId;

            return Guid.Empty;
        }

        public void StoreUserId(HttpResponseMessage response, string host, Guid userId)
        {
            var cookie = new CookieHeaderValue(UserIdCookieName, userId.ToString())
            {
                Expires = DateTimeOffset.Now.AddYears(1),
                Domain = host,
                Path = "/"
            };

            response.Headers.AddCookies(new[] { cookie });
        }
    }
}