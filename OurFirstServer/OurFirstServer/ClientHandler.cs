using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OurFirstServer
{
    public class ClientHandler
    {
        Socket clientSocket;
        byte[] buffer = new byte[256];

        public ClientHandler(Socket cs)
        {
            clientSocket = cs;
            clientSocket.Send(Encoding.ASCII.GetBytes("Hello Client! Ready to receive data..."));       // ACII => damit String umgewandelt wird
            Task.Factory.StartNew(ReceiveData);         // generate a new Thread for Receiving data from specific client
        }

        public void ReceiveData()
        {
            string newdata = "";
            while (true)
            {
                int length = clientSocket.Receive(buffer);           //schreib alle empfangenden Daten in buffer rein und gib Länge zurück
                //string newdata = Encoding.ASCII.GetString(buffer, 0, length);       //fang bei 0 zu zählen an und gib nur zurück wieviele empfangen wurden
                //Console.Write(newdata);

                // Neu, besser:
                newdata += Encoding.ASCII.GetString(buffer, 0, length);

                if (newdata.Contains("\r\n"))                        // erst wenn ich Enter drücke wird es angezeigt
                {
                    Console.Write(newdata);
                    newdata = "";

                }

            }
        }
    }
}
