using Pandora.BackEnd.Business.Concrets;
using Pandora.BackEnd.Business.Contracts;
using Pandora.BackEnd.Data.Concrets;
using Pandora.BackEnd.Data.Contracts;
using Pandora.BackEnd.Data.Helpers;
using SimpleInjector;

namespace Pandora.BackEnd.Business.Config
{
    public class SimpleInjectorBusinessConfig
    {
        public static void Register(ref Container container)
        {
            //Repository
            container.Register<IRepositoryProvider, RepositoryProvider>(Lifestyle.Scoped);
            container.Register<RepositoryFactories, RepositoryFactories>(Lifestyle.Singleton);

            //Unit of Work
            container.Register<IApplicationUow, ApplicationUow>(Lifestyle.Scoped);
            container.Register<IApplicationDbContext, ApplicationDbContext>(Lifestyle.Scoped);

            // Business
            container.Register<IAccountSVC, AccountSVC>(Lifestyle.Scoped);
            container.Register<IEmployeeSVC, EmployeeSVC>(Lifestyle.Scoped);



            //var types = container.GetCurrentRegistrations()
            //        .Where(r => r.ServiceType.IsInterface && r.ServiceType != typeof(Func<>))
            //        .Select(r => r.ServiceType);
            //var typesList = types as IList<Type> ?? types.ToList();
            //foreach (var t in typesList)
            //{
            //    var typeToRegister = typeof(Func<>).MakeGenericType(t);
            //    //This needs to be replaced:
            //    container.Register(typeToRegister, () => container.GetInstance(t));
            //}
        }
    }
}
