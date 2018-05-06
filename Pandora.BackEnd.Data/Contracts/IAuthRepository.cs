using Microsoft.AspNet.Identity;
using Pandora.BackEnd.Model.AppEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Data.Contracts
{
    public interface IAuthRepository
    {
        Task<List<AppUser>> GeAllUsersAsync();

        Task<AppUser> FindUserBydIdAsync(string userId);

        Task<AppUser> FindUserAsync(string userName);

        Task<AppUser> FindUserAsync(string userName, string password);

        Task<AppUser> FindUserAsync(UserLoginInfo userLoginInfo);

        Task<IdentityResult> CreateUserAsync(AppUser user);

        Task<IdentityResult> CreateUserAsync(AppUser user, string password);

        Task<IdentityResult> EditUserAsync(AppUser user);

        Task<IdentityResult> DeleteUserAsync(string userId);

        Task<IdentityResult> RegisterUserAsync(string userName, string email, string password);

        Task<IdentityResult> AddToRolesAsync(string userId, params string[] AppRolees);

        Task<IdentityResult> RemoveRolesAsync(string userId, params string[] AppRolees);

        Task<IList<string>> GetRolesByUserIdAsync(string userId);

        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);

        Task SendEmailAsync(string userId, string subject, string body);

        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);

        Task<bool> IsEmailConfirmedAsync(string userId);

        Task<bool> ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
