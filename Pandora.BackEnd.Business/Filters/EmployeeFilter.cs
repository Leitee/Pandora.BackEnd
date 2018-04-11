namespace Pandora.BackEnd.Business.Filters
{
    public class EmployeeFilter : PagedFilter
    {
        public int? EmployeeId { get; set; }
        public string Name { get; set; }
        public int? Gender { get; set; }
    }
}
