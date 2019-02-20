using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.D.A_Client_ConsoleTest
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            CommandHandler CommandExe = new CommandHandler();
            SocketHandler Client = new SocketHandler();
            Client.StartSocket();
            string Input;
            while (true)
            {
                Input = Console.ReadLine();
                Client.Send(Input);
                var Temp = Client.Listen();
                Console.WriteLine("Comd:" + Temp.Command);

                //Test response and command merging using a "cut in" value. in this test being #/# <-- in this location data will be inserted here for the user
                string CommandData = CommandExe.HandleCommand(Temp.Command);
                if(CommandData != "" && Temp.Response.Contains("#/#"))
                {
                    Console.WriteLine(Temp.Response.Replace("#/#", CommandData));
                }
                else
                {
                    Console.WriteLine(Temp.Response);
                }
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                
                Console.WriteLine("Dict:");
                foreach (string dictent in Temp.Dictionary)
                {
                    Console.WriteLine("     -" + dictent);
                    
                }
            }
        }
    }
}
