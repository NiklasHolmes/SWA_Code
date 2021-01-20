using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ShapeServer.Communication
{
    class Communicator
    {
        Socket serverSocket;
        Socket clientSocket;
        const int port = 10100;
        byte[] buffer = new byte[512];
        public Action<string> informer;

        public Communicator(bool isServer, Action<string> informer)
        {
            this.informer = informer;

            if (isServer)
            {
                serverSocket = new Socket(
                    AddressFamily.InterNetwork,     // welcher Internettyp
                    SocketType.Stream,              // wie Pakete übermittelt werden
                    ProtocolType.Tcp                // welches Protokoll
                );

                // socket auf Port binden: 
                serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
                serverSocket.Listen(5);             //=> wie viele Clients in die Liste kommen können die gerade anfragen / auf Bestätigung warten

                Task.Factory.StartNew(StartAccepting);
            }
            else
            {
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Loopback, port));
                clientSocket = client.Client;               // Socket vom Client
                Task.Factory.StartNew(Receive);
            }
        }

        public void StartAccepting()
        {
            clientSocket = serverSocket.Accept();           // clientSocket beim Server => an den schicke ich
            Task.Factory.StartNew(Receive);                 // 
        }

        private void Receive()
        {
            int length;
            while (true)
            {
                length = clientSocket.Receive(buffer);          // 
                informer(Encoding.UTF8.GetString(buffer, 0, length));
            }
        }

        public void Send(byte[] data)       // von Server ausgeführt
        {
            clientSocket.Send(data);        // Daten an ClientSocket senden
        }


    }
}
