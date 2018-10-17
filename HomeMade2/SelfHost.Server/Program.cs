using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace SelfHost.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:5050";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Services started at: {DateTime.UtcNow:D} t Url: {url}");
                Console.ReadLine();
            }
        }
    }
}
