using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Pandora.BackEnd.Common;
using Pandora.BackEnd.Data.Concrets;
using Pandora.BackEnd.Model.AppEntity;
using System;

namespace Pandora.BackEnd.Data.AccountManager
{
    public class ApplicationUserManager : UserManager<AppUser>
    {
        public ApplicationUserManager(IUserStore<AppUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<ApplicationDbContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<AppUser>(appDbContext));

            // Configure validation logic for usernames
            appUserManager.UserValidator = new CustomUserValidator<AppUser>(Helpers.ContextHelper.GetUserManager(appDbContext))
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            appUserManager.PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            appUserManager.UserLockoutEnabledByDefault = true;
            appUserManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            appUserManager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            appUserManager.RegisterTwoFactorProvider("Security Code by Phone", new PhoneNumberTokenProvider<AppUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            appUserManager.RegisterTwoFactorProvider("Security Code by Email", new EmailTokenProvider<AppUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            appUserManager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return appUserManager;
        }
    }
}
