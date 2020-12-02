using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dojo4_Server.Communication
{
    class ClientHandler
    {
        private Action<string, Socket> action;
        byte[] buffer = new byte[512];
        private Thread clientReceiveThread;
        // gleichzeitiges Ausführen (= Multitasking) (z.B. mehrere Clients können gleichzeitig an Server senden)
        public string Name { get; private set; }

        public Socket ClientSocket
        {
            get;
            private set;
        }

        public ClientHandler(Socket cs, Action<string, Socket> action)
        {
            this.ClientSocket = cs;
            this.action = action;
            clientReceiveThread = new Thread(ReceiveMessages);
            clientReceiveThread.Start();
        }

        public void ReceiveMessages()
        {
            string newMessage = "";
            while (!newMessage.Contains("@quit"))
            {
                int length = ClientSocket.Receive(buffer);                  //schreib alle empfangenden Daten in buffer rein und gib Länge zurück
                newMessage = Encoding.ASCII.GetString(buffer, 0, length);   //fang bei 0 zu zählen an und gib nur zurück wieviele empfangen wurden

                // Name vor Nachricht setzen (falls nicht schon passiert):
                if (Name == null && newMessage.Contains(":"))
                {
                    Name = newMessage.Split(':')[0];
                }
                //GUI durch Delegate informieren:
                action(newMessage, ClientSocket);
            }

            CloseConn();
        }

        public void SendMessage(string message)
        {
            ClientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        public void CloseConn()
        {
            SendMessage("@quit");           //an Client @quit senden
            ClientSocket.Close(1);          //disconnects       (Zahl für Timeout)
            clientReceiveThread.Abort();    //abort client threads (client threads abbrechen)
        }
    }
}
