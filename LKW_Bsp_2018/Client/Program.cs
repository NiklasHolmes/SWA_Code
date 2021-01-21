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

            string[] ladungen = new string[] { "Video|CD|Fernseher", "CD|Radio", "Video", "Tee|Kaffee", "Zucker|Salz", "Salz", "Erde|Wasser|Stein", "Reis" };

            while (true)
            {
                string lkwstring = "";
                Console.Write("ID eingeben:");
                string id = Console.ReadLine();

                Console.Write("Gewicht eingeben:");
                int gewicht = int.Parse(Console.ReadLine());

                int ladungCount = rn.Next(0, 3);

                lkwstring += id + "|" + gewicht + "|" + ladungen[rn.Next(0, 8)];

                Console.WriteLine("Gesendet: " + lkwstring);

                Cl.SendData(Encoding.UTF8.GetBytes(lkwstring));

                Thread.Sleep(5000);
            }
        }
    }
}
