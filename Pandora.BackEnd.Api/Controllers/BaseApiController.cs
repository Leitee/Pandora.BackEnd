﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Pandora.BackEnd.Data.AccountManager;
using System.Net.Http;
using System.Web.Http;

namespace Pandora.BackEnd.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        //private ModelFactory _modelFactory;
        private readonly ApplicationUserManager _AppUserManager = null;
        private readonly ApplicationRoleManager _AppRoleManager = null;


        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        //protected ModelFactory TheModelFactory
        //{
        //    get
        //    {
        //        if (_modelFactory == null)
        //        {
        //            _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
        //        }
        //        return _modelFactory;
        //    }
        //}

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
