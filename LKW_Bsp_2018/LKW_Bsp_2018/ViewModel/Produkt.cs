using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKW_Bsp_2018.ViewModel
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
