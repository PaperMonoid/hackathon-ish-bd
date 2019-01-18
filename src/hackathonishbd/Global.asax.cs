using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace hackathonishbd
{
    public class Global : HttpApplication
    {
        public static int _id { get; internal set; }
        public static string _rol { get; internal set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
