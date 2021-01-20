using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static Random rn = new Random();
        public static Client Cl { get; set; }

        static void Main(string[] args)
        {
            Cl = new Client();

            string[] products = new string[] { "{Bananen,Autoreifen,}", "{XBox,Playstation,Wii}", "{Server,Monitor,Drucker}", "{Autoreifen,,}" };

            while (true)
            {
                string flug = "F947" + rn.Next(0, 9) + ":";
                int containerCount = rn.Next(0, 3);

                for (int i = 0; i <= containerCount; i++)
                {
                    flug += products[rn.Next(0, 4)];
                    if (i != containerCount)
                    {
                        flug += ";";
                    }
                }

                Console.WriteLine(flug);

                Cl.SendData(Encoding.UTF8.GetBytes(flug));

                Thread.Sleep(5000);
            }

            while (true)
            {
                string newFlight = "F3454" + ":{" + "Apfel,Banane" + "};{" + "XBox" + "};";

                Cl.SendData(Encoding.UTF8.GetBytes(newFlight));      // Enter anhängen damit es funktioniert!!
            }
        }
    }
}
