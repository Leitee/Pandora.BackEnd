using AutoMapper;
using Pandora.BackEnd.Common.Helpers;
using Pandora.BackEnd.Model.Users;
using Pandora.BackEnd.Reports.DRO;

namespace Pandora.BackEnd.Reports.Config
{
    public class ReportMapperProfile : Profile
    {
        public ReportMapperProfile()
        {
            //Employee
             CreateMap<Employee, EmployeeDRO>()
            .ForMember(d => d.EmployeeId, o => o.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.ToString()))
            .ForMember(d => d.Gender, o => o.MapFrom(s => EnumHelper.GetDescription(s.Gender)));
        }
    }
}
