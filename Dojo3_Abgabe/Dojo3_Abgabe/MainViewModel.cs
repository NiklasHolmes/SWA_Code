using Dojo3_Abgabe;
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
using static Dojo3_Abgabe.Delegates;

namespace Dojo3_Abgabe
{
    class MainViewModel : INotifyPropertyChanged                    //interface einfügen (für Uhrzeit)
    {
        public ObservableCollection<Aktor> AktorenList { get; set; }
        public Aktor[] AktorenPrepared { get; set; }
        public Sensor[] SensorenPrepared { get; set; }
        public ObservableCollection<Sensor> SensorenList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;       //automatisch implementiert
        public RelayCommand GenerateAktorenBtnClickedCmd { get; set; }
        public RelayCommand DeleteAktorenBtnClickedCmd { get; private set; }
        public RelayCommand DeleteSensorenBtnClickedCmd { get; private set; }
        public RelayCommand GenerateSensorenBtnClickedCmd { get; set; }
        public RelayCommand DecreaseSpeakerBtnClickedCmd { get; set; }
        public RelayCommand IncreaseSpeakerBtnClickedCmd { get; set; }

        private Informer CounterEllapsedInformer;

        //public DateTime TimeAnzeige { get; private set; }                   // ist schon unten
        private DispatcherTimer timer = new DispatcherTimer();
        private DateTime _now;

        private Aktor selectedAktor; // variable | class member
        private Sensor selectedSensor; // variable | class member

        //private delegate void Informer(DateTime source);
        //private Informer call;

        public int NewID = 0;
        public int NewIDSens = 0;
        public int SpeakerInt = 5;
        public string SpeakerPicLink = "Speaker Decrease - 01.png";

        public MainViewModel()
        {
            AktorenList = new ObservableCollection<Aktor>();
            SensorenList = new ObservableCollection<Sensor>();
            GenerateDemoEntries();

            //LoadData();

            //var demodata = new DemoDataGenerator(CounterEllapsedInformer, 5, DataGeneratedFromGenerator);       // jz kennt UILogic die Klasse

            // Aktoren Action:
            GenerateAktorenBtnClickedCmd = new RelayCommand(() =>
            {
                AddAktorToList();
            }, () => { return true; });

            DeleteAktorenBtnClickedCmd = new RelayCommand(DeleteAktor,
                () => { if (selectedAktor == null) return false; else return true; }); // anonyme Methode!!

            // Sensoren Action: 
            GenerateSensorenBtnClickedCmd = new RelayCommand(() =>
            {
                AddSensorToList();
            }, () => { return true; });

            DeleteSensorenBtnClickedCmd = new RelayCommand(DeleteSensor,
                () => { if (selectedSensor == null) return false; else return true; }); // anonyme Methode!!

            _now = DateTime.Now;                            // aktuelle Zeit
            //timer.Interval = TimeSpan.FromSeconds(1);       //setze Intervall => jede Sekunde               new TimeSpan(0, 0, 1);  //0 = h, 0 = min, dann sec
            timer.Interval = TimeSpan.FromMilliseconds(250);        // ohne Milliseconds überspringt er manchmal Sekunden
            timer.Tick += new EventHandler(TimerReady);     // Methodenaufruf => TimerReady
            // += bedeutet trage mich in die Liste ein => in die Liste für dieses Event
            timer.Start();                                // Timer starten

            // Speaker Action:
            IncreaseSpeakerBtnClickedCmd = new RelayCommand(() =>
            {
                IncreaseSpeaker();
            }, () => { return true; });

            DecreaseSpeakerBtnClickedCmd = new RelayCommand(() =>
            {
                DecreaseSpeaker();
            }, () => { return true; });


        }

        private void AddAktorToList()
        {
            if (NewID > 7) {
                NewID = 0;
            }
            AktorenList.Add(AktorenPrepared[NewID]);
            NewID++;
        }
        private void AddSensorToList()
        {
            if (NewIDSens > 7)
            {
                NewIDSens = 0;
            }
            SensorenList.Add(SensorenPrepared[NewIDSens]);
            NewIDSens++;
        }

