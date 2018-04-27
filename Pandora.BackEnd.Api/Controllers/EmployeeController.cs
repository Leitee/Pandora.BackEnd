using Pandora.BackEnd.Business.Contracts;
using Pandora.BackEnd.Reports.Contracts;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pandora.BackEnd.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/employee")]
    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeSVC _employeeSvc;
        private readonly IEmployeeReportSVC _employeeReportSVC;

        public EmployeeController(IEmployeeSVC pEmployeeSvc, IEmployeeReportSVC pEmployeeReportSVC)
        {
            _employeeSvc = pEmployeeSvc;
            _employeeReportSVC = pEmployeeReportSVC;
        }

        public async Task<IHttpActionResult> Get()
        {
            var response = await _employeeSvc.GetAllAsync();

            if (response.HasErrors)
                throw new Exception(string.Join(" - ", response.Errors.ToArray()));

            return Ok(response.Data);
        }

        [Route("report")]
        public async Task<HttpResponseMessage> GetReport()
        {
            HttpResponseMessage apiResponse;

            var response = await _employeeReportSVC.EmployeeFullListReport();

            if (response.HasErrors)
                throw new Exception(string.Join(" - ", response.Errors.ToArray()));

            apiResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(response.Report)
            };
            apiResponse.Content.Headers.ContentLength = response.Report.Length;
            apiResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            apiResponse.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "employees_full_list"
            };        

            return apiResponse;
        }
    }
}
