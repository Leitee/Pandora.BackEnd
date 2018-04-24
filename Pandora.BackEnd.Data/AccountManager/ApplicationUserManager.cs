using Microsoft.AspNet.Identity;
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
        private static IUserStore<AppUser> _store;

        public ApplicationUserManager(IUserStore<AppUser> store) : base(store)
        {
            _store = store;
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appContext = context.Get<ApplicationDbContext>();
            var manager = new ApplicationUserManager(_store);

            // Configure validation logic for usernames
            manager.UserValidator = new CustomUserValidator(Helpers.ContextHelper.GetUserManager(appContext))
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Security Code by Phone", new PhoneNumberTokenProvider<AppUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Security Code by Email", new EmailTokenProvider<AppUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            manager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return manager;
        }
    }
}
