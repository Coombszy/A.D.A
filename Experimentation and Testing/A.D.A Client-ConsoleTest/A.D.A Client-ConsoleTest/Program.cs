using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.D.A_Client_ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketHandler Client = new SocketHandler();
            Client.StartSocket();
            string Input;
            while (true)
            {
                Input = Console.ReadLine();
                Client.Send(Input);
                var Temp = Client.Listen();
                Console.WriteLine(Temp.Response);
                Console.WriteLine("Comd:" + Temp.Command);
                Console.WriteLine("Dict:");
                foreach (string dictent in Temp.Dictionary)
                {
                    Console.WriteLine("     -" + dictent);
                }
            }
        }
    }
}
