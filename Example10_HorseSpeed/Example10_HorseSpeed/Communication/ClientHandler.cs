using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example10_HorseSpeed.Communication
{
    public class ClientHandler
    {
        private Action<string, Socket> action;
        private byte[] buffer = new byte[512];
        private Thread clientReceiveThread;

        public string Name { get; private set; }

        public Socket ClientHandlerSocket
        {
            get;
            private set;
        }

        public ClientHandler(Socket socket, Action<string, Socket> action)
        {
            this.ClientHandlerSocket = socket;
            this.action = action;
            //start receiving in a new thread
            clientReceiveThread = new Thread(Receive);
            clientReceiveThread.Start();
        }

        public void Send(string data)
        {
            ClientHandlerSocket.Send(Encoding.UTF8.GetBytes(data));
        }

        private void Receive()
        {
            /*
            int length;
            while (true)
            {
                length = socket.Receive(buffer);
                GuiUpdater(Encoding.UTF8.GetString(buffer, 0, length));
            }*/
            string message = "";
            while (true)
            {
                int length = ClientHandlerSocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                //set name property if not already done
                if (Name == null && message.Contains("|"))
                {
                    Name = message.Split('|')[0];
                }
                //inform GUI via delegate
                action(message, ClientHandlerSocket);
            }
        }
    }
}
