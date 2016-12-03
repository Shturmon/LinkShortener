using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using Microsoft.Practices.Unity;
using System.Web.Http;
using LinkShortener.BLL.Business;
using LinkShortener.BLL.Contracts;
using LinkShortener.BLL.Services;
using LinkShortener.Data.Contracts;
using LinkShortener.DAL;
using LinkShortener.DAL.Contracts;
using LinkShortener.DAL.Repositories;
using LinkShortener.Web.Services;
using LinkShortener.Web.Services.Contracts;
using Unity.WebApi;

namespace LinkShortener.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var migrator = new DbMigrator(new DAL.Migrations.Configuration
            {
                TargetDatabase = new DbConnectionInfo("LinkShortenerDbContext")
            });
            migrator.Update();

            var container = BuildUnityContainer();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<ILinkShortenerDbContext, LinkShortenerDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(),
                    new InjectionProperty("LinkRepository", new ResolvedParameter<ILinkRepository>()));

            container.RegisterType<ILinkRepository, LinkRepository>();

            container.RegisterType<ILinkBusiness, LinkBusiness>();

            container.RegisterType<INumberEncodingService, NumberEncodingService>();

            container.RegisterType<ICookieService, CookieService>();

            return container;
        }
    }
}