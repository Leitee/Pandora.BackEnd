using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pandora.BackEnd.Data.Concrets;
using Pandora.BackEnd.Model.AppEntity;

namespace Pandora.BackEnd.Data.Helpers
{
    public static class ContextHelper
    {
        public static UserManager<AppUser> GetUserManager(ApplicationDbContext _context)
        {
            var userStored = new UserStore<AppUser>(_context);

            return new UserManager<AppUser>(userStored);
        }
    }
}
