using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurFirstServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            //server.AcceptClient();
            //server.ReceiveData();
            //Console.WriteLine("close application");

            Console.WriteLine("application handles client requests");
            Console.ReadLine();

        }
    }
}
