using Pandora.BackEnd.Model.Users;

namespace Pandora.BackEnd.Data.Contracts
{
    public interface IApplicationUow
    {
        bool Commit();

        //Repositorios
        IRepository<Employee> EmployeeRepository { get; }
    }
}
