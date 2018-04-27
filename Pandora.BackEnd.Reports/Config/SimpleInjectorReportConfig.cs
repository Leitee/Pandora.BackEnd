using Pandora.BackEnd.Reports.Contracts;
using SimpleInjector;

namespace Pandora.BackEnd.Reports.Config
{
    public class SimpleInjectorReportConfig
    {
        public static void Register(ref Container container)
        {
            container.Register<IEmployeeReportSVC, ReportSVC>(Lifestyle.Scoped);            
        }
    }
}
