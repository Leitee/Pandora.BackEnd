using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Pandora.BackEnd.Data.Concrets;
using Pandora.BackEnd.Model.AppEntity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Pandora.BackEnd.Data.Infraestructure
{
    public class ApplicationRoleManager : RoleManager<AppRole>
    {
        public ApplicationRoleManager(IRoleStore<AppRole, string> roleStore) : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<AppRole>(context.Get<ApplicationDbContext>()));
        }

        public static bool RoleExists(ApplicationRoleManager roleManager, string name)
        {
            return roleManager.RoleExists(name);
        }

        public static bool CreateRole(ApplicationRoleManager _roleManager, string name, string description = "")
        {
            var idResult = _roleManager.Create<AppRole, string>(new AppRole(name, description));
            return idResult.Succeeded;
        }


        public static bool AddUserToRole(ApplicationUserManager _userManager, string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        public static void RemoveFromRole(ApplicationUserManager userManager, string userId, string roleName)
        {
            userManager.RemoveFromRole(userId, roleName);
        }

        public static void DeleteRole(ApplicationDbContext context, ApplicationUserManager userManager, string roleId)
        {
            var roleUsers = context.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId));
            var role = context.Roles.Find(roleId);

            foreach (var user in roleUsers)
            {
                RemoveFromRole(userManager, user.Id, role.Name);
            }
            context.Roles.Remove(role);
            context.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public static List<string> GetRolesForUser(ClaimsIdentity identity)
        {
            return identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).ToList();
        }
    }
}
