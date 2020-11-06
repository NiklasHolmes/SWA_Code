using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstConverterExample
{
    public class Character : ViewModelBase      // ist von MVVM Light
    {
        private int power, hitpoints;

        public string CharacterClass { get; set; }          //
        public string Name { get; set; }                    
        public int Mana { get; set; }                       
        public int Hitpoints        // Farbe => hitpoints: <50(red), >50<100(yellow), else green
        { 
            get { return hitpoints;  }
            set 
            {
                hitpoints = value;
                //RaisePropertyChanged(); // => leer => default => hitpoints würde nochmal nachgeschaut werden
                RaisePropertyChanged("IsAlive"); // => informiert die GUI dass auch IsAlive abgefragt werden soll
            }
        }
        public bool IsAlive
        {
            get {
                if (Hitpoints > 0) return true; 
                else return false;
            }
        }
        
        public int Power { 
            get { return power; }
            set
            {
                if (power > 0)
                {
                    power = power * value;  // überschreiben mit dem neuen Wert (multipliziert)
                }
                else power = value;     //sonst würde immer null dastehen

                //wenn die GUI ein set aufruft dann ruft sie danach auch nochmal ein get auf
                
            }
        }

    }
}


