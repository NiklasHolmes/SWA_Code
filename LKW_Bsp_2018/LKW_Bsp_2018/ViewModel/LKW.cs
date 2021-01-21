using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKW_Bsp_2018.ViewModel
{
    public class LKW : ViewModelBase
    {
        public string ID { get; set; }
        public int Ladungsgewicht { get; set; }
        public int Ladunganzahl { get; set; }
        public ObservableCollection<Ladung> Ladungliste { get; set; }


        public LKW()
        {
            ID = "";
            Ladungliste = new ObservableCollection<Ladung>();
            Ladungsgewicht = 0;
            Ladunganzahl = Ladungliste.Count();
        }
    }
}
