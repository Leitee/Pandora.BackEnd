using Microsoft.AspNet.Identity;
using Pandora.BackEnd.Model.AppEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Data.Contracts
{
    public interface IRolRepository : IRepository<AppRole>
    {
        Task<List<AppRole>> GeAllRolesAsync();

        Task<AppRole> GetRoleById(string roleId);

        Task<IdentityResult> CreateRoleAsync(AppRole role);

        Task<IdentityResult> EditRoleAsync(AppRole role);

        Task<IdentityResult> DeleteRoleAsync(string roleId);

        Task<AppRole> GetRoleByName(string roleName);
    }
}
