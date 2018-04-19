using Pandora.BackEnd.Business.DTO;
using Pandora.BackEnd.Business.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Business.Contracts
{
    public interface IEmployeeSVC
    {
        Task<BLResponse<List<EmployeeDTO>>> GetAllPagedAsync(EmployeeFilter pEmpFilter);
        Task<BLResponse<List<EmployeeDTO>>> GetAllAsync();
        Task<BLResponse<EmployeeDTO>> GetByIdAsync(int pId);
        Task<BLResponse<object>> CreateAsync(EmployeeDTO pEmpDto);
        Task<BLResponse<bool>> UpdateAsync(EmployeeDTO pEmpDto);
        Task<BLResponse<bool>> DeleteAsync(int pId);
    }
}
