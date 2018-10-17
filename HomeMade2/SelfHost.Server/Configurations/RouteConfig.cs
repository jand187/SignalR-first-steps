using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace SelfHost.Server.Configurations
{
    public static class RouteConfig
    {
        public static void RegisterRoute(HttpConfiguration route)
        {
            route.Routes.MapHttpRoute(
                name: "DefaultApi", 
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id=RouteParameter.Optional});
        }
    }
}
