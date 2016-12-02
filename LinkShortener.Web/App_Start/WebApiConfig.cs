using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LinkShortener.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            RegisterRoutes(config);
            RegisterFormatters(config);
        }

        private static void RegisterFormatters(HttpConfiguration config)
        {
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            var settings = formatters.JsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "RedirectApiLink",
                routeTemplate: "a/{token}",
                defaults: new {controller = "Link", action = "RedirectLink"}
            );
        }
    }
}
