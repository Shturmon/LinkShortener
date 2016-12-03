using System;
using System.Net.Http;

namespace LinkShortener.Web.Services.Contracts
{
    public interface ICookieService
    {
        Guid GetUserId(HttpRequestMessage request);
        void StoreUserId(HttpResponseMessage response, string host, Guid userId);
    }
}
