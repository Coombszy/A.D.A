using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.D.A_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryHandler MemoryUnit = new MemoryHandler();
            MemoryUnit.BuildEventList();
            MemoryUnit.BuildMemoryStructure();
            Console.WriteLine("Press Enter to Continue...");
            //Console.ReadLine();
            Console.Clear();
            ServerHandler Server = new ServerHandler(ref MemoryUnit);
            Server.PortNumber = 3300;
            Server.StartHandler();
            Console.WriteLine("END OF PROGRAM!");
            Console.ReadLine();
        }
    }
}
