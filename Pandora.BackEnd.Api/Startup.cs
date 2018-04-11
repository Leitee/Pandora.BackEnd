using Microsoft.Owin;
using Owin;
using Pandora.BackEnd.Api;

[assembly: OwinStartup(typeof(Startup))]

namespace Pandora.BackEnd.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
