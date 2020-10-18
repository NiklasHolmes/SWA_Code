using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SimpleWPF_Example
{
    public class UILogic : INotifyPropertyChanged
    {
        public ObservableCollection<Transportation> WaitingList { get; set; }
        public ObservableCollection<Transportation> ReadyList { get; set; }
        public ObservableCollection<CargoItem> SelectedCargo { get; set; }
        DispatcherTimer timer;

        //Delegate of Type Informer (own defined Delegate) used to point to CounterEllapsed Method
        Informer CounterEllapsedInformer;

        public event PropertyChangedEventHandler PropertyChanged;

        public UILogic()
        {
            WaitingList = new ObservableCollection<Transportation>();
            ReadyList = new ObservableCollection<Transportation>();

            CounterEllapsedInformer = new Informer(CounterEllapsed); //create instance of Informer and point to specific method which is called if informer is evaluated

            GenerateDemoEntries();
        }

        private void CounterEllapsed(Transportation source)
        {
            ReadyList.Add(source);
            WaitingList.Remove(source);
        }

        private void GenerateDemoEntries()
        {
            WaitingList.Add(new Transportation(CounterEllapsedInformer) //constructur used to provide instance of Informer delegate to Transportation object
            {
                Countdown = 10,
                Destination = "Vienna",
                Source = "Linz",
                Cargo = new ObservableCollection<CargoItem>()
                {
                    new CargoItem()
                    {
                        Description = "Manna Schnitten",
                        Amount=1000,
                        Weight=2
                    },
                    new CargoItem()
                    {
                        Description = "Auer Torten Ecken",
                        Amount=2000,
                        Weight=3
                    }
                }
            });
            WaitingList.Last().StartCountDown();


            WaitingList.Add(new Transportation(CounterEllapsedInformer)
            {
                Countdown = 3,
                Destination = "Innsbruck",
                Source = "bregenz",
                Cargo = new ObservableCollection<CargoItem>()
                {
                    new CargoItem()
                    {
                        Description = "Käse ecken",
                        Amount=1000,
                        Weight=2
                    },
                    new CargoItem()
                    {
                        Description = "Mozartkugeln",
                        Amount=3000,
                        Weight=500
                    }
                }
            });
            WaitingList.Last().StartCountDown();

        }


    }
}
