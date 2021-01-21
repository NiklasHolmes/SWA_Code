using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LKW_Bsp_2018.Communication
{
    public class Server
    {
        Action<string> GuiUpdaterAction;
        Socket serverSocket;
        List<ClientHandler> clients = new List<ClientHandler>();

        int port = 10100;

        Thread acceptingThread;

        public Server(Action<string> guiUpdater)
        {
            GuiUpdaterAction = guiUpdater;

            serverSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
            serverSocket.Listen(5);

            //StartAcceptingClients ist im MainViewModel.cs
            Task.Factory.StartNew(AcceptClients);
        }

        /*
        // Verbindungsanfrage annehmen:
        public void StartAcceptingClients()
        {
            acceptingThread = new Thread(new ThreadStart(AcceptClients));
            acceptingThread.IsBackground = true;                  
            acceptingThread.Start();
        }
        */

        private void AcceptClients()
        {
            while (true)
            {
                clients.Add(new ClientHandler(serverSocket.Accept(), GuiUpdaterAction));
            }
            /*
            while (acceptingThread.IsAlive)
            {
                try
                {
                    clients.Add(new ClientHandler(serverSocket.Accept(), GuiUpdaterAction));
                }
                catch (Exception e)
                {
                }
            }
            */


        }
    }
}
