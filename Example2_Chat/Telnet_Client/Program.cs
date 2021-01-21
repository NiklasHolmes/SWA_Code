using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telnet_Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello there! Please enter the correct port number for a connection to the server: ");
            string entry = Console.ReadLine();
            
            int result;
            if (int.TryParse(entry, out result))
            {
                // Client eingeben => Add reference
                Client cl = new Client(result);

                Console.Write(cl.ReceiveData());
                string name = Console.ReadLine();

                while (true)
                {
                    cl.SendData(Console.ReadLine() + "\r\n", name);
                    Console.Write(cl.ReceiveData());        // selbst
                }
            }
            else { Console.WriteLine("We need a number!"); }
        }
    }
}
