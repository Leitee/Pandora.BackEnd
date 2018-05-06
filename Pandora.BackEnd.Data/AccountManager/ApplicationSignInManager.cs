using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Pandora.BackEnd.Model.AppEntity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Data.AccountManager
{
    public class ApplicationSignInManager : SignInManager<AppUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public async override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
        {
             string authenticationType = "JWT";
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await UserManager.CreateIdentityAsync(user, authenticationType);

            // Add custom user claims here

            return userIdentity;
        }

        public async Task<SignInStatus> TrySignIn(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var signResul = await PasswordSignInAsync(context.UserName, context.Password, false, false);

            if (signResul == SignInStatus.Failure)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }

            if (signResul == SignInStatus.RequiresVerification)
            {
                context.SetError("invalid_grant", "User did not confirm email.");
            }

            if (signResul == SignInStatus.LockedOut)
            {
                context.SetError("invalid_grant", "This User is currently locked out.");
            }

            return signResul;
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

    }
}
