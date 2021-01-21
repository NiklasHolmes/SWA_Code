using Client_wi19b040.Communication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Threading;

namespace Client_wi19b040.ViewModel
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

        public ObservableCollection<Item> DisplayedItems { get; set; }

        public RelayCommand ExitBtnClickCmd { get; set; }

        private int buttonHeight;

        public int ButtonHeight
        {
            get { return buttonHeight; }
            set { buttonHeight = value; RaisePropertyChanged(); }
        }

        private string statusVisible;
        public string StatusVisible
        {
            get { return statusVisible; }
            set { statusVisible = value; RaisePropertyChanged(); }
        }

        private string notificationText;
        public string NotificationText
        {
            get { return notificationText; }
            set { notificationText = value; RaisePropertyChanged(); }
        }

        private DispatcherTimer timer;

        public Client client { get; set; }

        public MainViewModel()
        {
            StatusVisible = "Hidden";
            ButtonHeight = 80;

            DisplayedItems = new ObservableCollection<Item>();

            /*
            Item Item1 = new Item() { Name = "CPU4", ID = 45, State = "danger", value = 43 };
            Item Item2 = new Item() { Name = "CPU5", ID = 23, State = "normal", value = 43 };

            DisplayedItems.Add(Item1);
            DisplayedItems.Add(Item2);
            */
            

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += Timer_Tick;

            client = new Client(GuiUpdater);

            ExitBtnClickCmd = new RelayCommand(
               () =>
               {
                   client.Send(Encoding.UTF8.GetBytes("QUIT"));

                   //  funktioniert nur bedingt: => Problem ist dass er mittendrin abbricht => wirft Fehler
                   //  unten stehende Funktion auskommentieren dann kommt natürlich keine Fehlermeldung mehr 
                   client.CloseClientServerConnection();
               },
               () => { return true;  }
            );
        }

        public void ShowNotification(Item item)
        {
            StatusVisible = "Visible";
            ButtonHeight = 40;
            string hms = DateTime.Now.ToString("hh:mm:ss");

            string notstring = hms + ": " + item.Name + " state " + item.State + "!";
            NotificationText = notstring;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)     // nach 2 Sekunden geht er hier rein
        {
            StatusVisible = "Hidden";
            ButtonHeight = 80;
            NotificationText = "";
            timer.Stop();
        }

        private void GuiUpdater(string newData)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                // Nachricht: CPU1: 70 (warning)\r\n       Backup Errors1: 0 (warning)
                string noEnter = newData.Replace("\r\n", string.Empty);

                string[] splitData = noEnter.Split(':');
                //CPU1:70(warning)

                string newname = splitData[0];      // CPU1   oder Backup Errors1

                //  eist die Zahl nach CPU (namen) die ID?  erst am Schluss gecheckt aber so könnte man dann die ID extra rausbekommen und extra abspeichern
                //  in meinem Fall is die ID jetzt auch unter Name (Key) zu finden
                if(newname.Contains("1") || newname.Contains("2") || newname.Contains("3") || newname.Contains("4") || newname.Contains("5"))
                {
                    string idvll = newname.Substring(newname.Length - 1,1);
                    string newid = idvll;
                }

                string noBlank = splitData[1].Replace(" ", string.Empty);  // 70(warning)

                string[] splitData2 = noBlank.Split('(');      // [0]70  [1]warning)

                int newvalue = Int32.Parse(splitData2[0]);
                string newstate = splitData2[1].Replace(")", string.Empty);

                Item newItem = new Item()
                {
                    //ID = newid,
                    Name = newname,
                    Value = newvalue,
                    State = newstate
                };
                bool existisAlready = false;
                foreach (var item in DisplayedItems)
                {
                    if (item.Name.Equals(newname))
                    {
                        //DisplayedItems.Remove(item);  // früher mal removed aber da sprangen die Items stark herum
                        item.Value = newvalue;
                        item.State = newstate;
                        existisAlready = true;
                        break;
                    }
                }
                if(!existisAlready)
                {
                    DisplayedItems.Add(newItem);
                }

                if(newItem.State.Equals("danger"))
                {
                    ShowNotification(newItem);
                }
            });
        }
    }
}