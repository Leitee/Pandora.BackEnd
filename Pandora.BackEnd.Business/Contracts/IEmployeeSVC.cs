using Pandora.BackEnd.Business.DTOs;
using Pandora.BackEnd.Business.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Business.Contracts
{
    public interface IEmployeeSVC
    {
        Task<BLResponse<List<EmployeeDTO>>> GetAllPagedAsync(EmployeeFilter _empFilter);
        Task<BLResponse<List<EmployeeDTO>>> GetAllAsync();
        Task<BLResponse<EmployeeDTO>> GetByIdAsync(int _id);
        Task<BLResponse<object>> CreateAsync(EmployeeDTO _empDto);
        Task<BLResponse<bool>> UpdateAsync(EmployeeDTO _empDto);
        Task<BLResponse<bool>> DeleteAsync(int _id);
    }
}
