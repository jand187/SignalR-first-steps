using System;
using System.Threading;
using Microsoft.AspNetCore.SignalR;

namespace WebServer.Hubs
{
    public class NotificationHub : Hub
    {
        public void ServerTime()
        {
            do
            {
                Console.WriteLine($"Client Id: {Context.ConnectionId} Time called: {DateTime.UtcNow:G}");
                Clients.All.SendCoreAsync("displayTime", new[] {$"{DateTime.UtcNow:F}"});
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            while (true);
        }

        public string[] Get()
        {
            return new[] {"value1", "value2", "value3"};
        }

        public void Send(string sender, string message)
        {
            Clients.All.SendCoreAsync("displayTime", new[] { $"{DateTime.UtcNow:F}" });

        }
    }
}
