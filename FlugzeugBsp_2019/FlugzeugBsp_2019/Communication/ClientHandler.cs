using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlugzeugBsp_2019.Communication
{
    public class ClientHandler
    {
        private Action<string> GuiUpdaterAction;
        //private Thread clientReceiveThread;
        private Socket ClientSocket;

        byte[] buffer = new byte[256]; // oder 512

        public ClientHandler(Socket cs, Action<string> action)
        {
            ClientSocket = cs;
            GuiUpdaterAction = action;

            Task.Factory.StartNew(ReceiveData);

            // oder: 
            /*
            clientReceiveThread = new Thread(ReceiveData);
            clientReceiveThread.Start();
            */
        }

        private void ReceiveData()
        {
            int length;

            while (true)
            {
                length = ClientSocket.Receive(buffer);
                GuiUpdaterAction(Encoding.UTF8.GetString(buffer, 0, length));
            }
        }
    }
}
