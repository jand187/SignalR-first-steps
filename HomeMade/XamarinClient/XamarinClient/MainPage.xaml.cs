using System;
using CommonStandard;
using Xamarin.Forms;

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
        }
    }
}
