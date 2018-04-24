using Microsoft.AspNet.Identity;
using Pandora.BackEnd.Model.AppEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Common
{
    public class CustomUserValidator : UserValidator<AppUser>
    {

        List<string> _allowedEmailDomains = new List<string> { "outlook.com", "hotmail.com", "gmail.com", "yahoo.com", "pandorasistemas.com" };

        public CustomUserValidator(UserManager<AppUser> appUserManager) : base(appUserManager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);
            
            var emailDomain = user.Email.Split('@')[1];

            if (!_allowedEmailDomains.Contains(emailDomain.ToLower()))
            {
                var errors = result.Errors.ToList();

                errors.Add(String.Format("Email domain '{0}' is not allowed", emailDomain));

                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}