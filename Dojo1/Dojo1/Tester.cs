﻿using Dojo1.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo1
{
    class Tester
    {
        static void Main(string[] args)
        {
            //TStack<int> Stapel = new TStack<int>();
            //Stapel.Push(677);

            /*Console.WriteLine();
            Console.WriteLine("Press return to close the application.");
            Console.ReadLine();
            */

            Dojo1.Stack.Stack<int> test = new Dojo1.Stack.Stack<int>();

            test.Push(1);
            test.Push(2);
            test.Push(3);
            Console.WriteLine("Peek-read: '{0}'", test.Peek());      //{0} == Placeholder für Variable
            int intTemp = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Du hast {0} eingegeben", intTemp);
            test.Push(intTemp);

            Console.WriteLine("{0} removed", test.Pop());
            Console.WriteLine("{0} removed", test.Pop());
            Console.WriteLine("Peek-read: '{0}'", test.Peek());
            Console.WriteLine("{0} removed", test.Pop());
            Console.ReadLine();

            /*
            Dojo1.Stack.Stack<string> test2 = new Dojo1.Stack.Stack<string>();

            test2.Push("E1");
            test2.Push("E2");
            Console.WriteLine("read: '{0}'", test2.Peek());
            test2.Push("E3");
            Console.WriteLine("hinzugefügt: '{0}'", test2.Peek());
            Console.WriteLine("{0} removed", test2.Pop());
            Console.WriteLine("{0} removed", test2.Pop());
            Console.WriteLine("read: '{0}'", test2.Peek());
            Console.WriteLine("{0} removed", test2.Pop());
            //Console.WriteLine("read: '{0}'", test2.Peek());
            //Console.WriteLine("{0} removed", test2.Pop());
            Console.ReadLine();
            */
        }
    }
}
