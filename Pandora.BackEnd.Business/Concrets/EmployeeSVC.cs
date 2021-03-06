﻿using AutoMapper;
using Pandora.BackEnd.Business.Contracts;
using Pandora.BackEnd.Business.DTO;
using Pandora.BackEnd.Business.Filters;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Business.Concrets
{
    public class EmployeeSVC : ServicesBase, IEmployeeSVC
    {
        public EmployeeSVC(IApplicationUow pUow)
        {
            Uow = pUow;
        }

        public Task<BLResponse<EmployeeDTO>> CreateAsync(EmployeeDTO pEmpDto)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<bool>> DeleteAsync(EmployeeDTO pDto)
        {
            throw new NotImplementedException();
        }

        public async Task<BLResponse<List<EmployeeDTO>>> GetAllAsync()
        {
            var response = new BLResponse<List<EmployeeDTO>>();

            try
            {
                var empsAsync = await Uow.Employees.AllAsync(e => e.Gender == Model.GenderEnum.MAN, o => o.OrderBy(e => e.BirthDate), e => e.AppUser);

                response.Data = Mapper.Map<List<Employee>, List<EmployeeDTO>>(empsAsync.ToList());
            }
            catch (Exception ex)
            {
                HandleSVCException(ref response, ex);
            }

            return response;
        }

        public Task<BLResponse<List<EmployeeDTO>>> GetAllPagedAsync(EmployeeFilter pEmpFilter)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<EmployeeDTO>> GetByIdAsync(int pId)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<bool>> UpdateAsync(EmployeeDTO pEmpDto)
        {
            throw new NotImplementedException();
        }
    }
}
