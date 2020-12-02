using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleChatClient;
using System.Collections.ObjectModel;

namespace WpfChatClient.ViewModel
{
    
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand ConnectBtnClikcCmd { get; set; }        // wichtig Wpf usen hier!
        public RelayCommand SendBtnClickCmd { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public ObservableCollection<string> ReceiveData { get; set; }

        bool isConnected = false;
        Client client;

        public MainViewModel()
        {
            Message = "";

            ConnectBtnClikcCmd = new RelayCommand(
                () => {

                    client = new Client(10100);
                    isConnected = true;

                },      // Action what to do after button clicked
                () => { return !isConnected; });     // is Clickable?

            SendBtnClickCmd = new RelayCommand(
                () => {

                    client.SendData(Message + "\r\n");

                },      // Action what to do after button clicked
                () => { return isConnected && Message.Length > 1; });     // is Clickable

        }
    }
}