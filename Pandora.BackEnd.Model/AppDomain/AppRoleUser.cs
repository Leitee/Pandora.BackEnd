using Microsoft.AspNet.Identity.EntityFramework;

namespace Pandora.BackEnd.Model.AppDomain
{
    public class AppUserRole : IdentityUserRole
    {
        public AppUserRole() : base()
        {

        }

        public AppRole ApplicationRole { get; set; }
    }
}
