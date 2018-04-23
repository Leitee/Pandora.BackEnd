using ATPSistema.Api.App_Start;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Pandora.BackEnd.Business.DTO;
using SimpleInjector;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Pandora.BackEnd.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Set Serialization format
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Set webapi dependency resolver
            config.DependencyResolver = SimpleInjectorConfig.Register(new Container());

            // Set AutoMapper Mapping
            AutoMapperConfig.Execute();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
