using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dojo4_Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Client clientcom;
        public string ClientName { get; set; }
        public ObservableCollection<string> MessagesReceived { get; set; }
        public RelayCommand SendBtnClickedCmd { get; set; }
        public RelayCommand ConnectClientBtnClickedCmd { get; set; }
        public bool isConnected = false;
        public string NewMessage { get; set; }

        public MainViewModel()
        {
            NewMessage = "";
            ClientName = "";
            MessagesReceived = new ObservableCollection<string>();

            ConnectClientBtnClickedCmd = new RelayCommand(ConnectClient,
                () => { return (!isConnected && ClientName.Length >= 1); }); // anonyme Methode!!

            SendBtnClickedCmd = new RelayCommand(SendMessage,
                () => { return (isConnected && NewMessage.Length >= 1); }); // anonyme Methode!!
        }

        private void SendMessage()
        {
            clientcom.SendMessage(ClientName + ": " + NewMessage);  // Clients sollen den Namen vor der Nachricht haben
            MessagesReceived.Add("YOU: " + NewMessage);             // der Sender soll statt eigenem Namen ein YOU haben

            // EXTRA: wenn Nachricht abgeschickt => Nachricht aus Eingabefeld löschen!
            NewMessage = "";
            RaisePropertyChanged("NewMessage");

            // Testzwecke:
            //messagesReceived = "Hallo";
            //RaisePropertyChanged("MessagesReceived");
        }

        private void ConnectClient()
        {
            isConnected = true;
            clientcom = new Client("127.0.0.1", 10100, new Action<string>(NewMessagesReceived), ClientDisconnected);
        }

        private void ClientDisconnected()
        {
            isConnected = false;
            // überprüft alle CanExecute der commands (würde sonst möglicherweise Fehler werfen?)
            CommandManager.InvalidateRequerySuggested();        // canExecute => für die Buttons  ~ ob enabled oder disabled
        }

        private void NewMessagesReceived(string message)
        {
            //write new message in Collection to display in GUI
            //switch thread to GUI thread to avoid problems
            App.Current.Dispatcher.Invoke(() =>
            {
                MessagesReceived.Add(message);
            });
        }

    }
}
