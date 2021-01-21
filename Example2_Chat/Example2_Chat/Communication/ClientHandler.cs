using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example2_Chat.Communication
{
    public class ClientHandler
    {
        private Thread clientReceiveThread;
        public Socket ClientSocket;          //{ get; private set; }
        byte[] buffer = new byte[256];
        private Action<string, Socket> action;
        public string Name { get; private set; }

        public ClientHandler(Socket cs, Action<string, Socket> action)
        {
            ClientSocket = cs;

            ClientSocket.Send(Encoding.ASCII.GetBytes("Hello Client!\r\nPlease enter your name"));

            this.action = action;
            clientReceiveThread = new Thread(ReceiveData);
            clientReceiveThread.Start();
        }

        public void ReceiveData()
        {
            string newData = "";

            while (!newData.Contains("@quit"))
            {
                int length = ClientSocket.Receive(buffer);
                newData = Encoding.ASCII.GetString(buffer, 0, length);

                if (Name == null && newData.Contains(":"))
                {
                    Name = newData.Split(':')[0];
                }
                //GUI durch Delegate informieren:
                action(newData, ClientSocket);
            }
        }

        public void SendData(string data)
        {
            ClientSocket.Send(Encoding.UTF8.GetBytes(data));
        }
    }
}
