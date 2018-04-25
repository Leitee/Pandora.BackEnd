using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Pandora.BackEnd.Data.AccountManager;
using Pandora.BackEnd.Model.AppEntity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Bussines.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        // we should validate clientes app here
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        // validate username and password from the request and validate them against our ASP.NET 2.1 Identity system
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // cors allowed
            var allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            AppUser user = await userManager.FindAsync(context.UserName, context.Password);            

            if (! await userManager.CheckPasswordAsync(user, context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }

            if ( await userManager.IsEmailConfirmedAsync(user.Id))
            {
                context.SetError("invalid_grant", "User did not confirm email.");
                context.Rejected();
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager);
            oAuthIdentity.AddClaims(ExtendedClaimsProvider.GetClaims(user));
            oAuthIdentity.AddClaims(ExtendedClaimsProvider.CreateRolesBasedOnClaims(oAuthIdentity));

            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties
            {
                AllowRefresh = true
            });

            context.Validated(ticket);
        }
    }
}