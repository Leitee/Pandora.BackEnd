using Pandora.BackEnd.Model.Users;

namespace Pandora.BackEnd.Data.Contracts
{
    public interface IApplicationUow
    {
        bool Commit();

        //Account Repositories  
        IAuthRepository Users   { get; }
        IRolRepository Roles { get; }

        //Model Repositories
        IRepository<Employee> Employees { get; }
    }
}
