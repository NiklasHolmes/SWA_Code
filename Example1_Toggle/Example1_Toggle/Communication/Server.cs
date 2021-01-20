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
        public Action<string> informer;
        Thread acceptingThread;

        public Server(string ip, int port, Action<string> informer)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(5);         // wie viele Anfragen auf einmal bearbeiten
            this.informer = informer;

            Task.Factory.StartNew(StartAccepting);
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
                    clients.Add(new ClientHandler(serverSocket.Accept()));
                    informer("newClient");
                }
                catch (Exception e) { }
            }
        }
        
        /*
        //SELBST: 
        public void SendMessage(string message)
        {
            //ClientSocket.Send(Encoding.UTF8.GetBytes(message));
            foreach (var item in clients)
            {
                item.ClientSocket.Send(Encoding.UTF8.GetBytes(message));
            }
        }
        */
    }
}
