using AutoMapper;
using Microsoft.Owin;
using Owin;
using Pandora.BackEnd.Api;
using Pandora.BackEnd.Business.Config;
using Pandora.BackEnd.Reports.Config;

[assembly: OwinStartup(typeof(Startup))]

namespace Pandora.BackEnd.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Set AutoMapper Mapping
            Mapper.Initialize(map =>
            {
                map.AddProfile<BusinessMapperProfile>();
                map.AddProfile<ReportMapperProfile>();
            });

            // Configure the db context and user manager to use a single instance per request
            AccountManagerConfig.GetManagers(ref app);

            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}
