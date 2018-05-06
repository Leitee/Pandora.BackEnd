using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Pandora.BackEnd.Data.AccountManager;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Model.AppEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Data.Concrets
{
    public class AuthRepository : EFRepository<AppUser>, IAuthRepository, IDisposable
    {
        private readonly IApplicationDbContext _currentContext;

        private readonly ApplicationUserManager _userManager;

        public AuthRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
            _currentContext = dbContext;
            _userManager = new ApplicationUserManager(new UserStore<AppUser>(dbContext as ApplicationDbContext));
        }

        public async Task<List<AppUser>> GeAllUsersAsync()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var users = this._userManager.Users.ToList();
                    return users;
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<AppUser> FindUserBydIdAsync(string userId)
        {
            try
            {
                return await this._userManager.FindByIdAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<AppUser> FindUserAsync(UserLoginInfo userLoginInfo)
        {
            AppUser user = await _userManager.FindAsync(userLoginInfo);

            return user;
        }

        public async Task<AppUser> FindUserAsync(string userName, string password)
        {
            AppUser user;

            try
            {
                user = await this._userManager.FindAsync(userName, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return user;
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            var response = new IdentityResult();

            try
            {
                response = await this._userManager.CreateAsync(user, password);

                if (!response.Succeeded)
                {
                    throw new Exception(string.Join(",", response.Errors.ToArray()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<IdentityResult> RegisterUserAsync(string userName, string email, string password)
        {
            var response = new IdentityResult();

            AppUser user = new AppUser
            {
                UserName = userName,
                Email = email
            };

            response = await this._userManager.CreateAsync(user, password);

            ApplicationRoleManager.AddUserToRole(this._userManager, user.Id, "User");

            return response;
        }

        public async Task<IdentityResult> EditUserAsync(AppUser user)
        {
            var response = new IdentityResult();

            try
            {
                var userEdit = this._userManager.FindById(user.Id);

                response = await this._userManager.UpdateAsync(userEdit);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var response = new IdentityResult();

            try
            {
                response = await this._userManager.DeleteAsync(await this.FindUserBydIdAsync(userId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<IdentityResult> AddToRolesAsync(string userId, params string[] roles)
        {
            var response = new IdentityResult();

            try
            {
                var rolRepository = new RolRepository(this._currentContext);

                var currentRoles = await this._userManager.GetRolesAsync(userId);

                var allRoles = await rolRepository.GeAllRolesAsync();

                var rolesNotExists = roles.Except(allRoles.Select(x => x.Name)).ToArray();

                if (rolesNotExists.Count() > 0)
                {
                    throw new Exception(string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                }

                IdentityResult removeResult = await this._userManager.RemoveFromRolesAsync(userId, currentRoles.ToArray());

                if (!removeResult.Succeeded)
                {
                    throw new Exception("Failed to remove user roles");
                }

                IdentityResult addResult = await this._userManager.AddToRolesAsync(userId, roles);

                if (!addResult.Succeeded)
                {
                    throw new Exception("Failed to add user roles");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<IList<string>> GetRolesByUserIdAsync(string userId)
        {
            try
            {
                return await this._userManager.GetRolesAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        //public async Task<bool> AddRefreshToken(RefreshToken token)
        //{

        //   var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

        //   if (existingToken != null)
        //   {
        //     var result = await RemoveRefreshToken(existingToken);
        //   }

        //    _ctx.RefreshTokens.Add(token);

        //    return await _ctx.SaveChangesAsync() > 0;
        //}

        //public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        //{
        //   var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

        //   if (refreshToken != null) {
        //       _ctx.RefreshTokens.Remove(refreshToken);
        //       return await _ctx.SaveChangesAsync() > 0;
        //   }

        //   return false;
        //}

        //public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        //{
        //    _ctx.RefreshTokens.Remove(refreshToken);
        //     return await _ctx.SaveChangesAsync() > 0;
        //}

        //public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        //{
        //    var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

        //    return refreshToken;
        //}

        //public List<RefreshToken> GetAllRefreshTokens()
        //{
        //     return  _ctx.RefreshTokens.ToList();
        //}

        //public async Task SendEmailAsync(string userId, string subject, string body)
        //{
        //    var emailSerivce = new EmailService();
        //    this._userManager.EmailService = emailSerivce;

        //    await this._userManager.SendEmailAsync(userId, subject, body);
        //}

        public async Task<bool> IsEmailConfirmedAsync(string userId)
        {
            try
            {
                var result = await this._userManager.IsEmailConfirmedAsync(userId);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("AuthRepository.IsEmailConfirmedAsync. " + ex.Message);
            }
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userid, string code)
        {
            IdentityResult response;

            try
            {
                var provider = new DpapiDataProtectionProvider("Sample");
                this._userManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(provider.Create("EmailConfirmation"));

                response = await this._userManager.ConfirmEmailAsync(userid, code);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            try
            {
                var provider = new DpapiDataProtectionProvider("Sample");
                this._userManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(provider.Create("EmailConfirmation"));

                var result = await this._userManager.GenerateEmailConfirmationTokenAsync(userId);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        async Task<IdentityResult> IAuthRepository.CreateUserAsync(AppUser user)
        {
            var response = new IdentityResult();

            try
            {
                response = await _userManager.CreateAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        async Task<IdentityResult> IAuthRepository.RemoveRolesAsync(string userId, params string[] roles)
        {
            var response = new IdentityResult();

            try
            {
                response = await this._userManager.RemoveFromRolesAsync(userId, roles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task SendEmailAsync(string userId, string subject, string body)
        {
            try
            {
                var msg = new IdentityMessage
                {
                    Body = body,
                    Subject = subject
                };
                await _userManager.EmailService.SendAsync(msg);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<AppUser> FindUserAsync(string userName)
        {
            AppUser user;

            try
            {
                user = await this._userManager.FindByNameAsync(userName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return user;
        }

        public async Task<bool> ChangePassword(string userName, string oldPassword, string newPassword)
        {
            try
            {
                var user = await this._userManager.FindAsync(userName, oldPassword);
                if (user == null) return false;

                user.PasswordHash = new PasswordHasher().HashPassword(newPassword);
                await this._userManager.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void Dispose()
        {
            _currentContext.Dispose();
            _userManager.Dispose();
        }
    }
}
