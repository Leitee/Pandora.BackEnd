using Pandora.BackEnd.Business.Contracts;
using Pandora.BackEnd.Business.DTOs;
using Pandora.BackEnd.Business.Filters;
using Pandora.BackEnd.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Business.Concrets
{
    public class EmployeeSVC : ServicesBase, IEmployeeSVC
    {
        public EmployeeSVC(IApplicationUow _uow)
        {
            Uow = _uow;
        }

        public Task<BLResponse<object>> CreateAsync(EmployeeDTO _empDto)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<bool>> DeleteAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<List<EmployeeDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<List<EmployeeDTO>>> GetAllPagedAsync(EmployeeFilter _empFilter)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<EmployeeDTO>> GetByIdAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<bool>> UpdateAsync(EmployeeDTO _empDto)
        {
            throw new NotImplementedException();
        }
    }
}
