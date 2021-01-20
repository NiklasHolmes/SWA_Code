using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlugzeugBsp_2019.ViewModel
{
    public class Flug : ViewModelBase
    {
        public string Flugnummer { get; set; }
        public int Containeranzahl { get; set; }
        public ObservableCollection<Container> Containerliste { get; set; }


        public Flug()
        {
            Flugnummer = "";
            Containerliste = new ObservableCollection<Container>();
            Containeranzahl = Containerliste.Count();
        }
    }
}
