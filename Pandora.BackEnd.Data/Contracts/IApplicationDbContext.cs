using Pandora.BackEnd.Model.Users;
using System;
using System.Data.Entity;

namespace Pandora.BackEnd.Data.Contracts
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<Employee> Employees { get; set; }
    }
}
