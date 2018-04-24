using Pandora.BackEnd.Business.DTO;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Model.Users;
using Pandora.BackEnd.Reports.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Reports
{
    public class ReportSVC : IEmployeeReportSVC
    {
        private readonly IApplicationUow _uow;

        public ReportSVC(IApplicationUow pUow)
        {
            _uow = pUow;
        }

        public async Task<RLResponse> EmployeeFullListReport()
        {
            var report = new RLResponse();

            try
            {
                var empsAsync = await _uow.EmployeeRepository.AllAsync(null, null, null);
                var empsRep = AutoMapper.Mapper.Map<List<Employee>, EmployeeDRO>(empsAsync.ToList());

                var reportInstance = new ReportMaker("EmployeesFullList", "EmployeeFullListDS", empsRep, ReportMaker.GetDeviceInfoXML());

                report.Data = reportInstance.Create();
            }
            catch (Exception ex)
            {
                HandleReportSVCException(ref report, ex);
            }

            return report;
        }

        private void HandleReportSVCException(ref RLResponse pResponse, Exception pEx)
        {
            pResponse.HasErrors = true;
            pResponse.Errors.Add("Error at Report Service");
            pResponse.Errors.Add(pEx.Message);
            if (pEx.InnerException != null)
                pResponse.Errors.Add(pEx.InnerException.Message);
        }
    }
}
