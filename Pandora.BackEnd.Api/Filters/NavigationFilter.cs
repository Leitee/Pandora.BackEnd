using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Pandora.BackEnd.Api.Filters
{
    public class NavigationFilter : ActionFilterAttribute, IActionFilter
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }        

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}

