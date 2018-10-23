using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommonStandard;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;
using XamarinClient.Annotations;

namespace XamarinClient
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel viewModel;
        private const string ServerUri = "https://signalrwebapiserver-jaxx.azurewebsites.net/signalr";

        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            BindingContext = this.viewModel;

        }


        private void Login_Clicked(object sender, EventArgs e)
        {
             var writer = new Action<string>(s => this.viewModel.SetMessage(s));

            var client = new ConsoleClient(ServerUri, "android1", writer);
            client.ConnectAsync();

            this.viewModel.SetMessage("Connected ...");

            //MessageTextBox.HeightRequest = 200;
            //MessageTextBox.Text = "Connected";
        }

    }

    public class MainPageViewModel : INotifyPropertyChanged
    {
        public string  Message { get; set; }

        public void SetMessage(string message)
        {
            this.Message = message;
            OnPropertyChanged(nameof(Message));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    //internal class ConsoleClient
    //{
    //    private readonly HubConnection connection;
    //    private readonly string login;
    //    private readonly Action<string> writer;

    //    public ConsoleClient(string serverUri, string login, Action<string> writer)
    //    {
    //        this.login = login;
    //        this.writer = writer;
    //        this.connection = new HubConnectionBuilder().WithUrl(serverUri).Build();
    //    }


    //    public async void ConnectAsync()
    //    {
    //        this.connection.Closed += async error =>
    //        {
    //            await Task.Delay(new Random().Next(0, 5) * 1000);
    //            await this.connection.StartAsync();
    //        };

    //        this.connection.On<string>(
    //            "displayMessage",
    //            message =>
    //            {
    //                var formattedMessage = $"From Server: {message}";
    //                this.writer(formattedMessage);
    //            });

    //        await this.connection.StartAsync();
    //    }

    //    public void SendMessage(string message)
    //    {
    //        this.connection.InvokeCoreAsync("Send", new object[] {this.login, message});
    //    }
    //}

}
