using Pandora.BackEnd.Business.DTO;
using Pandora.BackEnd.Business.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Business.Contracts
{
    public interface IEmployeeSVC : ICrudOperations<EmployeeDTO>
    {
        Task<BLResponse<List<EmployeeDTO>>> GetAllPagedAsync(EmployeeFilter pEmpFilter);
    }
}
