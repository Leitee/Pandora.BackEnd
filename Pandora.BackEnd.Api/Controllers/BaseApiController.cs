using Microsoft.AspNet.Identity.Owin;
using Pandora.BackEnd.Data.AccountManager;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Pandora.BackEnd.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        //private readonly ApplicationUserManager _userManager = null;
        //private readonly ApplicationRoleManager _roleManager = null;

        //protected ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //}

        //protected ApplicationRoleManager RoleManager
        //{
        //    get
        //    {
        //        return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        //    }
        //}
    }
}
