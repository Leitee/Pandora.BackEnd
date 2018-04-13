using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Pandora.BackEnd.Data.AccountManager;
using Pandora.BackEnd.Model.AppEntity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Bussines.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = context.OwinContext.GetUserManager<ApplicationUserManager>().Users.First(u => u.UserName == context.UserName);
            if (!context.OwinContext.Get<ApplicationUserManager>().CheckPassword(user, context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return Task.FromResult<object>(null);
            }
            var rol = context.OwinContext.Get<ApplicationRoleManager>().FindById(user.Roles.First().RoleId);

            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, user, rol), new AuthenticationProperties { AllowRefresh = true });
            context.Validated(ticket);
            
            return Task.FromResult<object>(null);
        }

        private static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, AppUser user, AppRole rol)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim("userId", user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("rolId", rol.Id));
            identity.AddClaim(new Claim(ClaimTypes.Role, rol.Name));
            return identity;
        }
    }
}