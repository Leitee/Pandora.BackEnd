using Pandora.BackEnd.Business.Contracts;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pandora.BackEnd.Api.Controllers
{
    //[Authorize]
    [RoutePrefix("api/employee")]
    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeSVC employeeSvc;        

        public EmployeeController(IEmployeeSVC _employeeSvc)
        {
            employeeSvc = _employeeSvc;            
        }

        [Route("emp")]
        public async Task<IHttpActionResult> Get()
        {
            var response = await employeeSvc.GetAllAsync();

            if (response.HasErrors)
            {
                return BadRequest(string.Join(" - ", response.Errors.ToArray()));
            }

            return Ok(response.Data);
        }        
    }
}
