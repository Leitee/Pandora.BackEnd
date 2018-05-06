using Microsoft.AspNet.Identity.EntityFramework;

namespace Pandora.BackEnd.Business.DTO
{
    public class AppRoleDTO : IdentityRole
    {
        public string Description { get; set ; }
    }
}
