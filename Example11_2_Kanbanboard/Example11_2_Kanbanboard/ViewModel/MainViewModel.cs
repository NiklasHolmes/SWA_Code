using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;

namespace Example11_2_Kanbanboard.ViewModel
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
        public RelayCommand<Card> DeleteCardBtnClickCmd { get; set; }
        public RelayCommand<Card> MoveCardBtnClickCmd { get; set; }

        public MainViewModel()
        {

            #region IsInDesignMode

            if (IsInDesignMode)
            {
                EstimateCards = new ObservableCollection<Card>();
                Card Testcard = new Card()
                {
                    Time = Time,
                    Person = Person,
                    Duration = TextBoxIntValue,
                    Urgent = CheckBoxBoolValue,
                    Column = SelectedComboBoxValue
                };
                EstimateCards.Add(Testcard);
                EstimateCards.Add(Testcard);
            }
            #endregion

            EstimateCards = new ObservableCollection<Card>();
            Card newcard1 = new Card()
            {
                Time = "12.03",
                Person = "Kunibert",
                Duration = 7,
                Urgent = false,
                Column = "Estimate"
            };
            EstimateCards.Add(newcard1);
            Card newcard2 = new Card()
            {Time = "12.03",Person = "Jamie",Duration = 90,Urgent = true, Column = "Estimate" };
            EstimateCards.Add(newcard2);

            //Tag = "TagBsp";
            Time = DateTime.Now.ToString("dd.MM");
            Person = "James Bond";
            TextBoxIntValue = 30;
            CheckBoxBoolValue = false;
            Cards = new ObservableCollection<Card>();

            //!EstimateCards = new ObservableCollection<Card>();
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
                    Card newCard = new Card()
                    {
                        Time = Time,
                        Person = Person,
                        Duration = TextBoxIntValue,
                        Urgent = CheckBoxBoolValue,
                        Column = SelectedComboBoxValue
                    };
                    Cards.Add(newCard);
                    Einordnen(newCard);
                },
                () => { return Time != " " && Person != " " && TextBoxIntValue > 0 && SelectedComboBoxValue != null; } // bei ComboBoxValue NULL!!
            );

            DeleteCardBtnClickCmd = new RelayCommand<Card>(DeleteCard, true);
            MoveCardBtnClickCmd = new RelayCommand<Card>(MoveCard, true);
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
                case "Deploy":
                    DeployCards.Add(newcard);
                    break;
                default:
                    EstimateCards.Add(newcard);
                    break;
            }
        }

        public void DeleteCard(Card cardtodelete)
        {
            // SWITCH:
            switch (cardtodelete.Column)
            {
                case "Estimate":
                    foreach (var item in EstimateCards)
                    {
                        if (item == cardtodelete)
                        {
                            EstimateCards.Remove(item);
                            break;
                        }
                    }
                    break;
                case "Testing":
                    foreach (var item in TestingCards)
                    {
                        if (item == cardtodelete)
                        {
                            TestingCards.Remove(item);
                            break;
                        }
                    }
                    break;
                case "Deploy":
                    foreach (var item in DeployCards)
                    {
                        if (item == cardtodelete)
                        {
                            DeployCards.Remove(item);
                            break;
                        }
                    }
                    break;
                default:
                    foreach (var item in Cards)
                    {
                        if (item == cardtodelete)
                        {
                            Cards.Remove(item);
                            break;
                        }
                    }
                    break;
            }
        }

        public void MoveCard(Card cardtodelete)
        {
            // SWITCH:
            switch (cardtodelete.Column)
            {
                case "Estimate":
                    foreach (var item in EstimateCards)
                    {
                        if (item == cardtodelete)
                        {
                            item.Column = "Testing";
                            TestingCards.Add(item);
                            EstimateCards.Remove(item);
                            break;
                        }
                    }
                    break;
                case "Testing":
                    foreach (var item in TestingCards)
                    {
                        if (item == cardtodelete)
                        {
                            item.Column = "Deploy";
                            DeployCards.Add(item);
                            TestingCards.Remove(item);
                            break;
                        }
                    }
                    break;
                case "Deploy":
                    foreach (var item in DeployCards)
                    {
                        if (item == cardtodelete)
                        {
                            item.Column = "Finish";
                            DeployCards.Remove(item);
                            break;
                        }
                    }
                    break;
                default:
                    foreach (var item in Cards)
                    {
                        if (item == cardtodelete)
                        {
                            Cards.Remove(item);
                            break;
                        }
                    }
                    break;
            }
        }

    }
    
}