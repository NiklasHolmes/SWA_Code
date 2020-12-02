﻿using SimpleChatClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello please enter port number for a connection to the server: ");
            string entry = Console.ReadLine();
            int result;
            if (int.TryParse(entry, out result))     // wenn parsen funktioniert => weitermachen
            {
                // Client eingeben => Add reference
                Client cl = new Client(result);
                // TODO: Add Receive Logic to receive name request
                Console.Write(cl.ReceiveData());

                while (true)
                {
                    cl.SendData(Console.ReadLine() + "\r\n" );      // Enter anhängen damit es funktioniert!!
                }
                
            }
            else { Console.WriteLine("Only numbers are allowed!"); }
        }
    }
}
