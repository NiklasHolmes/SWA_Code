using FlugzeugBsp_2019.Communication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlugzeugBsp_2019.ViewModel
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


        public ObservableCollection<Flug> Flights { get; set; }
        public ObservableCollection<Container> DisplayedContainer { get; set; }
        public ObservableCollection<Container> Containerliste1 { get; set; }
        public ObservableCollection<Produkt> Produkte { get; set; }

        public Container Container1 { get; set; }
        public Container Container2 { get; set; }

        private Flug selectedFlight;

        public Flug SelectedFlight
        {
            get { return selectedFlight; }
            set { 
                selectedFlight = value;
                RaisePropertyChanged();
                DisplayContainer(value.Flugnummer);
                RaisePropertyChanged("DisplayedContainer");
                ProduktanzahlBerechnen();
            }
        }

        private int produktanzahl;

        public int Produktanzahl
        {
            get { return produktanzahl; }
            set { 
                produktanzahl = value;
                RaisePropertyChanged("Produktanzahl");
            }
        }

        public RelayCommand<Container> DeleteContainerBtnClickCmd { get; set; }
        public RelayCommand DeleteAllBtnClickCmd { get; set; }


        // Kommunikation:
        Server Server;

        public MainViewModel()
        {
            Flights = new ObservableCollection<Flug>();
            DisplayedContainer = new ObservableCollection<Container>();
            Containerliste1 = new ObservableCollection<Container>();
            Container1 = new Container();
            Container2 = new Container();
            Produkte = new ObservableCollection<Produkt>();

            Produkte.Add(new Produkt() { Produktname = "Banane" });
            Produkte.Add(new Produkt() { Produktname = "XBox One" });
            Produkte.Add(new Produkt() { Produktname = "Apfel" });

            Container1.Produkte = Produkte;
            Container2.Produkte = Produkte;

            Containerliste1.Add(Container1);
            Containerliste1.Add(Container2);

            Flug Flug1 = new Flug()
            {
                Flugnummer = "F34234",
                Containerliste = Containerliste1,
                Containeranzahl = Containerliste1.Count
            };

            Flights.Add(Flug1);

            DeleteContainerBtnClickCmd = new RelayCommand<Container>(DeleteContainer, true);

            DeleteAllBtnClickCmd = new RelayCommand (DeleteAllContainer, 
                () => {
                    return CheckContainer();
            });

            if (IsInDesignMode)
            {
                SelectedFlight = Flug1;
            };


            // Kommunikation:
            Server = new Server(GuiUpdater);

        }

        public bool CheckContainer()
        {
            if (DisplayedContainer.Count > 0 && SelectedFlight != null)
            {
                return true;
            }
            else 
                return false;
        }

        public void DisplayContainer(string flugnummer)
        {
            foreach (var item in Flights)
            {
                if(item.Flugnummer.Equals(flugnummer))
                {
                    DisplayedContainer = item.Containerliste;
                    break;
                }
            }
            //RaisePropertyChanged("DisplayedToys");
            ProduktanzahlBerechnen();
        }

        public void ProduktanzahlBerechnen ()
        {
            int i = 0;
            foreach (var item in SelectedFlight.Containerliste)
            {
                i += item.Produkte.Count;
            }
            Produktanzahl = i;
            //RaisePropertyChanged("Produktanzahl");
        }

        public void DeleteContainer(Container delcon)
        {
            SelectedFlight.Containerliste.Remove(delcon);
            DisplayContainer(SelectedFlight.Flugnummer);
            ProduktanzahlBerechnen();
        }

        public void DeleteAllContainer()
        {
            SelectedFlight.Containerliste.Clear();
            DisplayContainer(SelectedFlight.Flugnummer);
            ProduktanzahlBerechnen();
        }


        private void GuiUpdater(string msg)
        {
            App.Current.Dispatcher.Invoke(
                 () =>
                 {

                     Console.WriteLine(msg);

                     // Example string: F4716:(Bananen,Autoreifen,);(Autoreifen,,)

                     string[] split = msg.Split(':');

                     /* split =
                      * [0] F4716
                      * [1] (Bananen,Autoreifen,);(Autoreifen,,)
                     */
                     Flug newFlight = new Flug()
                     {
                         Flugnummer = split[0],
                         Containerliste = new ObservableCollection<Container>()
                     };

                     // Alten Flug rauslöschen da neuer reingekommen ist:
                     foreach (var flug in Flights)
                     {
                         if (flug.Flugnummer.Equals(split[0])) {
                             Flights.Remove(flug);
                             break;
                         }
                     }

                     string[] splitContainer = split[1].Split(';');

                     /* splitContainer =
                      * [0] (Bananen,Autoreifen,)
                      * [1] (Autoreifen,,)
                      */

                     foreach (var item in splitContainer)
                     {
                         string CleanCont = item.Replace("{", "");             // "{" entfernen
                         CleanCont = CleanCont.Replace("}", "");               // "}" entfernen
                         string[] splitFreight = CleanCont.Split(',');         // splitten je "," -> [0] Bananen [1] Autoreifen

                         ObservableCollection<Produkt> Produktsammlung = new ObservableCollection<Produkt>();
                         Produktsammlung.Add(new Produkt() { Produktname = splitFreight[0] });
                         if(!splitFreight[1].Equals(""))
                         {
                             Produktsammlung.Add(new Produkt() { Produktname = splitFreight[1] });

                             if (!splitFreight[2].Equals(""))
                             {
                                 Produktsammlung.Add(new Produkt() { Produktname = splitFreight[2] });

                             }
                         }
                         Container newcontainer = new Container();
                         newcontainer.Produkte = Produktsammlung;

                         newFlight.Containerliste.Add(newcontainer);
                     }

                     newFlight.Containeranzahl = newFlight.Containerliste.Count();
                     Flights.Add(newFlight);
                 });
        }
    }
}