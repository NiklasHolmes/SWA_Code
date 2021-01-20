using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;

namespace Example11_KanbanBoard.ViewModel
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
        private string selectedComboBoxValue;

        public ObservableCollection<string> ComboBoxList { get; set; }
        public ObservableCollection<Card> Cards { get; set; }
        public ObservableCollection<Card> EstimateCards { get; set; }
        public ObservableCollection<Card> TestingCards { get; set; }
        public ObservableCollection<Card> DeployCards { get; set; }

        // XAML: 

        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; RaisePropertyChanged(); }
        }
        private string person;

        public string Person
        {
            get { return person; }
            set { person = value; RaisePropertyChanged(); }
        }
        private int textBoxInValue;

        public int TextBoxIntValue
        {
            get { return textBoxInValue; }
            set { textBoxInValue = value; RaisePropertyChanged(); }
        }
        private bool checkBoxBoolValue;

        public bool CheckBoxBoolValue
        {
            get { return checkBoxBoolValue; }
            set { checkBoxBoolValue = value; }
        }

        public string SelectedComboBoxValue
        {
            get { return selectedComboBoxValue; }
            set { selectedComboBoxValue = value; }
        }

        public RelayCommand AddBtnClickCmd { get; set; }

        public MainViewModel()
        {
            #region IsInDesignMode
            if (IsInDesignMode) 
            {
                DeployCards = new ObservableCollection<Card>();
                Card newcard = new Card()
                {
                    Time = Time,
                    Person = Person,
                    Duration = TextBoxIntValue,
                    Urgent = CheckBoxBoolValue,
                    Column = SelectedComboBoxValue
                };
                EstimateCards.Add(newcard);

            }
            #endregion

            //Tag = "TagBsp";
            Time = DateTime.Now.ToString("dd.MM");
            Person = "James Bond";
            TextBoxIntValue = 30;
            CheckBoxBoolValue = false;
            Cards = new ObservableCollection<Card>();

            EstimateCards = new ObservableCollection<Card>();
            TestingCards = new ObservableCollection<Card>();
            DeployCards = new ObservableCollection<Card>();

            ComboBoxList = new ObservableCollection<string>()
                {
                  "IQ",
                  "Estimate",
                  "Implement",
                  "Testing",
                  "Deploy"
                };

            AddBtnClickCmd = new RelayCommand(
                () =>
                {
                    // Adden und erstellen in einem:
                    /*Cards.Add(new Card()
                    {
                        Time = Time,
                        Person = Person,
                        Duration = TextBoxIntValue,
                        Urgent = CheckBoxBoolValue
                    });*/
                    Card newcard = new Card()
                    {
                        Time = Time,
                        Person = Person,
                        Duration = TextBoxIntValue,
                        Urgent = CheckBoxBoolValue,
                        Column = SelectedComboBoxValue
                    };
                    Cards.Add(newcard);
                    Einordnen(newcard);

                }, 
                () => { return Time != " " && Person != " " && TextBoxIntValue > 0; }
            );
        }

        public void Einordnen(Card newcard)
        {
            // SWITCH: 

            switch (newcard.Column)
            {
                case "Estimate":
                    EstimateCards.Add(newcard);
                    break;
                case "Testing":
                    TestingCards.Add(newcard);
                    break;
                case "´Deploy":
                    DeployCards.Add(newcard);
                    break;
                default:
                    EstimateCards.Add(newcard);
                    break;
            }
        }
    }
}