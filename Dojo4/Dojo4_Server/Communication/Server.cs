using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dojo4_Server.Communication
{
    class Server
    {
        Action<string> GuiUpdater;
        Thread acceptingThread;     //für die Akzeptierung von neuen Clients

        Socket serverSocket;

        byte[] buffer = new byte[256];
        List<ClientHandler> Clients = new List<ClientHandler>();

        public Server(string ip, int port, Action<string> guiUpdater)
        {
            GuiUpdater = guiUpdater;
            serverSocket = new Socket(
                AddressFamily.InterNetwork,     // welcher Internettyp
                SocketType.Stream,              // wie Pakete übermittelt werden
                ProtocolType.Tcp                // welches Protokoll
            );

            // socket auf Port binden: 
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(5);     //=> wie viele Clients in die Liste kommen können die gerade anfragen / auf Bestätigung warten
        }

        public void StartAcceptingClients() {

            acceptingThread = new Thread(new ThreadStart(AcceptClients));   //damit mehrere Clients gleichzeitig aufgenommen werden können
            acceptingThread.IsBackground = true;                            // auf Background setzen => wenn HauptThread geschlossen dann auch das
            acceptingThread.Start();
        }

        private void AcceptClients()
        {
            while (acceptingThread.IsAlive)     // while Schleife damit es parallel ablaufen kann
            {
                try
                {
                    // Blocking! Der Server bleibt solange stehen, bis etwas reinkommt
                    Clients.Add(new ClientHandler(serverSocket.Accept(), new Action<string, Socket>(NewMessagesReceived)));
                }
                catch (Exception e)
                {
                    //executed if serversocket.close is called
                }
            }
        }

        public void StopConnection()
        {
            serverSocket.Close();       //schließen
            acceptingThread.Abort();    //accepting thread abbrechen

            //alle Client Threads und Connections schließen
            foreach (var item in Clients)
            {
                item.CloseConn();
            }
            //alle Einträge aus Client-Liste löschen
            Clients.Clear();
        }

        public void DisconnectOneClient(string clientName)
        {
            // alle Clients durchgehen um bestimmen zu finden
            foreach (var item in Clients)
            {
                if (item.Name.Equals(clientName)) {
                    item.CloseConn();
                    Clients.Remove(item);       //Client aus Clientliste geben
                    break;                      // break damit nicht noch wer rausgehaut wird (Namensgleichheiten-Problem!)
                }
            }
        }

        private void NewMessagesReceived(string message, Socket senderSocket)
        {
            //GUI updaten
            GuiUpdater(message);    // (siehe auch UpdateGuiWithNewMessage in MainViewModel)

            //Nachricht an alle Clients schicken
            foreach (var item in Clients)
            {
                if (item.ClientSocket != senderSocket)
                {
                    item.SendMessage(message);
                }
            }
        }
    }
}
