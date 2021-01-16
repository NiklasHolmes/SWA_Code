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

        public Client(string ip, int port, Action<string> messageInformer)
        {
            try
            {
                this.MessageInformer = messageInformer;     //
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                clientsocket = client.Client;
                //StartReceiving();
                Task.Factory.StartNew(Receive);
            }
            catch (Exception)
            {
                messageInformer("Server not ready");
                //AbortInformer(); //reset Client Communication
            }
        }

        /*
        public void StartReceiving()
        {
            //Start receiving in new Thread
            Task.Factory.StartNew(Receive);
        }
        */

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
