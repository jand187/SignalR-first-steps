using System.ComponentModel;
using System.Runtime.CompilerServices;
using XamarinClient.Annotations;

namespace XamarinClient
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public string Message { get; set; }

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
}