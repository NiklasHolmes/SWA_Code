using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo1.Stack
{
    class Stack<T>
    {
        private TStack<T> _currentStack;    //aktuelles StackElement
        
        /*
        static void Main(string[] args)
        {
            TStack<int> Stapel = new TStack<int>();
            //Stapel.Push(677);
        }
        */
        public void Push(T data)
        {
            //falls Stack noch ganz leer ist:
            if (_currentStack == null)
            {
                _currentStack = new TStack<T>();
                _currentStack.ValueOfElement = data;
                _currentStack.Successor = null;     //Nachfolger = null
            }
            else
            {
                TStack<T> _newStack = new TStack<T>() { ValueOfElement = data,  Successor = _currentStack}; //smarte Art
                _currentStack = _newStack;

            }
        }

        public T Pop()
        {
            if (_currentStack == null)
            {
                // throw Exception
                //throw new Exception("Stack ist leer!");
                //oder:
                throw new NullReferenceException();
            }
            else
            {
                T popData = _currentStack.ValueOfElement;
                //var _newStack = new TStack<T>() { ValueOfElement = _currentStack.Successor.ValueOfElement, Successor = _currentStack.Successor.Successor };
                // Fehlerquelle (weil zweites Successor vergessen => blieb immer bei einer Variable hängen
                
                var _newStack = new TStack<T>();

                if (_currentStack.Successor == null)
                {
                    //_newStack.ValueOfElement = default(T);
                    // hier muss kein Wert / Vorgänger festgelegt werden da _newStack ja bereits mit default-Werten angelegt ist
                }
                else {
                    _newStack.ValueOfElement = _currentStack.Successor.ValueOfElement;
                    _newStack.Successor = _currentStack.Successor.Successor;
                }
                _currentStack = _newStack;
                // 2. Fehlerquelle: null Werte beim letzten Element problematisch

                // viele schöner und kürzer:
                //_currentStack = _currentStack.Successor;   //   Zeiger neu ausrichten
                return popData;
            }
        }

        public T Peek()
        {
            //falls Stack leer:
            if (_currentStack == null)
            {
                return default(T);  //default Wert von T zurückgeben
            } else
            {
                return _currentStack.ValueOfElement;
            }
        }


    }
}
