using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace FarbconverterTestVorbereitung.ViewModel
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

        private bool myBoolValue;

        public bool MyBoolValue
        {
            get { return myBoolValue; }
            set { myBoolValue = value; RaisePropertyChanged(); }
        }

        private int myIntValue;

        public int MyIntValue
        {
            get { return myIntValue; }
            set { myIntValue = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<string> ColList { get; set; }

        private string myStringValue;

        public string MyStringValue
        {
            get { return myStringValue; }
            set { myStringValue = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            MyBoolValue = false;
            MyIntValue = 30;
            MyStringValue = "Blue";

            ColList = new ObservableCollection<string>()
                {
                  "Blue",
                  "Red",
                  "Green",
                  "Brown",
                  "White "
                };
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
}