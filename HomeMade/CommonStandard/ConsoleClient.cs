using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace CommonStandard
{
    public class ConsoleClient
    {
        private readonly HubConnection connection;
        private readonly string login;
        private readonly Action<string> writer;

        public ConsoleClient(string serverUri, string login, Action<string> writer)
        {
            this.login = login;
            this.writer = writer;

            this.connection = new HubConnectionBuilder().WithUrl(serverUri).Build();
        }


        public async void ConnectAsync()
        {
            this.connection.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await this.connection.StartAsync();
            };

            this.connection.On<string>(
                "displayMessage",
                message =>
                {
                    this.writer($"From Server: {message}");
                });

            await this.connection.StartAsync();
        }

        public void SendMessage(string message)
        {
            this.connection.InvokeCoreAsync("Send", new object[] { this.login, message });
        }
    }
}