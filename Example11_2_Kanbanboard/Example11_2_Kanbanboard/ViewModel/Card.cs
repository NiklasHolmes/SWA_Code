using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example11_2_Kanbanboard.ViewModel
{
    public class Card : ViewModelBase
    {
        public string Time { get; set; }
        public string Person { get; set; }
        public int Duration { get; set; }
        public bool Urgent { get; set; }
        public string Column { get; set; }

        public Card()
        {
            Time = "";
            Person = "";
            Duration = 0;
            Urgent = false;
            Column = "";
        }
    }
}
