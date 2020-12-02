using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Dojo4_Client.ViewModel
{
    class Client
    {
        byte[] buffer = new byte[512];
        Socket clientSocket;
        Action<string> MessageInformer;
        Action AbortInformer;

        public Client(string ip, int port, Action<string> messageInformer, Action abortInformer)
        {
            try
            {
                this.MessageInformer = messageInformer;     // damit Nachrichten reinkommen (=> siehe NewMessagesReceived - MainViewModel)
                this.AbortInformer = abortInformer;         // falls Connection abgebrochen (=> siehe ClientDisconnected - MainViewModel)
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                clientSocket = client.Client;
                StartReceivingMessages();
            }
            catch (Exception)
            {
                messageInformer("Server not ready");        // schreib diese Nachricht wenn Server nicht nicht verbindungsfähig
                AbortInformer(); //reset Client Communication
            }
        }

        public void StartReceivingMessages()
        {
            //Start receiving in new Thread (and generate a new Thread)
            Task.Factory.StartNew(ReceiveMessage);
        }

        public void SendMessage(string message)
        {
            if (clientSocket != null) //prüfen ob clientSocket connected ist
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(message));
            }
        }

        public void ReceiveMessage()
        {
            string messages = "";

            while (!messages.Contains("@quit"))                   // solange kein @quit kommt 
            {
                int length = clientSocket.Receive(buffer);                  //schreib alle empfangenden Daten in buffer rein und gib Länge zurück
                messages = Encoding.ASCII.GetString(buffer, 0, length);     //fang bei 0 zu zählen an und gib nur zurück wieviele empfangen wurden

                //inform GUI via delegate (delegates => Verweis auf Methode)
                MessageInformer(messages);
            }
            CloseConnection();
        }

        private void CloseConnection()
        {
            clientSocket.Close();   // Verbindung schließen
            AbortInformer();        // Informer abbrechen
        }
    }
}
