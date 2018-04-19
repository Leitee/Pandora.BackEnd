using AutoMapper;
using Pandora.BackEnd.Common.Helpers;
using Pandora.BackEnd.Model.Users;

namespace Pandora.BackEnd.Business.DRO
{
    public class DROConfig
    {
        public static void Execute()
        {
            Mapper.Initialize(map =>
            {
                //Set mapping below

                //Employee
                map.CreateMap<Employee, EmployeeDRO>()
                .ForMember(d => d.EmployeeId, o => o.MapFrom(s => s.EmployeeId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.ToString()))
                .ForMember(d => d.Gender, o => o.MapFrom(s => EnumHelper.GetDescription(s.Gender)));
            });
        }
    }
}
