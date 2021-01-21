using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example2_Chat.Communication
{
    class Server
    {
        Action<string> GuiUpdater;
        Socket serverSocket;
        private const int port = 10100;
        private const string ip = "127.0.0.1";

        byte[] buffer = new byte[256];
        List<ClientHandler> clients = new List<ClientHandler>();

        Thread acceptingThread;

        public Server(Action<string> guiUpdater)
        {
            GuiUpdater = guiUpdater;

            serverSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(10);
            StartAcceptingClientThread();

            //  generate a new Thread (Background Thread - wenn Haupt geschlossen auch das)
            //Task.Factory.StartNew(AcceptClient);
        }

        public void StartAcceptingClientThread()
        {
            acceptingThread = new Thread(new ThreadStart(AcceptClients));
            acceptingThread.IsBackground = true;
            acceptingThread.Start();
        }

        private void AcceptClients()
        {
            while (acceptingThread.IsAlive)
            {
                try
                {
                    clients.Add(new ClientHandler(serverSocket.Accept(), new Action<string, Socket>(NewMessagesReceived)));
                }
                catch (Exception e) { }
            }
        }

        private void NewMessagesReceived(string message, Socket senderSocket)
        {
            GuiUpdater(message);    // (siehe auch UpdateGuiWithNewMessage in MainViewModel)

            foreach (var item in clients)
            {
                if (item.ClientSocket != senderSocket)
                {
                    item.SendData(message);
                }
            }
        }


    }
}
