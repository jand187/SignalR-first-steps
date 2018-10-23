using System;
using CommonStandard;

namespace Client1
{
    internal class Program
    {
        private const string ServerUri = "https://signalrwebapiserver-jaxx.azurewebsites.net/signalr";
        //private const string ServerUri = "http://localhost:63840/signalr";

        private static void Main(string[] args)
        {
            Console.WriteLine("Client 1 Logining in");
            var login = "win1";

            var client = new ConsoleClient(ServerUri, login, s =>
            {
                Console.Clear();
                Console.WriteLine(s);
            });
            client.ConnectAsync();

            Console.WriteLine("Type message: ");

            while (true)
            {
                var message = Console.ReadLine();
                if (message == "q")
                {
                    break;
                }

                client.SendMessage(message);
            }
        }
    }
}
