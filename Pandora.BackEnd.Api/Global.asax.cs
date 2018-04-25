using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace Pandora.BackEnd.Api
{
    public class WebApiApplication : HttpApplication
    {
#if DEBUG
        string connString = ConfigurationManager.ConnectionStrings["DebugConnection"].ConnectionString;
#else
        string connString = ConfigurationManager.ConnectionStrings["ReleaseConnection"].ConnectionString;
#endif

        protected void Application_Start()
        {
            //Start SQL Dependency
            SqlDependency.Start(connString);
        }

        protected void Application_Stop()
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }
    }
}