        private void DeleteAktor()
        {
            AktorenList.Remove(SelectedAktor);     //durch SelectedItem
            NotifyPropertyChanged("AktorenList");
        }
        private void DeleteSensor()
        {
            SensorenList.Remove(SelectedSensor);     //durch SelectedItem
            NotifyPropertyChanged("SensorenList");
        }

        public Aktor SelectedAktor    // Property (großgeschrieben weil Methode)
        {
            get { return selectedAktor; }
            set
            {
                selectedAktor = value;
                // GUI informieren wenn sich etwas ändert => fragr dann das mitgegebene Property nach Wert
            }
        }

        public Sensor SelectedSensor    // Property (großgeschrieben weil Methode)
        {
            get { return selectedSensor; }
            set
            {
                selectedSensor = value;
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
            AktorenPrepared = new Aktor[8];
            AktorenPrepared[0] = new Aktor(CounterEllapsedInformer) //constructor used to provide instance of Informer delegate to Aktor Object
            {
                Id = 100,
                Description = "Beschreibung",
                Name = 2.5,
                Room = "Raum",
                PosX = 7.5,
                PosY = 4.34,
                ValueType = "WertTyp",
                ItemType = "irgendwas",
                Mode = "Modus",
                Value = true,
                IsInDesignMode = "Nope"
            };
            AktorenPrepared[1] = new Aktor(CounterEllapsedInformer) { Id = 101, Description = "Licht Wohnzimmer", Name = 2.5, Room = "WZ", PosX = 7.5, PosY = 3.5, ValueType = "Boolean", ItemType = "irgendwas", Mode = "Auto", Value = true, IsInDesignMode = " " };
            AktorenPrepared[2] = new Aktor(CounterEllapsedInformer) { Id = 102, Description = "Licht Aussen", Name = 2.6, Room = "Garten", PosX = 6.3, PosY = 9.0, ValueType = "Boolean", ItemType = "irgendwas", Mode = "Auto", Value = true, IsInDesignMode = " " };
            AktorenPrepared[3] = new Aktor(CounterEllapsedInformer) { Id = 103, Description = "Licht Badezimmer", Name = 2.3, Room = "Bad", PosX = 5.7, PosY = 2.4, ValueType = "Boolean", ItemType = "irgendwas", Mode = "Auto", Value = true, IsInDesignMode = " " };
            AktorenPrepared[4] = new Aktor(CounterEllapsedInformer) { Id = 104, Description = "Dose Badezimmer", Name = 2.2, Room = "Bad", PosX = 9.2, PosY = 9.5, ValueType = "Boolean", ItemType = "irgendwas", Mode = "Auto", Value = true, IsInDesignMode = " " };
            AktorenPrepared[5] = new Aktor(CounterEllapsedInformer) { Id = 105, Description = "Dose Wohnzimmer", Name = 2.1, Room = "WZ", PosX = 6.3, PosY = 1.8, ValueType = "Boolean", ItemType = "irgendwas", Mode = "Auto", Value = true, IsInDesignMode = " " };
            AktorenPrepared[6] = new Aktor(CounterEllapsedInformer) { Id = 106, Description = "Licht Küche", Name = 1.7, Room = "Küche", PosX =6.3, PosY = 3.7, ValueType = "Boolean", ItemType = "irgendwas", Mode = "Auto", Value = true, IsInDesignMode = " " };
            AktorenPrepared[7] = new Aktor(CounterEllapsedInformer) { Id = 107, Description = "Leselampe Wohnzimmer", Name = 3.8, Room = "WZ", PosX = 1.7, PosY = 6.2, ValueType = "Boolean", ItemType = "irgendwas", Mode = "Auto", Value = true, IsInDesignMode = " " };

            SensorenPrepared = new Sensor[8];
            SensorenPrepared[0] = new Sensor(CounterEllapsedInformer) //constructor used to provide instance of Informer delegate to Aktor Object
            {
                Id = 1,
                Description = "Beschreibung",
                Name = 2.5,
                Room = "Raum",
                PosX = 7.5,
                PosY = 4.34,
                ValueType = "WertTyp",
                Status = true,
                Mode = "Modus",
                Value = 3.45
            };

            SensorenPrepared[1] = new Sensor(CounterEllapsedInformer) {Id = 2, Description = "TA Wohnzimmer", Name = 0.01, Room = "WZ", PosX = 0, PosY = 0, ValueType = "Boolean", Status = true, Mode = "Enabled", Value = 3.45};
            SensorenPrepared[2] = new Sensor(CounterEllapsedInformer) {Id = 3, Description = "TA Badezimmer", Name = 0.02, Room = "Bad", PosX = 0, PosY = 0, ValueType = "Boolean", Status = true, Mode = "Enabled", Value = 3.45 };
            SensorenPrepared[3] = new Sensor(CounterEllapsedInformer) {Id = 4, Description = "IR Haustüre", Name = 0.03, Room = "Garderobe", PosX = 0, PosY = 0, ValueType = "Boolean", Status = true, Mode = "Enabled", Value = 3.45 };
            SensorenPrepared[4] = new Sensor(CounterEllapsedInformer) {Id = 5, Description = "IR Wohnzimmer", Name = 0.04, Room = "WZ", PosX = 0, PosY = 0, ValueType = "Boolean", Status = true, Mode = "Enabled", Value = 3.45 };
            SensorenPrepared[5] = new Sensor(CounterEllapsedInformer) {Id = 6, Description = "Temp Badezimmer", Name = 0.05, Room = "Bad", PosX = 0, PosY = 0, ValueType = "Single", Status = true, Mode = "Enabled", Value = 3.45 };
            SensorenPrepared[6] = new Sensor(CounterEllapsedInformer) {Id = 7, Description = "Temp Aussen Nord", Name = 0.06, Room = "Garten", PosX = 0, PosY = 0, ValueType = "Single", Status = true, Mode = "Enabled", Value = 3.45 };
            SensorenPrepared[7] = new Sensor(CounterEllapsedInformer) { Id = 8, Description = "Dämmerungssensor Nord", Name = 0.07, Room = "Garten", PosX = 0, PosY = 0, ValueType = "Int32", Status = true, Mode = "Enabled", Value = 3.45 };

            /*
            // Alter Version:
            var temp = new Aktor(CounterEllapsedInformer) //constructor used to provide instance of Informer delegate to Aktor Object
            {
                Id = NewID,
                Description = "Beschreibung",
                Name = 2.5,
                Room = "Raumo",
                PosX = 7.5,
                PosY = 4.34,
                ValueType = "halloo",
                ItemType = "irgendwas",
                Mode = "moodus",
                Value = true,
                IsInDesignMode = "ssdfsd"
            };
            AktorenList.Add(temp);
            */
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

        public String SpeakerString
        {
            get { return SpeakerInt.ToString(); }
            private set
            {
                //SpeakerString = value;
                NotifyPropertyChanged("SpeakerString");
            }
        }

        private void IncreaseSpeaker()
        {
            if (SpeakerInt <= 0)
            {
                SpeakerPic = "Speaker Decrease - 01.png";
            }
            SpeakerInt++;
            SpeakerString = SpeakerInt.ToString();
        }

        private void DecreaseSpeaker()
        {
            if (SpeakerInt <= 1)
            {
                SpeakerInt = 0;
                SpeakerPic = "Speaker Mute - 04.png";
            }
            else {
                SpeakerInt--;
            }
            SpeakerString = SpeakerInt.ToString();
        }
        /*
        Speaker Decrease - 01.png
        Speaker Mute - 04.png
        */
        public String SpeakerPic
        {
            get { return SpeakerPicLink; }
            private set
            {
                SpeakerPicLink = value;
                NotifyPropertyChanged("SpeakerPic");
            }
        }

    }
}
