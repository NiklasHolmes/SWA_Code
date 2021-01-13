namespace Dojo1.Stack
{
    class TStack<T>
    {
        public T ValueOfElement { get; set; } //lagert Wert des Elements
        public TStack<T> Successor { get; set; } //zeigt auf den Eintrag darunter bzw. davor
    }
}