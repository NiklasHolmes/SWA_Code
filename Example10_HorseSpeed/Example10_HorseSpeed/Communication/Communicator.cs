using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example10_HorseSpeed.Communication
{
    public class Communicator : ViewModelBase
    {
        Socket serverSocket;
        Socket clientSocket;
        public int port = 10100;
        byte[] buffer = new byte[512];
        public Action<string> GuiUpdater;
        Thread acceptingThread;
        public List<ClientHandler> clients;

        public Communicator(bool isServer, Action<string> guiUpdater)
        {
            this.GuiUpdater = guiUpdater;

            if (isServer)
            {
                clients = new List<ClientHandler>();
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
                serverSocket.Listen(5);
                Task.Factory.StartNew(StartAccepting);
            }
            else
            {
                this.GuiUpdater = guiUpdater;
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Loopback, port));
                clientSocket = client.Client;
                Task.Factory.StartNew(Receive);
            }
        }

        public void StartAccepting()
        {
            acceptingThread = new Thread(new ThreadStart(Accept));
            acceptingThread.IsBackground = true;
            acceptingThread.Start();
        }

        private void Accept()
        {
            while (acceptingThread.IsAlive)
            {
                try
                {
                    clients.Add(new ClientHandler(serverSocket.Accept(), new Action<string, Socket>(NewItemReceived)));
                    GuiUpdater("newclient");
                }
                catch (Exception e) { }
            }
        }

        public void NewItemReceived(string data, Socket senderSocket)
        {
            GuiUpdater(data);
            //write message to all clients
            foreach (var item in clients)
            {
                if (item.ClientHandlerSocket != senderSocket)
                {
                    item.Send(data);
                }
            }
        }

        //Client:
        private void Receive()
        {
            while (true)
            {
                //length = clientSocket.Receive(buffer);
                //informer(Encoding.UTF8.GetString(buffer, 0, length));
                string data = "";
                while (true)
                {
                    int length = clientSocket.Receive(buffer);
                    data = Encoding.UTF8.GetString(buffer, 0, length);
                    GuiUpdater(data);
                }
            }
        }

        public void Send(byte[] data)
        {
            if (clientSocket != null) //check if clientsocket connected!
            {
                clientSocket.Send(data);
            }
            //clientSocket.Send(data);    // senden an Client
            //serverSocket.Send(data);
        }
    }
}
