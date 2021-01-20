using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlugzeugBsp_2019.ViewModel
{
    public class Produkt : ViewModelBase
    {
        public string Produktname { get; set; }

        public Produkt()
        {
            Produktname = "";

        }
    }
}
