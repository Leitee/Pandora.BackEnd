﻿using Pandora.BackEnd.Business.Concrets;
using Pandora.BackEnd.Business.Contracts;
using Pandora.BackEnd.Data.Concrets;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Data.Helpers;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;

namespace ATPSistema.Api.App_Start
{
    public static class SimpleInjectorConfig
    {
        public static SimpleInjectorWebApiDependencyResolver Register(Container container)

        {
            // Create the container as usual.
            if (container == null)
                container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();// new WebApiRequestLifestyle();

            container.Register<RepositoryFactories, RepositoryFactories>(Lifestyle.Singleton);

            container.Register<IRepositoryProvider,RepositoryProvider>(Lifestyle.Scoped);

            //container.Register<IAuthRepository, AuthRepository>();
            //container.Register<IRolRepository, RolRepository>();


            // Register your types, for instance using the scoped lifestyle:
            //Unit of Work
            container.Register<IApplicationUow, ApplicationUow>(Lifestyle.Scoped);
            container.Register<IApplicationDbContext, ApplicationDbContext>(Lifestyle.Scoped);

            //Repository
            //container.Register<IRepository, EFRepository>(Lifestyle.Scoped);

            //Bussines Services
            container.Register<IEmployeeSVC, EmployeeSVC>();
            

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            return new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}