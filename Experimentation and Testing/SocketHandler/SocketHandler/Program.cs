using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter Verification: ");
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            string Pass = Console.ReadLine();
            Console.ForegroundColor = (ConsoleColor)temp;
            Console.WriteLine("HEYSERVER!" + Pass);
            Console.ReadLine();
        }
    }
}
