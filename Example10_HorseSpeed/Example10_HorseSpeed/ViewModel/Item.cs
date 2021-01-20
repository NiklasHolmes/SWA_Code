using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example10_HorseSpeed.ViewModel
{
    public class Item : ViewModelBase
    {
        public int Speed { get; set; }
        public string Name { get; set; }

        public Item()
        {
            Speed = 0;
            Name = "";
        }


    }
}
