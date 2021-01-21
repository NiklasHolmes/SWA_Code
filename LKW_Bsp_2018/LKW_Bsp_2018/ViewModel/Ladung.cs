using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKW_Bsp_2018.ViewModel
{
    public class Ladung : ViewModelBase
    {
        public ObservableCollection<Produkt> Produkte { get; set; }

        public Ladung()
        {
            Produkte = new ObservableCollection<Produkt>();
        }
    }
}
