using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_wi19b040.ViewModel
{
    public class Item : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //public int Value { get; set; }

        private string state;
        public string State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    INotifyPropertyChanged("State");
                }
            }
        }

        private int valueItem;
        public int Value
        {
            get { return valueItem; }
            set
            {
                if (valueItem != value)
                {
                    valueItem = value;
                    INotifyPropertyChanged("Value");
                }
            }
        }

        protected void INotifyPropertyChanged(string state)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(state));
            }
        }

        public Item()
        {
            Name = "";
            Value = 0;
            State = "";
            ID = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
