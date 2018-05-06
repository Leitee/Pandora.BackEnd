using Pandora.BackEnd.Business.Contracts;
using System.Web.Http;

namespace Pandora.BackEnd.Api.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected IAccountSVC AccountManager;
    }
}
