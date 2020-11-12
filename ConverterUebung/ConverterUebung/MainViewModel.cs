using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterUebung
{
    public class MainViewModel : ViewModelBase      // WICHTIG!
    {

        public bool State
        {
            get { return state; }
            set { state = value; RaisePropertyChanged(); }
        }

        private bool state;
        public RelayCommand InvertBtnClickedCmd { get; set; }

        public MainViewModel()
        {
            InvertBtnClickedCmd = new RelayCommand(() =>
            {
                State = !State;
            });
        }

    }
}
