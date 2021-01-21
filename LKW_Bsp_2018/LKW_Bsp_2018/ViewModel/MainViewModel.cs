using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LKW_Bsp_2018.Communication;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LKW_Bsp_2018.ViewModel
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
        public ObservableCollection<LKW> LKWs { get; set; }
        public ObservableCollection<Ladung> DisplayedLadung { get; set; }
        public ObservableCollection<Ladung> Ladungliste1 { get; set; }
        public ObservableCollection<Produkt> Produkte { get; set; }

        public Ladung Ladung1 { get; set; }
        public Ladung Ladung2 { get; set; }

        private LKW selectedLKW;

        public LKW SelectedLKW
        {
            get { return selectedLKW; }
            set
            {
                selectedLKW = value;
                RaisePropertyChanged();
                if(value != null)
                    DisplayLadung(value.ID);
                RaisePropertyChanged("DisplayedLadung");
                ProduktanzahlBerechnen();
            }
        }

        private int produktanzahl;

        public int Produktanzahl
        {
            get { return produktanzahl; }
            set
            {
                produktanzahl = value;
                RaisePropertyChanged("Produktanzahl");
            }
        }

        public RelayCommand<Ladung> DeleteLadungBtnClickCmd { get; set; }
        public RelayCommand DeleteAllBtnClickCmd { get; set; }


        // Kommunikation:
        Server Server;

        public MainViewModel()
        {

            if(IsInDesignMode)
            {
                // für Design:
                LKWs = new ObservableCollection<LKW>();
                DisplayedLadung = new ObservableCollection<Ladung>();
                Ladungliste1 = new ObservableCollection<Ladung>();
                Ladung1 = new Ladung();
                Ladung2 = new Ladung();
                Produkte = new ObservableCollection<Produkt>();

                Produkte.Add(new Produkt() { Produktname = "Reis" });
                Produkte.Add(new Produkt() { Produktname = "Apfel" });

                Ladung1.Produkte = Produkte;
                Ladung2.Produkte = Produkte;

                Ladungliste1.Add(Ladung1);
                Ladungliste1.Add(Ladung2);

                LKW LKW2 = new LKW()
                {
                    ID = "F34234",
                    Ladungliste = Ladungliste1,
                    Ladungsgewicht = 7,
                    Ladunganzahl = Ladungliste1.Count()
                };

                LKWs.Add(LKW2);

                DeleteLadungBtnClickCmd = new RelayCommand<Ladung>(DeleteLadung, true);

                DeleteAllBtnClickCmd = new RelayCommand(DeleteAllLadung,
                    () => {
                        return CheckLadung();
                    });
                SelectedLKW = LKW2;
            }
            else
            {
                LKWs = new ObservableCollection<LKW>();
                DisplayedLadung = new ObservableCollection<Ladung>();
                Ladungliste1 = new ObservableCollection<Ladung>();
                Ladung1 = new Ladung();
                Ladung2 = new Ladung();
                Produkte = new ObservableCollection<Produkt>();

                Produkte.Add(new Produkt() { Produktname = "Reis" });
                Produkte.Add(new Produkt() { Produktname = "Apfel" });

                Ladung1.Produkte = Produkte;
                Ladung2.Produkte = Produkte;

                Ladungliste1.Add(Ladung1);
                Ladungliste1.Add(Ladung2);

                LKW LKW1 = new LKW()
                {
                    ID = "F34234",
                    Ladungliste = Ladungliste1,
                    Ladungsgewicht = 7,
                    Ladunganzahl = Ladungliste1.Count()
                };

                LKWs.Add(LKW1);

                DeleteLadungBtnClickCmd = new RelayCommand<Ladung>(DeleteLadung, true);

                DeleteAllBtnClickCmd = new RelayCommand(DeleteAllLadung,
                    () => {
                        return CheckLadung();
                    });

                // Kommunikation:
                Server = new Server(GuiUpdater);

            }
        }

        public bool CheckLadung()
        {
            if (DisplayedLadung.Count > 0 && SelectedLKW != null)
            {
                return true;
            }
            else
                return false;
        }

        public void DisplayLadung(string id)
        {
            foreach (var item in LKWs)
            {
                if (item.ID.Equals(id))
                {
                    DisplayedLadung = item.Ladungliste;
                    break;
                }
            }
            //RaisePropertyChanged("DisplayedToys");
            ProduktanzahlBerechnen();
        }

        public void ProduktanzahlBerechnen()
        {
            if(SelectedLKW != null)
            {
                int i = 0;
                foreach (var item in SelectedLKW.Ladungliste)
                {
                    i += item.Produkte.Count;
                }
                Produktanzahl = i;
                //RaisePropertyChanged("Produktanzahl");
            }
        }

        public void DeleteLadung(Ladung delcon)
        {
            SelectedLKW.Ladungliste.Remove(delcon);
            DisplayLadung(SelectedLKW.ID);
            ProduktanzahlBerechnen();
        }

        public void DeleteAllLadung()
        {
            SelectedLKW.Ladungliste.Clear();
            DisplayLadung(SelectedLKW.ID);
            ProduktanzahlBerechnen();
        }

        private void GuiUpdater(string msg)
        {
            App.Current.Dispatcher.Invoke(
                 () =>
                 {
                     Console.WriteLine(msg);
                     string[] split = msg.Split('|');
                     /* split =
                      * [0] ID L345
                      * [1] Gewicht
                      * [2] Beladung
                     */
                     ObservableCollection<Ladung>  oldLadungsliste = new ObservableCollection<Ladung>();

                     foreach (var lkw in LKWs)
                     {
                         if (lkw.ID.Equals(split[0]))
                         {
                             oldLadungsliste = lkw.Ladungliste;
                             SelectedLKW = null;
                             LKWs.Remove(lkw);
                             break;
                         }
                     }

                     LKW newLKW = new LKW()
                     {
                         ID = split[0],
                         Ladungsgewicht = int.Parse(split[1]),
                         Ladungliste = oldLadungsliste
                     };
                     
                     ObservableCollection<Produkt> Produktsammlung = new ObservableCollection<Produkt>();

                     for (int i = 2; i < split.Length; i++)
                     {
                         Produktsammlung.Add(new Produkt() { Produktname = split[i] });
                     }

                     Ladung newLadung = new Ladung();
                     newLadung.Produkte = Produktsammlung;

                     newLKW.Ladungliste.Add(newLadung);

                     newLKW.Ladunganzahl = newLKW.Ladungliste.Count();
                     LKWs.Add(newLKW);

                     SelectedLKW = newLKW;
                 });
        }
    }
}