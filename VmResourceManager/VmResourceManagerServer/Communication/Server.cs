using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VmResourceManagerServer.Communication
{
    //Handles the communication requests from Client till Handshake with Client
    class Server
    {
        private List<ClientHandler> clients = new List<ClientHandler>();        //
        private Action<string> action;
        TcpListener tcpListener;
        // use TCP Listener or Socket
        public Server(string ip, int port, Action<string> action)
        {
            this.action = action;

            tcpListener = new TcpListener(IPAddress.Parse(ip), port);       // using System.Net.
            tcpListener.Start();

            Task.Factory.StartNew(StartAccepting);      // nur wenn mehrere Clients
            
        }

        private void StartAccepting()
        {
            while (true)                        // damit mehrere angenommen werden => nach Annahme wird auf nächsten gewartet
            {
                clients.Add(new ClientHandler(tcpListener.AcceptSocket(), action));     // immer wenn einer verbunden => in Liste hinzufügen  // auch hier add parameter für action
                // ClientHandler => einfach automatisch anlegen lassen
                // ich möchte lesen => weiß aber nicht wann 

            }
        }
    }
}
