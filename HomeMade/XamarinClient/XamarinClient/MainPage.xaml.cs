using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;

namespace XamarinClient
{
    public partial class MainPage : ContentPage
    {
        private const string ServerUri = "https://signalrwebapiserver-jaxx.azurewebsites.net/signalr";

        public MainPage()
        {
            InitializeComponent();
        }


        private void Login_Clicked(object sender, EventArgs e)
        {
             var writer = new Action<string>(s => MessageTextBox.Text = s);

            var client = new ConsoleClient(ServerUri, "android1", writer);
            client.ConnectAsync();
            MessageTextBox.HeightRequest = 200;
            MessageTextBox.Text = "Connected";

            client.SendMessage("test");

            
        }

    }


    internal class ConsoleClient
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
                    var formattedMessage = $"From Server: {message}";
                    this.writer(formattedMessage);
                });

            await this.connection.StartAsync();
        }

        public void SendMessage(string message)
        {
            this.connection.InvokeCoreAsync("Send", new object[] { this.login, message });
        }
    }

}
