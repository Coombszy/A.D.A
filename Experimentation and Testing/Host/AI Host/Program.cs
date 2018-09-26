using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.IO;

namespace AI_Host
{
    class Program
    {
        static TcpClient InterfaceSocket;
        static void Main(string[] args)
        {
            TcpListener HostSocket;
            //TcpClient InterfaceSocket;
            HostSocket = new TcpListener(IPAddress.Any, 25565);
            HostSocket.Start();
            List<Thread> Threads = new List<Thread>();
            Console.WriteLine(" >> Host Initiated!");
            Console.WriteLine(" >> Waiting for an interface");
            while (true)
            {
                try
                {
                    if (HostSocket.Pending())
                    {
                        InterfaceSocket = HostSocket.AcceptTcpClient();
                        Console.WriteLine(" >> An interface is connecting");
                        Threads.Add(new Thread(CreateInterface));
                        Threads[Threads.Count-1].Start();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("CATCH STATEMENT= "+e.Message);
                }
            }

            //--------------
            Console.ReadLine();
        }
        public static void CreateInterface()
        {
            InterfaceHandler NewInterface = new InterfaceHandler(InterfaceSocket);
        }
    }
}
