using Pandora.BackEnd.Model.AppEntity;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Pandora.BackEnd.Data.AccountManager
{
    public static class ExtendedClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(AppUser user)
        {

            List<Claim> claims = new List<Claim>();

            var daysInWork = (DateTime.Now.Date - user.JoinDate).TotalDays;

            if (daysInWork > 90)
            {
                claims.Add(CreateClaim("FTE", "1"));

            }
            else
            {
                claims.Add(CreateClaim("FTE", "0"));
            }

            return claims;
        }

        public static IEnumerable<Claim> CreateRolesBasedOnClaims(ClaimsIdentity identity)
        {
            List<Claim> claims = new List<Claim>();

            if (identity.HasClaim(c => c.Type == "FTE" && c.Value == "1") &&
                identity.HasClaim(ClaimTypes.Role, "Admin"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "IncidentResolvers"));
            }

            return claims;
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

    }
}
