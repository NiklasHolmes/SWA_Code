using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OurFirstServer
{
    class Server
    {
        Socket socket, clientSocket;          //einfach using
        // Server hat immer einen Socket + so viele wie er Verbindungen hat

        byte[] buffer = new byte[256];
        List<ClientHandler> clients = new List<ClientHandler>();

        public Server()
        {
            socket = new Socket(
                AddressFamily.InterNetwork,     // welcher Internettyp
                SocketType.Stream,              // wie Pakete übermittelt werden
                ProtocolType.Tcp                // welches Protokoll
                );

            // socket auf Port binden: 
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10100));

            //sagen was er tun soll => horchen ob jmd etwas sendet
            socket.Listen(10);              //=> wie viele Clients in die Liste kommen können die gerade anfragen / auf Bestätigung warten

            Console.WriteLine("Listening Mode...");
            // generate a new Thread (Background Thread - wenn Haupt geschlossen auch das)
            Task.Factory.StartNew(AcceptClient);

            // stattdessen geht auch (für Threads):
            //ThreadPool.
            // hier gibt es mehr Möglichkeiten etwas einzustellen (dafür sind Pool und Factory schneller):
            //Thread th = new Thread(new ThreadStart("sdfs"));  

        }

        // Verbindungsnafrage annehmen:
        private void AcceptClient()             //vorher wars auf public
        {
            /* Alt:
            Console.WriteLine("Ready to accept a Client");
            clientSocket = socket.Accept();     // Blocking! Der Server bleibt solange stehen, bis etwas reinkommt
            Console.WriteLine("Client accepted");

            clientSocket.Send(Encoding.ASCII.GetBytes("Hello Client! Ready to receive data..."));       // ACII => damit String umgewandelt wird
            */

            while (true)                 // while Schleife damit es parallel ablaufen kann
            {
                Console.WriteLine("Ready to accept a Client");

                clients.Add(new ClientHandler(socket.Accept()));     // Blocking! Der Server bleibt solange stehen, bis etwas reinkommt
                Console.WriteLine("Client accepted");

            }

        }

        /*public void ReceiveData()
        {
            string newdata = "";
            while (true)
            {
                int length = clientSocket.Receive(buffer);           //schreib alle empfangenden Daten in buffer rein und gib Länge zurück
                //string newdata = Encoding.ASCII.GetString(buffer, 0, length);       //fang bei 0 zu zählen an und gib nur zurück wieviele empfangen wurden
                //Console.Write(newdata);

                // Neu, besser:
                newdata += Encoding.ASCII.GetString(buffer, 0, length);

                if(newdata.Contains("\r\n"))                        // erst wenn ich Enter drücke wird es angezeigt
                {
                    Console.Write(newdata);
                    newdata = "";
                }
            }
        }*/
    }
}
