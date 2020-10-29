using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using static Dojo3.Delegates;

namespace Dojo3
{
    class MainViewModel : INotifyPropertyChanged                    //interface einfügen 
    {
        public ObservableCollection<Aktor> AktorenList { get; set; }
        public ObservableCollection<Aktor> SensorenList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;       //automatisch implementiert
        public RelayCommand GenerateAktorenBtnClickedCmd { get; set; }
        public RelayCommand GenerateSensorenBtnClickedCmd { get; set; }
        public DateTime currentTimeDate = DateTime.Now;
        private Informer CounterEllapsedInformer;

        //private delegate void Informer(DateTime source);
        private Informer call;

        //DispatcherTimer timer;

        private DateTime _now;

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


            /*_now = DateTime.Now;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);  //0 = h, 0 = min, dann sec
            timer.Tick += TimerReady;                //nach += Tabulator drücken!
            // += bedeutet trage mich in die Liste ein => in die Liste für dieses Event
            timer.Start();*/

            //for time /date update
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 40);
            timer.Tick += TimerUpdate;
        }

        public DateTime TimerAnzeige
        {
            get { return currentTimeDate; }
            set { currentTimeDate = value; RaisePropertyChanged(); }
        }

        private void TimerUpdate(object sender, EventArgs e)
        {
            TimerAnzeige = DateTime.Now;
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

        public DateTime CurrentDateTime
        {
            get { return _now; }
            private set
            {
                _now = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TimeAnzeige"));
            }
        }

        private void TimerReady(object sender, EventArgs e)
        {
            TimerAnzeige = DateTime.Now;
            //DateTime.Now.ToString("hh:mm");
        }
    }
}
