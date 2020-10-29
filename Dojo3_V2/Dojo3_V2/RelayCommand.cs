using System;
using System.Windows.Input;

namespace Dojo3_V2
{
    public class RelayCommand : ICommand       // von System Input   // public wichtig!!
    {
        private Action execute;             // delegate => ganz einfach, gibt nur weiter
        private Func<bool> canExecute;      // 

        //public event EventHandler CanExecuteChanged;      // explicit hinzugefügt

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;         // this verweist auf die Objektvariable
            this.canExecute = canExecute;         // Wert null bzw. false       // damit Button ausgegraut ist, wenn nichts ausgewählt wurde

        }

        event EventHandler ICommand.CanExecuteChanged       // diese Klasse braucht man einmal und verwendet sie immer wieder
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;   //  dann wieder weggeben
            }
        }

        public bool CanExecute(object parameter)
        {
            //throw new NotImplementedException();
            return canExecute();

        }

        public void Execute(object parameter)
        {
            //throw new NotImplementedException();    // 
            execute();
        }
    }
}