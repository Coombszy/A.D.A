using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SocketHandler
{
    class ServerHandler
    {
        TcpListener HostSocket; //Host socket to allow incoming connections
        public int PortNumber = 3300; //Port to listen to
        private List<Thread> ConnectedClients; // List containing the threads with 
        private TcpClient TempSocket; //temporary location to store an accepted new client
        public void StartHandler()
        {
            try
            {
                ConnectedClients = new List<Thread>();
                HostSocket = new TcpListener(IPAddress.Any, PortNumber);
                HostSocket.Start();
                Console.WriteLine(" >> Server Started!");
                ListeningLoop();
            }
            catch(Exception e)
            {
                Console.WriteLine("FATALERROR - Starting ServerHandler");
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }
        }
        private void ListeningLoop()
        {
            while(true)
            {
                if(HostSocket.Pending())
                {
                    TempSocket = HostSocket.AcceptTcpClient();
                    Console.WriteLine(" >> Client is connecting ");
                    ConnectedClients.Add(new Thread(CreateClientThread));
                    ConnectedClients[ConnectedClients.Count - 1].Start();
                }
            }
        }//This will hold up the entire thread

        private void CreateClientThread()
        {
            ClientHandler Client = new ClientHandler(TempSocket);
        }
    }
}
