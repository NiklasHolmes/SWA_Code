using Example1_Toggle.Communication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Text;

namespace Example1_Toggle.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region  TOGGLE Value
        private bool toggle1Value;
        public bool Toggle1Value
        {
            get { return toggle1Value; }
            set { toggle1Value = value; RaisePropertyChanged(); }
        }

        private bool toggle2Value;
        public bool Toggle2Value
        {
            get { return toggle2Value; }
            set { toggle2Value = value; RaisePropertyChanged(); }
        }
        private bool toggle3Value;
        public bool Toggle3Value
        {
            get { return toggle3Value; }
            set { toggle3Value = value; RaisePropertyChanged(); }
        }
        private bool toggle4Value;
        public bool Toggle4Value
        {
            get { return toggle4Value; }
            set { toggle4Value = value; RaisePropertyChanged(); }
        }
        #endregion

        private Server server;
        private const int port = 10100;
        private const string ip = "127.0.0.1";
        private Client clientcom;

        public RelayCommand ConnectBtnClickCmd { get; set; }
        public RelayCommand ListenBtnClickCmd { get; set; }
        public RelayCommand Toggle1Cmd { get; set; }
        public RelayCommand Toggle2Cmd { get; set; }
        public RelayCommand Toggle3Cmd { get; set; }
        public RelayCommand Toggle4Cmd { get; set; }

        public ObservableCollection<string> ReceivedHistory { get; set; }

        // Button Sichtbarkeit: 
        private string toggleVis;
        public string ToggleVis
        {
            get { return toggleVis; }
            set { toggleVis = value; RaisePropertyChanged(); }
        }

        public bool ServerCon { get; set; }
        public bool ClientCon { get; set; }

        private DateTime _now;

        public MainViewModel()
        {
            ReceivedHistory = new ObservableCollection<string>();

            ToggleVis = "Visible";

            ServerCon = false;
            ClientCon = false;

            ConnectBtnClickCmd = new RelayCommand(
                () =>
                {
                    server = new Server(ip, port, GuiUpdater);
                    //server.StartAcceptingClients();
                    ServerCon = true;
                },
                () => { return (!ServerCon && !ClientCon); }
            );

            ListenBtnClickCmd = new RelayCommand(
                () =>
                {
                    ClientCon = true;
                    ToggleVis = "Hidden";
                    clientcom = new Client(ip, port, new Action<string>(NewMessageReceived));
                },
                () => { return (!ServerCon && !ClientCon); }
            );

            #region TOGGLE BUTTONS
            Toggle1Cmd = new RelayCommand(
                () =>
                {
                    Toggle1Value = !toggle1Value;

                    // Message: 
                    string value = Toggle1Value.ToString();
                    string hms = DateTime.Now.ToString("hh:mm:ss");
                    string dmy = DateTime.Now.ToString("dd:MM:yyyy");

                    //string time = "16:45";
                    string message = ("1x" + value + "x" + hms);

                    ReceivedHistory.Add("Toggle 1 " + value + " " + hms);
                    RaisePropertyChanged("ReceivedHistory");

                    server.SendMessage(message);
                },
                () => { return ServerCon; }
            );
            Toggle2Cmd = new RelayCommand(
                () =>
                {
                    Toggle2Value = !toggle2Value;

                    // Message: 
                    string value = Toggle2Value.ToString();
                    string hms = DateTime.Now.ToString("hh:mm:ss");
                    string dmy = DateTime.Now.ToString("dd:MM:yyyy");
                    string message = ("2x" + value + "x" + hms);

                    ReceivedHistory.Add("Toggle 2 " + value + " " + hms);
                    RaisePropertyChanged("ReceivedHistory");

                    server.SendMessage(message);
                },
                () => { return ServerCon; }
            );
            Toggle3Cmd = new RelayCommand(
                () =>
                {
                    Toggle3Value = !toggle3Value;
                    // Message: 
                    string value = Toggle3Value.ToString();
                    string hms = DateTime.Now.ToString("hh:mm:ss");
                    string dmy = DateTime.Now.ToString("dd:MM:yyyy");
                    string message = ("3x" + value + "x" + hms);
                    ReceivedHistory.Add("Toggle 3 " + value + " " + hms);
                    RaisePropertyChanged("ReceivedHistory");
                    server.SendMessage(message);
                },
                () => { return ServerCon; }
            );
            Toggle4Cmd = new RelayCommand(
                () =>
                {
                    Toggle4Value = !toggle4Value;
                    // Message: 
                    string value = Toggle4Value.ToString();
                    string hms = DateTime.Now.ToString("hh:mm:ss");
                    string dmy = DateTime.Now.ToString("dd.MM.yyyy");
                    string message = ("4x" + value + "x" + hms);
                    ReceivedHistory.Add("Toggle 4 " + value + " " + dmy);
                    RaisePropertyChanged("ReceivedHistory");
                    server.SendMessage(message);
                },
                () => { return ServerCon; }
            );
            #endregion
        }


        // Client:
        private void NewMessageReceived(string message)
        {
            //write new message in Collection to display in GUI
            //switch thread to GUI thread to avoid problems
            App.Current.Dispatcher.Invoke(() =>
            {
                if (message != null) {
                    string newvalue = "[red]";
                    string[] toggle = message.Split('x');
                    if (toggle[1] == "True")
                    {
                        newvalue = "[green]";
                    }
                    else
                    {
                        newvalue = "[red]";
                    }

                    ReceivedHistory.Add("Von Server: Toggle " + toggle[0] + " " + newvalue + " " + toggle[2]);

                    switch (toggle[0])
                    {
                        case "1":
                            Toggle1Value = !toggle1Value;
                            break;
                        case "2":
                            Toggle2Value = !toggle2Value;
                            break;
                        case "3":
                            Toggle3Value = !toggle3Value;
                            break;
                        case "4":
                            Toggle4Value = !toggle4Value;
                            break;
                        default:
                            break;
                    }
                }

            });
        }

        private void GuiUpdater() {

            int i = 0;
            foreach (var client in server.clients)
            {
                i++;
                if (i == server.clients.Count)
                {
                    foreach (var message in ReceivedHistory)
                    {
                        client.ClientSocket.Send(Encoding.UTF8.GetBytes(message));
                    }
                }
            }

        }

    }
}