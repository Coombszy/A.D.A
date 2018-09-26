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
            Console.ReadLine();
        }
    }
}
