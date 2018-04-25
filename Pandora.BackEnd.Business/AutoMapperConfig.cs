using AutoMapper;
using Pandora.BackEnd.Business.DTO;
using Pandora.BackEnd.Common.Helpers;
using Pandora.BackEnd.Model.Users;

namespace Pandora.BackEnd.Business
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(map =>
            {
                //Set mapping below

                #region Report

                //Employee
                map.CreateMap<Employee, EmployeeDRO>()
                .ForMember(d => d.EmployeeId, o => o.MapFrom(s => s.EmployeeId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.ToString()))
                .ForMember(d => d.Gender, o => o.MapFrom(s => EnumHelper.GetDescription(s.Gender)));

                #endregion

                #region Business

                //Employee
                map.CreateMap<Employee, EmployeeDTO>()
                .ForMember(d => d.EmployeeId, o => o.MapFrom(s => s.EmployeeId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.ToString()))
                .ForMember(d => d.Gender, o => o.MapFrom(s => EnumHelper.GetDescription(s.Gender)));
                map.CreateMap<EmployeeDTO, Employee>();

                #endregion

            });
        }
    }
}
