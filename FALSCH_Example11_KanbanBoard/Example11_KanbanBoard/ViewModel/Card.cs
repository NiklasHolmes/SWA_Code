using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example11_KanbanBoard.ViewModel
{
    public class Card : ViewModelBase
    {
        public string Time;
        public string Person;
        public int Duration;
        public bool Urgent;
        public string Column;

    }
}
