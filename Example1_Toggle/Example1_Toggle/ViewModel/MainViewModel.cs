using Example1_Toggle.Communication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Example1_Toggle.ViewModel
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
        private Server server;
        private const int port = 10100;
        private const string ip = "127.0.0.1";

        public RelayCommand ConnectBtnClickCmd { get; set; }
        public RelayCommand ListBtnClickCmd { get; set; }


        public MainViewModel()
        {


            

        }
    }
}