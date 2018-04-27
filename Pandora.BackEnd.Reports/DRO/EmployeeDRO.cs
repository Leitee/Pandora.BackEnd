using System.Collections.Generic;

namespace Pandora.BackEnd.Reports.DRO
{
    public class EmployeeDRO
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }

    public class EmployeesFullListDRO : List<EmployeeDRO> { }
}
