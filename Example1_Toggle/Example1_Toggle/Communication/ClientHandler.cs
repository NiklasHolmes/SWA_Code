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
        //private Action<string, Socket> action;
        //byte[] buffer = new byte[512];
        //private Thread clientReceiveThread;
        // gleichzeitiges Ausführen (= Multitasking) (z.B. mehrere Clients können gleichzeitig an Server senden

        public Socket ClientSocket
        {
            get;
            private set;
        }

        public ClientHandler(Socket socket)
        {
            this.ClientSocket = socket;
        }
    }
}
