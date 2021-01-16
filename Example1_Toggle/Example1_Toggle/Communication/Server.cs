using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example1_Toggle.Communication
{
    public class Server
    {


        Socket serverSocket;            // using NetSockets
        public List<ClientHandler> clients = new List<ClientHandler>();
        Action GuiUpdater;
        Thread acceptingThread;     // using Threading      //handles the accepting of new clients

        //private Server server;

        public Server(string ip, int port, Action GuiUpdater)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(5);         // wie viele Anfragen auf einmal bearbeiten
            this.GuiUpdater = GuiUpdater;

            Task.Factory.StartNew(StartAcceptingClients);
        }
        public void StartAcceptingClients()
        {
            acceptingThread = new Thread(new ThreadStart(AcceptClients));   //
            acceptingThread.IsBackground = true;
            acceptingThread.Start();
        }

        private void AcceptClients()
        {
            while (acceptingThread.IsAlive)     // while Schleife damit es parallel ablaufen kann
            {
                try     // Probieren ob Client existiert
                {
                    // Blocking! Der Server bleibt solange stehen, bis etwas reinkommt
                    clients.Add(new ClientHandler(serverSocket.Accept() /*, new Action<string, Socket>(NewMessageReceived)*/ ));
                    GuiUpdater();
                }
                catch (Exception e)
                {
                    //executed if serversocket.close is called
                }
            }
        }

        /*
        private void NewMessageReceived(string message, Socket senderSocket)
        {
            //GUI updaten
            GuiUpdater(message);    // (siehe auch UpdateGuiWithNewMessage in MainViewModel)

            //Nachricht an alle Clients schicken
            foreach (var item in clients)
            {
                if (item.ClientSocket != senderSocket)
                {
                    item.Send(message);
                }
            }
        }
        */
        
        
        //SELBST: 
        public void SendMessage(string message)
        {
            //ClientSocket.Send(Encoding.UTF8.GetBytes(message));
            foreach (var item in clients)
            {
                item.ClientSocket.Send(Encoding.UTF8.GetBytes(message));
            }
        }
    }
}
