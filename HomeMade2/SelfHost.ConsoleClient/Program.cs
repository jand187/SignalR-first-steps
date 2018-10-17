using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SelfHost.ConsoleClient
{
    class Program
    {
        static string[] GetFromServer()
        {
            var client = new HttpClient();
            var response = client.GetAsync("http://localhost:5050/api/product").Result;
            var result = response.Content.ReadAsAsync<string[]>().Result;
            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            return null;
        }

        static string[] RunSignalR()
        {
            var url = "http://localhost:5050";
            var connection = new HubConnection(url);
            var proxy = connection.CreateHubProxy("notificationHub");
            try
            {
                connection.Start().Wait();

                var text = proxy.Invoke<string[]>("get").Result;

                Console.WriteLine(text.First());
                /*
            proxy.On<string>(
                "displayTime",
                time =>
                {
                    Console.Clear();
                    Console.WriteLine($"From Server: {time}");
                });
            proxy.Invoke("ServerTime");
            */
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return null;

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to start.");
            Console.ReadLine();
            GetFromServer()
                .ToList()
                .ForEach(
                    p =>
                    {
                        Console.WriteLine($"Item: {p}");
                    });

            Console.WriteLine("Press enter for signalr services.");
            Console.ReadLine();

            RunSignalR();
            Console.ReadLine();

        }
    }
}
