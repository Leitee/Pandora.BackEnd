﻿using AutoMapper;
using Pandora.BackEnd.Business.DTO;
using Pandora.BackEnd.Common.Helpers;
using Pandora.BackEnd.Model.Users;

namespace Pandora.BackEnd.Business.Config
{
    public class BusinessMapperProfile : Profile
    {
        public BusinessMapperProfile()
        {
            //Employee
            CreateMap<Employee, EmployeeDTO>()
            .ForMember(d => d.EmployeeId, o => o.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.ToString()))
            .ForMember(d => d.Gender, o => o.MapFrom(s => EnumHelper.GetDescription(s.Gender)));
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
