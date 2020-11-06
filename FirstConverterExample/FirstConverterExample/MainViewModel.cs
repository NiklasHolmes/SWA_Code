using FirstConverterExample.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstConverterExample
{
    public class MainViewModel
    {
        public ObservableCollection<Character> Characters { get; set; }
        public List<string> CharacterClasses { get; set; }
        public List<int> PowerSelection { get; set; }

        public MainViewModel()      //ctor tab tab
        {
            CharacterClasses = new List<string> { "Wizard", "Knight", "Warrier", "Rogue" };
            PowerSelection = new List<int>() { 2, 4, 6 };
            
            Characters = new ObservableCollection<Character>();
            LoadDemoData();
        }

        private void LoadDemoData()
        {
            Characters.Add(new Character()
            {
                Name = "Gandalf",
                Hitpoints = 1000,
                Mana = 5000,
                //IsAlive = true,
                Power = 500,
                CharacterClass = "Wizard"
            });
            Characters.Add(new Character()
            {
                Name = "Saroman",
                Hitpoints = 90,
                Mana = 7000,
                //IsAlive = true,
                Power = 450,
                CharacterClass = "Wizard"
            });
            Characters.Add(new Character()
            {
                Name = "Boromir",
                Hitpoints = 0,
                Mana = 0,
                Power = 600,
                CharacterClass = "Knight"
            });
            //Characters.Last().IsAlive = IsAliveCheck(Characters.Last().Hitpoints);
        }
        /*
        private bool IsAliveCheck(int hit)
        {
            return false;
        }
        */
    }
}
