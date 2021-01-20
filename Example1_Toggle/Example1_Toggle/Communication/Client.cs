using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Example1_Toggle.Communication
{
    public class Client
    {
        byte[] buffer = new byte[512];
        Socket clientsocket;
        Action<string> MessageInformer;
        //Action AbortInformer;


        public Client(IPAddress ip, int port, Action<string> messageInformer)
        {
            try
            {
                this.MessageInformer = messageInformer;
                TcpClient client = new TcpClient();
                client.Connect(ip, port);
                clientsocket = client.Client;
                //StartReceiving();
                Task.Factory.StartNew(Receive);
            }
            catch (Exception)
            {
                messageInformer("Server is not ready!");
                //AbortInformer(); //reset Client Communication
            }
        }

        private void Receive()
        {
            string message = "";
            while (true)
            {
                int length = clientsocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                //inform GUI via delegate
                MessageInformer(message);
            }
        }
    }
}
