using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Pandora.BackEnd.Business.DTO
{
    public class AppUserDTO : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
