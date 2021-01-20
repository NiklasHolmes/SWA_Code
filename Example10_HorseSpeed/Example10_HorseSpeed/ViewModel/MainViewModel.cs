using Example10_HorseSpeed.Communication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Example10_HorseSpeed.ViewModel
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

        public ObservableCollection<Item> Items { get; set; }

        public BitmapImage Image { get; set; }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }

        private int speed;

        public int Speed
        {
            get { return speed; }
            set { speed = value; RaisePropertyChanged(); }
        }

        public RelayCommand AddBtnClickCmd { get; set; }

        private string fastestHorseName;

        public string FastestHorseName
        {
            get { return fastestHorseName; }
            set { fastestHorseName = value; RaisePropertyChanged();  }
        }

        // Kommunikation:

        public bool ServerCon { get; set; }
        public bool ClientCon { get; set; }

        public Communicator com { get; set; }
        public RelayCommand ServerBtnClickCmd { get; set; }
        public RelayCommand ClientBtnClickCmd { get; set; }

        public MainViewModel()
        {
            BitmapImage Image = new BitmapImage(new Uri("../Images/Urgent.png", UriKind.Relative));
            FastestHorseName = "";

            Items = new ObservableCollection<Item>();

            if (IsInDesignMode)
            {
                Item item1 = new Item
                {
                    Name = "George",
                    Speed = 40
                };
                Items.Add(item1);
            }

            /*
            // Alt:
            AddBtnClickCmd = new RelayCommand(
               () =>
               {
                   Item newItem = new Item()
                   {
                       Name = Name,
                       Speed = Speed
                   };
                   Items.Add(newItem);
                   string newName = getFastestHorse(); // unnötig
                                                       //FastestHorseName = newName;
                  },
               () => { return !ServerCon && ClientCon; }
            );*/

            AddBtnClickCmd = new RelayCommand(
               () =>
               {
                   Item newItem = new Item()
                   {
                       Name = Name,
                       Speed = Speed
                   };
                   string NewData = Name + "|" + Speed;
                   com.Send(Encoding.UTF8.GetBytes(NewData));
                   GuiUpdater(NewData);

               },
               () => { return !ServerCon && ClientCon; }
            );

            ServerCon = false;
            ClientCon = false;

            ServerBtnClickCmd = new RelayCommand(() => 
                { 
                    com = new Communicator(true, GuiUpdater);
                    ServerCon = true;
                },
                () => { return !ServerCon && !ClientCon; ; });

            ClientBtnClickCmd = new RelayCommand(() =>
            {
                com = new Communicator(false, GuiUpdater);
                ClientCon = true;
                //com.Send(Encoding.UTF8.GetBytes("newclient"));
            },
                () => { return !ServerCon && !ClientCon; });
        }

        public string getFastestHorse() {

            int i = 0;
            FastestHorseName = "";
            foreach (var item in Items)
            {
                if(item.Speed >= i)
                {
                    i = item.Speed;
                    FastestHorseName = item.Name;
                }
            }
            return fastestHorseName;
        }

        private void GuiUpdater(string newData)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                string[] splitData = newData.Split('|');

                if (newData.Equals("newclient")) {
                    string data = "";
                    foreach (Item h in Items)
                    {
                        data = h.Name + "|" + h.Speed.ToString();
                        //com.clients.Last().Send(data);
                        com.clients.Last().Send(data);
                    }
                }
                else if (ServerCon || ClientCon)
                {
                    string newname = splitData[0];
                    int newspeed = Int32.Parse(splitData[1]);
                    Item newItem = new Item()
                    {
                        Name = newname,
                        Speed = newspeed
                    };
                    foreach (var item in Items)
                    {
                        if (item.Name.Equals(newname))
                        {
                            Items.Remove(item);
                            break;
                        }
                    }
                    Items.Add(newItem);
                }

                getFastestHorse();

                //RaisePropertyChanged("Items");
            });
        }
    }
}