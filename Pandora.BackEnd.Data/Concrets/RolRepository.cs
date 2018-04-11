﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Model.AppDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Data.Concrets
{
    public class RolRepository : EFRepository<Role>, IRolRepository
    {
        private readonly IApplicationDbContext _context;

        private readonly RoleStore<Role> _roleStore;
        private readonly ApplicationRoleManager _roleManager;

        public RolRepository(IApplicationDbContext context)
            : base(context)
        {
            _context = context;
            this._roleStore = new RoleStore<Role>(_context as ApplicationDbContext);
            this._roleManager = new ApplicationRoleManager(this._roleStore);
        }

        public async Task<List<Role>> GeAllRolesAsync()
        {
            return await Task.Run(() => {
                return this._roleManager.Roles.ToList();
            });
        }

        public async Task<Role> GetRoleById(string roleId)
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

        public async Task<IdentityResult> CreateRoleAsync(Role role)
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

        public async Task<IdentityResult> EditRoleAsync(Role role)
        {
            var response = new IdentityResult();

            try
            {
                var rol = await this.GetRoleById(role.Id);
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
                response = await this._roleManager.DeleteAsync(await this.GetRoleById(roleId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<Role> GetRoleByName(string roleName)
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