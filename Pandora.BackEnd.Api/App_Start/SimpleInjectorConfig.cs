using Pandora.BackEnd.Business.Config;
using Pandora.BackEnd.Reports.Config;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;

namespace Pandora.BackEnd.Api
{
    public static class SimpleInjectorConfig
    {
        public static SimpleInjectorWebApiDependencyResolver Register(Container container)
        {
            // Create the container as usual.
            if (container == null)
                container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();// new WebApiRequestLifestyle();

            //Bussines Services
            SimpleInjectorBusinessConfig.Register(ref container);

            //Report Services
            SimpleInjectorReportConfig.Register(ref container);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            return new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}