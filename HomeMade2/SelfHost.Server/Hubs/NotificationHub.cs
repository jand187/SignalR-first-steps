using System;
using System.Threading;
using Microsoft.AspNet.SignalR;

namespace SelfHost.Server.Hubs
{
    public class NotificationHub : Hub
    {
        public void ServerTime()
        {
            do
            {
                Console.WriteLine($"Client Id: {Context.ConnectionId} Time called: {DateTime.UtcNow:G}");
                Clients.All.displayTime($"{DateTime.UtcNow:F}");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            while (true);
        }

        public string[] Get()
        {
            return new[] {"value1", "value2", "value3"};
        }
    }
}
