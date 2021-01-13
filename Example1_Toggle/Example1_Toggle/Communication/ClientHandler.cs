using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example1_Toggle.Communication
{
    public class ClientHandler
    {
        private Action<string, Socket> action;
        private byte[] buffer = new byte[512];
        private Thread clientReceiveThread;

        public Socket Clientsocket
        {
            get;
            private set;
        }

        public ClientHandler(Socket socket, Action<string, Socket> action)
        {
            this.Clientsocket = socket;
            this.action = action;
            //start receiving in a new thread
            clientReceiveThread = new Thread(Receive);
            clientReceiveThread.Start();
        }

        private void Receive()
        {
            string message = "";
            while (true)
            {
                int length = Clientsocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                //set name property if not already done

                //inform GUI via delegate
                action(message, Clientsocket);
            }
        }

        public void Send(string message)
        {
            Clientsocket.Send(Encoding.UTF8.GetBytes(message));
        }


    }
}
