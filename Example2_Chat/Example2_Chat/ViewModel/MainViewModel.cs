using Example2_Chat.Communication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using Telnet_Client;

namespace Example2_Chat.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public string label1 { get; set; }
        public string label2 { get; set; }
        public string selectedUserName { get; set; }

        public ObservableCollection<ClientUser> ConnectedUsers { get; set; }
        public ObservableCollection<string> prepusermessage { get; set; }
        public ObservableCollection<string> preptimestamp { get; set; }

        private ClientUser selectedUser;

        public ClientUser SelectedUser
        {
            get { return selectedUser; }
            set 
            {
                selectedUser = value;
                RaisePropertyChanged();
                selectedUserName = value.Username;
                RaisePropertyChanged("SelectedUserName");
            }
        }

        // Kommunikation: 
        private Server server;

        public RelayCommand StartReceivingBtnClickCmd { get; set; }
        bool isConnected = false;
        public ObservableCollection<string> Chatmessages{ get; set; }

        public string UsernameEingabe { get; set; }

        public MainViewModel()
        {
            label1 = "Registered\nUsers:";
            label2 = "Chat\nHistory:";
            preptimestamp = new ObservableCollection<string>();
            preptimestamp.Add("14:34");
            preptimestamp.Add("14:39");
            prepusermessage = new ObservableCollection<string>();
            prepusermessage.Add("Hello there");
            prepusermessage.Add("Oh noo!");

            ConnectedUsers = new ObservableCollection<ClientUser>();
            Chatmessages = new ObservableCollection<string>();

            ConnectedUsers.Add(new ClientUser()
            {
                Username = "Felicity",
                singleUserMessage = prepusermessage,
                userTimestamp = preptimestamp
            }) ;
            ConnectedUsers.Add(new ClientUser() { Username = "Dr. Watson"});
            ConnectedUsers.Add(new ClientUser() { Username = "Bruce Wayne"});

            StartReceivingBtnClickCmd = new RelayCommand(
                () => {
                    server = new Server(UpdateGuiWithNewMessage);
                    isConnected = true;
                },      // Action what to do after button clicked
                () => { return !isConnected; });     // Button klickbar?
        }

        public void UpdateGuiWithNewMessage(string message)
        {
            //switch thread to GUI thread to write to GUI       // Prof: sorgt dafür, dass der Thread in der GUI ausgeführt wird
            //damit es gleichzeitig abläuft 
            //(Dispatcher extra erstellen => Render (Hintergrund) und GUI Threads => aktualisieren GUI)

            App.Current.Dispatcher.Invoke(() =>
            {
                //string name = message;
                string name = message.Split(':')[0];
                string singlemessageWithEnter = message.Split(':')[1];
                string singlemessage = singlemessageWithEnter.Replace("\r\n", string.Empty);

                string hms = DateTime.Now.ToString("hh:mm:ss");

                bool existingUser = false; 
                foreach (var clientuserobj in ConnectedUsers)
                {
                    if (clientuserobj.Username.Equals(name))                               //!ConnectedUsers.Username.Contains(name)
                    {//not in list => add it
                        existingUser = true;

                        clientuserobj.singleUserMessage.Add(singlemessage);
                        clientuserobj.userTimestamp.Add(hms);
                    }
                }

                if (!existingUser) {
                    ObservableCollection<string> prepsingleUserMessage = new ObservableCollection<string>();
                    ObservableCollection<string> prepuserTimestamp = new ObservableCollection<string>();
                    prepsingleUserMessage.Add(singlemessage);
                    prepuserTimestamp.Add(hms);

                    ConnectedUsers.Add(new ClientUser() {
                        Username = name,
                        singleUserMessage = prepsingleUserMessage,
                        userTimestamp = prepuserTimestamp
                    });
                }
                
                //neue Nachricht zu Nachrichten-Collection hinzufügen
                Chatmessages.Add(name + ": " + singlemessage);

                // GUI informieren, dass Nachrichtenanzahl gestiegen ist
                RaisePropertyChanged("ConnectedUsers");
                RaisePropertyChanged("Chatmessages");
            });
        }
    }
}