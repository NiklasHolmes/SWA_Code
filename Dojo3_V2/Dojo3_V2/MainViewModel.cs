using Dojo3_V2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using static Dojo3_V2.Delegates;

namespace Dojo3_V2
{
    class MainViewModel : INotifyPropertyChanged                    //interface einfügen (für Uhrzeit)
    {
        public ObservableCollection<Aktor> AktorenList { get; set; }
        public ObservableCollection<Aktor> SensorenList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;       //automatisch implementiert
        public RelayCommand GenerateAktorenBtnClickedCmd { get; set; }
        public RelayCommand DeleteAktorenBtnClickedCmd { get; private set; }
        public RelayCommand GenerateSensorenBtnClickedCmd { get; set; }
        
        private Informer CounterEllapsedInformer;

        //public DateTime TimeAnzeige { get; private set; }                   // ist schon unten
        private DispatcherTimer timer = new DispatcherTimer();
        private DateTime _now;

        private Aktor selectedDevice; // variable | class member

        //private delegate void Informer(DateTime source);
        private Informer call;   

        public MainViewModel()
        {
            AktorenList = new ObservableCollection<Aktor>();
            SensorenList = new ObservableCollection<Aktor>();

            //LoadData();

            //var demodata = new DemoDataGenerator(CounterEllapsedInformer, 5, DataGeneratedFromGenerator);       // jz kennt UILogic die Klasse

            GenerateAktorenBtnClickedCmd = new RelayCommand(() =>
            {
                GenerateDemoEntries();
            }, () => { return true; });

            DeleteAktorenBtnClickedCmd = new RelayCommand(DeleteDevice,
                () => { if (selectedDevice == null) return false; else return true; }); // anonyme Methode!!


            _now = DateTime.Now;                            // aktuelle Zeit
            timer.Interval = TimeSpan.FromSeconds(1);       //setze Intervall => jede Sekunde               new TimeSpan(0, 0, 1);  //0 = h, 0 = min, dann sec
            timer.Tick += new EventHandler(TimerReady);     // Methodenaufruf => TimerReady
            // += bedeutet trage mich in die Liste ein => in die Liste für dieses Event
            timer.Start();                                  // Timer starten

        }

        private void DeleteDevice()
        {
            AktorenList.Remove(SelectedDevice);     //durch SelectedItem
            NotifyPropertyChanged("AktorenList");
        }

        public Aktor SelectedDevice    // Property (großgeschrieben weil Methode)
        {
            get { return selectedDevice; }
            set
            {
                selectedDevice = value;
            // GUI informieren wenn sich etwas ändert => fragr dann das mitgegebene Property nach Wert
            }
        }

        private void DataGeneratedFromGenerator(Aktor obj)
        {
            AktorenList.Add(obj);
        }

        protected void NotifyPropertyChanged(string propertyname)     // immer wenn Wert ausgewählt => dann werden die Details angezeigt
        //      protected => die Erben können darauf zugreifen
        {
            if (PropertyChanged != null) // da von GUI initialisiert => nicht null
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private void GenerateDemoEntries()
        {
            var temp = new Aktor(CounterEllapsedInformer) //constructor used to provide instance of Informer delegate to Transportaion Object
            {
                Id = 3,
                Description = "Beschreibung",
                Name = "der Name",
                Room = "Raumo",
                PosX = 7,
                PosY = 4,
                ValueType = "halloo",
                ItemType = "irgendwas",
                Mode = "moodus",
                Value = 4,
                IsInDesignMode = "ssdfsd"
            };

            AktorenList.Add(temp);
        }

        private void LoadData()
        {
            //AktorenList.Add(item: new BaseActuator("namestring", "beschreibung", 5, "raum", "boolean"));

        }

        
        public DateTime TimeAnzeige
        {
            get { return _now; }
            private set
            {
                _now = value;
                NotifyPropertyChanged("TimeAnzeige");
            }
        }
        
        private void TimerReady(object sender, EventArgs e)
        {
            TimeAnzeige = DateTime.Now;
            //DateTime.Now.ToString("hh:mm");
        }

    }
}
