using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client2
{
    internal class Program
    {
        private const string ServerUri = "https://signalrwebapiserver-jaxx.azurewebsites.net/signalr";
        //private const string ServerUri = "http://localhost:63840/signalr";

        private static void Main(string[] args)
        {
            Console.WriteLine("Client 2 Logining in");
            var login = "win2";
            
            var client = new ConsoleClient(Console.Out, ServerUri, login);
            client.ConnectAsync();

            Console.WriteLine("Type message: ");

            while (true)
            {
                var message = Console.ReadLine();
                if (message == "q")
                    break;
                client.SendMessage(message);
            }

            //Console.ReadLine();
        }
    }

    internal class ConsoleClient
    {
        private readonly HubConnection connection;
        private readonly string login;
        private readonly TextWriter textWriter;

        public ConsoleClient(TextWriter textWriter, string serverUri, string login)
        {
            this.textWriter = textWriter;
            this.login = login;

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
                    Console.Clear();
                    Console.WriteLine($"From Server: {message}");
                });

            await this.connection.StartAsync();
        }

        public void SendMessage(string message)
        {
            this.connection.InvokeCoreAsync("Send", new object[] {this.login, message});
        }
    }
}
