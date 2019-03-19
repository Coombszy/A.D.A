using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace A.D.A_Host
{
    class ServerHandler
    {
        TcpListener HostSocket; //Host socket to allow incoming connections
        public int PortNumber = 25565; //Port to listen to
        private List<Thread> ConnectedClients; // List containing the threads with 
        private TcpClient TempSocket; //temporary location to store an accepted new client
        private MemoryHandler MasterMemoryUnit;
 
        public ServerHandler(ref MemoryHandler MainMemoryUnit)
        {
            this.MasterMemoryUnit = MainMemoryUnit;
        }
        public void StartHandler()
        {
            try
            {
                ConnectedClients = new List<Thread>();
                HostSocket = new TcpListener(IPAddress.Any, PortNumber);
                HostSocket.Start();
                Console.WriteLine(" >> Server Started!");
                Thread UserInput = new Thread(ServerCommandEntry);
                UserInput.Start();
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
                    Console.WriteLine(" >> Client is connecting...");
                    ConnectedClients.Add(new Thread(CreateClientThread));
                    ConnectedClients[ConnectedClients.Count - 1].Start();
                }
            }
        }//This will hold up the entire thread
        private void CreateClientThread()
        {
            ClientHandler Client = new ClientHandler(TempSocket, ref this.MasterMemoryUnit);
        }
        private void ServerCommandEntry()
        {
            while (true)
            {
                string Input = Console.ReadLine().ToLower();
                switch (Input)
                {
                    case "help":
                        Console.WriteLine("shutdown - shutdown these server");
                        Console.WriteLine("rebuild - rebuilds the list of events");

                        break;
                    case "shutdown":
                        Environment.Exit(1);
                        break;
                    case "rebuild":
                        MasterMemoryUnit.BuildEventList();
                        MasterMemoryUnit.BuildMemoryStructure();
                        break;
                }
            }
        }
    }
}
