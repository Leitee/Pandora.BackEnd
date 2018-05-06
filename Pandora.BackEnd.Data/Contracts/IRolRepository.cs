using Microsoft.AspNet.Identity;
using Pandora.BackEnd.Model.AppEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Data.Contracts
{
    public interface IRolRepository
    {
        Task<List<AppRole>> GeAllRolesAsync();

        Task<AppRole> GetRoleByIdAsync(string roleId);

        Task<IdentityResult> CreateRoleAsync(AppRole role);

        Task<IdentityResult> UpdateRoleAsync(AppRole role);

        Task<IdentityResult> DeleteRoleAsync(string roleId);

        Task<AppRole> GetRoleByNameAsync(string roleName);
    }
}
