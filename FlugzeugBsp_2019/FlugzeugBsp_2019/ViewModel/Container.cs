using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlugzeugBsp_2019.ViewModel
{
    public class Container : ViewModelBase
    {
        public ObservableCollection<Produkt> Produkte { get; set; }

        public Container()
        {
            Produkte = new ObservableCollection<Produkt>();
        }
    }
}
