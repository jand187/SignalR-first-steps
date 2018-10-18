using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication.CoreServer.Hubs
{
    public class NotificationHub: Hub
    {
        public void ServerTime()
        {
            do
            {
                Console.WriteLine($"Client Id: {Context.ConnectionId} Time called: {DateTime.UtcNow:G}");
                Clients.All.SendCoreAsync("displayTime", new object[] {$"{DateTime.UtcNow:F}"});
                //Clients.All.displayTime($"{DateTime.UtcNow:F}");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            while (true);
        }

        [HttpPost]
        public string[] Get()
        {
            return new[] { "value1", "value2", "value3" };
        }
    }
}
