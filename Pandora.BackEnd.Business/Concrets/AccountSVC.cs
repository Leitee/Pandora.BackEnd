using AutoMapper;
using Pandora.BackEnd.Business.Contracts;
using Pandora.BackEnd.Business.DTO;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Model.AppEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Business.Concrets
{
    public class AccountSVC : ServicesBase, IAccountSVC
    {
        public AccountSVC(IApplicationUow uow)
        {
            Uow = uow;
        }

        public async Task<BLResponse<List<AppRoleDTO>>> GetAllRolesAsync()
        {
            var response = new BLResponse<List<AppRoleDTO>>();

            try
            {
                var resulAsync = await Uow.Roles.GeAllRolesAsync();

                response.Data = Mapper.Map<List<AppRole>, List<AppRoleDTO>>(resulAsync.ToList());
            }
            catch (Exception ex)
            {
                HandleSVCException(ref response, ex);
            }

            return response;
        }

        public async Task<BLResponse<AppRoleDTO>> GetRoleByIdAsync(string pId)
        {
            var response = new BLResponse<AppRoleDTO>();

            try
            {
                var resulAsync = await Uow.Roles.GetRoleByIdAsync(pId);

                response.Data = Mapper.Map<AppRole, AppRoleDTO>(resulAsync);
            }
            catch (Exception ex)
            {
                HandleSVCException(ref response, ex);
            }

            return response;
        }

        public async Task<BLResponse<AppRoleDTO>> GetRoleByNameAsync(string pRolName)
        {
            var response = new BLResponse<AppRoleDTO>();

            try
            {
                var resulAsync = await Uow.Roles.GetRoleByNameAsync(pRolName);

                response.Data = Mapper.Map<AppRole, AppRoleDTO>(resulAsync);
            }
            catch (Exception ex)
            {
                HandleSVCException(ref response, ex);
            }

            return response;
        }

        public async Task<BLResponse<AppRoleDTO>> CreateRoleAsync(AppRoleDTO pDto)
        {
            var response = new BLResponse<AppRoleDTO>();

            try
            {
                var rolModel = Mapper.Map<AppRoleDTO, AppRole>(pDto);
                var resulAsync = await Uow.Roles.CreateRoleAsync(rolModel);
                if (resulAsync.Succeeded)
                {
                    response.Data = (await GetRoleByNameAsync(pDto.Name)).Data;
                }
                else
                {
                    HandleSVCException(ref response, resulAsync.Errors.ToArray());
                }
            }
            catch (Exception ex)
            {
                HandleSVCException(ref response, ex);
            }

            return response;
        }

        public async Task<BLResponse<bool>> UpdateRoleAsync(AppRoleDTO pDto)
        {
            var response = new BLResponse<bool>();

            try
            {
                var rolModel = Mapper.Map<AppRoleDTO, AppRole>(pDto);
                var resulAsync = await Uow.Roles.UpdateRoleAsync(rolModel);
                if (resulAsync.Succeeded)
                {
                    response.Data = resulAsync.Succeeded;
                }
                else
                {
                    HandleSVCException(ref response, resulAsync.Errors.ToArray());
                }
            }
            catch (Exception ex)
            {
                HandleSVCException(ref response, ex);
            }

            return response;
        }

        public async Task<BLResponse<bool>> DeleteRoleAsync(AppRoleDTO pDto)
        {
            var response = new BLResponse<bool>();

            try
            {
                var resulAsync = await Uow.Roles.DeleteRoleAsync(pDto.Id);
                if (resulAsync.Succeeded)
                {
                    response.Data = resulAsync.Succeeded;
                }
                else
                {
                    HandleSVCException(ref response, resulAsync.Errors.ToArray());
                }
            }
            catch (Exception ex)
            {
                HandleSVCException(ref response, ex);
            }

            return response;
        }


        public Task<BLResponse<List<AppUserDTO>>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<AppUserDTO>> GetUserByIdAsync(string pId)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<AppUserDTO>> GetUserByNameAsync(string pUserName)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<AppUserDTO>> CreateUserAsync(AppUserDTO pDto)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<bool>> UpdateUserAsync(AppUserDTO pDto)
        {
            throw new NotImplementedException();
        }

        public Task<BLResponse<bool>> DeleteUserAsync(AppUserDTO pDto)
        {
            throw new NotImplementedException();
        }
    }
}
