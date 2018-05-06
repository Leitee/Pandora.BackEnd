using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pandora.BackEnd.Data.AccountManager;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Model.AppEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Data.Concrets
{
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationRoleManager _roleManager;

        public RolRepository(IApplicationDbContext context)
        {
            _roleManager = new ApplicationRoleManager(new RoleStore<AppRole>(context as ApplicationDbContext));
        }

        public async Task<List<AppRole>> GeAllRolesAsync()
        {
            return await Task.Run(() => {
                return this._roleManager.Roles.ToList();
            });
        }

        public async Task<AppRole> GetRoleByIdAsync(string roleId)
        {
            try
            {
                return await this._roleManager.FindByIdAsync(roleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<IdentityResult> CreateRoleAsync(AppRole role)
        {
            var response = new IdentityResult();

            try
            {
                response = await this._roleManager.CreateAsync(role);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<IdentityResult> UpdateRoleAsync(AppRole role)
        {
            var response = new IdentityResult();

            try
            {
                var rol = await this.GetRoleByIdAsync(role.Id);
                rol.Name = role.Name;
                rol.Description = role.Description;

                response = await this._roleManager.UpdateAsync(rol);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var response = new IdentityResult();

            try
            {
                response = await this._roleManager.DeleteAsync(await this.GetRoleByIdAsync(roleId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<AppRole> GetRoleByNameAsync(string roleName)
        {
            try
            {
                return await this._roleManager.FindByNameAsync(roleName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
