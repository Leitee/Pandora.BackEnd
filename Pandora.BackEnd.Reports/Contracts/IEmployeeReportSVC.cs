using System.Threading.Tasks;

namespace Pandora.BackEnd.Reports.Contracts
{
    public interface IEmployeeReportSVC
    {
        Task<RLResponse> EmployeeFullListReport();
    }
}
