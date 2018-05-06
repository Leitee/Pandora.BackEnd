using Pandora.BackEnd.Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Business.Contracts
{
    public interface IAccountSVC
    {
        Task<BLResponse<List<AppRoleDTO>>> GetAllRolesAsync();
        Task<BLResponse<AppRoleDTO>> GetRoleByIdAsync(string pId);
        Task<BLResponse<AppRoleDTO>> GetRoleByNameAsync(string pRolName);
        Task<BLResponse<AppRoleDTO>> CreateRoleAsync(AppRoleDTO pDto);
        Task<BLResponse<bool>> UpdateRoleAsync(AppRoleDTO pDto);
        Task<BLResponse<bool>> DeleteRoleAsync(AppRoleDTO pDto);

        Task<BLResponse<List<AppUserDTO>>> GetAllUsersAsync();
        Task<BLResponse<AppUserDTO>> GetUserByIdAsync(string pId);
        Task<BLResponse<AppUserDTO>> GetUserByNameAsync(string pUserName);
        Task<BLResponse<AppUserDTO>> CreateUserAsync(AppUserDTO pDto);
        Task<BLResponse<bool>> UpdateUserAsync(AppUserDTO pDto);
        Task<BLResponse<bool>> DeleteUserAsync(AppUserDTO pDto);
    }
}
