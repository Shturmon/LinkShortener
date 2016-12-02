using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LinkShortener.Web.Startup))]

namespace LinkShortener.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            app.UseWebApi(config);

            UnityConfig.RegisterComponents(config);
            WebApiConfig.Register(config);
        }
    }
}
