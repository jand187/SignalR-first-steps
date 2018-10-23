﻿using System;
using CommonStandard;

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
