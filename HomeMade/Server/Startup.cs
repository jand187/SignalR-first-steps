using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddCors(options => options.AddPolicy("test", builder => builder.AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(
                async context =>
                {
//                    await context.Response.WriteAsync("Hello World!");
                });

            app.UseSignalR(builder => builder.MapHub<MyHub>("/signalr"));
        }
    }

    public class MyHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.SendCoreAsync("addMessage", new object[] {name, message});
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Client connected: " + Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Client disconnected: " + Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
