using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_wi19b040.Communication
{
    public class Client : ViewModelBase
    {

        Socket clientSocket;
        public int port = 8000;
        byte[] buffer = new byte[512];
        public Action<string> GuiUpdater;
        Action CloseConnInformer;

        public Client(Action<string> guiUpdater)
        {
            this.GuiUpdater = guiUpdater;
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Loopback, port));
            clientSocket = client.Client;
            Task.Factory.StartNew(Receive);
        }

        //Client:
        private void Receive()
        {
            string data = "";

            while (!data.Contains("QUIT"))
            {
                int length = clientSocket.Receive(buffer);
                data = Encoding.UTF8.GetString(buffer, 0, length);
                GuiUpdater(data);
            }
        }

        public void Send(byte[] data)
        {
            if (clientSocket != null) //checking
            {
                clientSocket.Send(data);
            }
        }

        public void CloseClientServerConnection()
        {
            clientSocket.Close();
        }
    }
}
