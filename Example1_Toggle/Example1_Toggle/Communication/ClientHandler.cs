using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example1_Toggle.Communication
{
    public class ClientHandler
    {
        private Action<string, Socket> action;
        byte[] buffer = new byte[512];
        private Thread clientReceiveThread;
        // gleichzeitiges Ausführen (= Multitasking) (z.B. mehrere Clients können gleichzeitig an Server senden

        public Socket ClientSocket
        {
            get;
            private set;
        }


        public ClientHandler(Socket socket /*, Action<string, Socket> action*/)
        {
            this.ClientSocket = socket;
            //this.action = action;
            //start receiving in a new thread
            //clientReceiveThread = new Thread(ReceiveMessages);
            //clientReceiveThread.Start();
        }

        /*
        public void ReceiveMessages()
        {
            string newMessage = "";
            while (!newMessage.Contains("@quit") || true)
            {
                int length = ClientSocket.Receive(buffer);                  //schreib alle empfangenden Daten in buffer rein und gib Länge zurück
                newMessage = Encoding.ASCII.GetString(buffer, 0, length);   //fang bei 0 zu zählen an und gib nur zurück wieviele empfangen wurden

                //GUI durch Delegate informieren:
                action(newMessage, ClientSocket);
            }
        }
        */

        /*
        public void Send(string message)
        {
            ClientSocket.Send(Encoding.UTF8.GetBytes(message));
        }
        */
        

    }
}
