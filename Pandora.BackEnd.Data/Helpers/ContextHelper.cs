using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pandora.BackEnd.Data.Concrets;
using Pandora.BackEnd.Model.AppEntity;

namespace Pandora.BackEnd.Data.Helpers
{
    public static class ContextHelper
    {
        public static UserManager<AppUser> GetUserManager(ApplicationDbContext pContext)
        {
            var userStored = new UserStore<AppUser>(pContext);

            return new UserManager<AppUser>(userStored);
        }
    }
}
