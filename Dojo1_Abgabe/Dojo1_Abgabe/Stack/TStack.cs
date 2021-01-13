using System;

namespace Dojo1_Abgabe.Stack
{
    class TStack<T>
    {
        public T ValueOfElement { get; set; } //lagert Wert des Elements
        public TStack<T> Successor { get; set; } //zeigt auf den Eintrag darunter bzw. davor
    }
}

