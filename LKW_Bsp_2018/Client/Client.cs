using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client
    {
        Socket clientSocket;
        byte[] buffer = new byte[256];  // oder 512
        int port = 10100;

        public Client()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, port));

            /* //oder:
            TcpClient client = new TcpClient();

            client.Connect(new IPEndPoint(IPAddress.Loopback, 10100));
            clientSocket = client.Client;
            */
        }

        public void SendData(byte[] data)
        {
            clientSocket.Send(data); //Encoding.UTF8.GetBytes
        }
    }
}
