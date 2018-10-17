using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SelfHost.Server;
using SelfHost.Server.Configurations;

[assembly: OwinStartup(typeof(Startup))]

namespace SelfHost.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            InitiateConfiguration(app);
        }

        private void InitiateConfiguration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            RouteConfig.RegisterRoute(config);
            app.UseCors(CorsOptions.AllowAll);
            app.Map("/signalr",
                map =>
                {
                    var hubConfig = new HubConfiguration();
                    map.RunSignalR();
                });
            app.UseWebApi(config);
        }
    }
}
