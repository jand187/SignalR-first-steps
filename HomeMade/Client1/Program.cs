using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Client1
{
    class Program
    {
        //private const string ServerUri = "https://localhost:44375/signalr";
        private const string ServerUri = "http://localhost:63840/signalr";

        static void Main(string[] args)
        {
            Console.WriteLine("Login: ");
            var login = Console.ReadLine();

            var client = new ConsoleClient(Console.Out, ServerUri);
            client.ConnectAsync();

            Console.WriteLine("Type message: ");
            var message = Console.ReadLine();

            client.SendMessage(message);


            Console.ReadLine();
        }



    }

    internal class ConsoleClient
    {
        private readonly TextWriter textWriter;

        public ConsoleClient(TextWriter textWriter, string serverUri)
        {
            this.textWriter = textWriter;

            Connection = new HubConnectionBuilder().WithUrl(serverUri).Build();

        }

        private HubConnection Connection { get; set; }

        public async void ConnectAsync()
        {
            Connection.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await Connection.StartAsync();
            };

            Connection.On<string>(
                "displayTime",
                time =>
                {
                    Console.Clear();
                    Console.WriteLine($"From Server: {time}");
                });

            await Connection.StartAsync();
        }

        public void SendMessage(string message)
        {
            Connection.InvokeCoreAsync("Send", new object[] {"Hansemann", "Ja dav!"});
        }
    }
}
