using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Telnet_Client
{
    public class Client
    {
        // Two ways to implement a (TCP) Client
        // TcpClient (auf TCP Protokoll fixiert)
        //Socket        (hier mehr Möglichkeiten)
        Socket clientSocket;
        byte[] buffer = new byte[256];
        public string Name;
        //private const int port = 10100;

        public Client(int port)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, port));
        }

        public void SendData(string message, string name)
        {
            Name = name;
            if(clientSocket != null)
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(name + ":" + message));
            }
        }

        public string ReceiveData()
        {
            string message = "";
            int length;
            while (!message.Contains("\r\n"))
            {
                length = clientSocket.Receive(buffer);
                message += Encoding.UTF8.GetString(buffer, 0, length);
            }
            return message;
        }
    }
}
