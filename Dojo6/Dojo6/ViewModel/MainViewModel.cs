using Dojo6.Navigators;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Dojo6.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        // DOJO 6 !!!
        public RelayCommand<string> MenuBtnClickCmd { get; set; }

        private ViewModelBase viewModelBase;

        public ViewModelBase CurrentDetail
        {
            get { return viewModelBase; }
            set { viewModelBase = value; RaisePropertyChanged(); }
        }
        private INavigator navigator = new Navigator();

        private string statusImage;

        public string StatusImage
        {
            get { return statusImage; }
            set { statusImage = value; RaisePropertyChanged(); }
        }
        private string statusVisible;

        public string StatusVisible
        {
            get { return statusVisible; }
            set { statusVisible = value; RaisePropertyChanged();  }
        }
        private string statusText;

        public string StatusText
        {
            get { return statusText; }
            set { statusText = value; RaisePropertyChanged(); }
        }
        private DispatcherTimer timer;

        public MainViewModel()
        {
            StatusVisible = "Hidden";

            CurrentDetail = navigator.Navigate("overview");
            MenuBtnClickCmd = new RelayCommand<string>((p) => CurrentDetail = navigator.Navigate(p));

            Messenger.Default.Register<ToyVm>(this, "AddToCart", AddWasClicked);

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += Timer_Tick;

        }

        public void AddWasClicked(ToyVm toyToCart)
        {
            // alles gut verlaufen? Dann Info ausgeben:
            StatusVisible = "Visible";
            StatusImage = "Info";
            StatusText = "New Entry Added";
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            StatusVisible = "Hidden";
            StatusImage = "";
            StatusText = "";
            timer.Stop();
        }

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
    }
}