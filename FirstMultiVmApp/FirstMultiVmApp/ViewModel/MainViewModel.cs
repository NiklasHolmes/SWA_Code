using CommonServiceLocator;
using FirstMultiVmApp.Navigators;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;

namespace FirstMultiVmApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase viewModelBase;

        public ViewModelBase CurrentDetail
        {
            get { return viewModelBase; }
            set { viewModelBase = value; RaisePropertyChanged(); }
        }

        private INavigator navigator = new Navigator();


        public RelayCommand<string> MenuBtnClickCmd { get; set; }           // using WPF!!


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //CurrentDetail = ServiceLocator.Current.GetInstance<MasterDataVm>();
            //CurrentDetail = ServiceLocator.Current.GetInstance<ReportVm>();

            //MenuBtnClickCmd = new RelayCommand<string>(ContentSwitcher);

            CurrentDetail = navigator.Navigate("masterdata");
            MenuBtnClickCmd = new RelayCommand<string>((p) => CurrentDetail = navigator.Navigate(p));
        }

        // anfangs:
        /*
        private void ContentSwitcher(string obj)
        {
            switch (obj)
            {
                case "masterdata":
                    CurrentDetail = ServiceLocator.Current.GetInstance<MasterDataVm>();
                    break;
                case "reports":
                    CurrentDetail = ServiceLocator.Current.GetInstance<ReportVm>();
                    break;
                case "dynamicdata":
                    CurrentDetail = ServiceLocator.Current.GetInstance<DynamicDataVm>();
                    break;
                default:
                    CurrentDetail = ServiceLocator.Current.GetInstance<MasterDataVm>();
                    break;

            }
        }
        */
    }
}