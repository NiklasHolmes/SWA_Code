using Dojo4_Server.Communication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo4_Server.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Server server;
        public bool Connected = false;
        private const int port = 10100;
        private const string ip = "127.0.0.1";

        public RelayCommand StartConnectBtnClickedCmd { get; set; }
        public RelayCommand StopConnectBtnClickedCmd { get; set; }
        public RelayCommand DropUserBtnClickedCmd { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public ObservableCollection<string> ConnectedClients { get; set; }
        public string SelectedClient { get; set; }

        public int MessagesCnt
        {
            get
            {
                return Messages.Count;  // Anzahl an Nachrichten
            }
        }

        public MainViewModel()
        {
            Messages = new ObservableCollection<string>();
            ConnectedClients = new ObservableCollection<string>();

            StartConnectBtnClickedCmd = new RelayCommand(StartConnBtnClicked,
                () => { return !Connected; });

            StopConnectBtnClickedCmd = new RelayCommand(StopConnBtnClicked,
                () => { return Connected; });

            DropUserBtnClickedCmd = new RelayCommand(
                () =>   //anonyme Methode:
                {
                    server.DisconnectOneClient(SelectedClient);  //
                    ConnectedClients.Remove(SelectedClient);             // remove from GUI listbox
                },  
                () => { return (Connected && SelectedClient != null); });
        }

        private void StartConnBtnClicked()
        {
            server = new Server(ip, port, UpdateGuiWithNewMessage);
            server.StartAcceptingClients();

            //RaisePropertyChanged("MessagesCnt"); //für Testzwecke
            Connected = true;
        }
        private void StopConnBtnClicked()
        {
            server.StopConnection();
            ConnectedClients.Clear();
            Connected = false;
        }

        public void UpdateGuiWithNewMessage(string message)
        {
            //switch thread to GUI thread to write to GUI       // Prof: sorgt dafür, dass der Thread in der GUI ausgeführt wird
            //damit es gleichzeitig abläuft 
            //(Dispatcher extra erstellen => Render (Hintergrund) und GUI Threads => aktualisieren GUI)

            App.Current.Dispatcher.Invoke(() =>
            {
                string name = message.Split(':')[0];
                if (!ConnectedClients.Contains(name))
                {//not in list => add it
                    ConnectedClients.Add(name);
                }
                if (message.Contains("@quit"))
                {
                    server.DisconnectOneClient(name);
                    // Extra:
                    ConnectedClients.Remove(name);      // Client auch aus connectedClients Liste löschen
                }
                //neue Nachricht zu Nachrichten-Collection hinzufügen
                Messages.Add(message);

                // GUI informieren, dass Nachrichtenanzhal gestiegen ist
                RaisePropertyChanged("MessagesCnt");
            });
        }
    }
}
