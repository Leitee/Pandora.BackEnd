using Owin;
using Pandora.BackEnd.Data.AccountManager;
using Pandora.BackEnd.Data.Concrets;

namespace Pandora.BackEnd.Business.Config
{
    public class AccountManagerConfig
    {
        public static void GetManagers(ref IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
        }
    }
}
