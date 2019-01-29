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
            int option;
            while (true)
            {
                Console.WriteLine("Socket Testing and Experimentation");
                Console.WriteLine("     1) Server");
                Console.WriteLine("     2) Client");
                Console.WriteLine("     3) Client - Random MessageSpam");
                try
                {
                    option = int.Parse(Console.ReadLine());
                    if(option > 0 && option <=3 )
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Option! Press enter to continue...");
                    Console.ReadLine();
                    Console.Clear();

                }
            }

            if(option == 1)
            {
                ServerHandler Server = new ServerHandler();
                Server.StartHandler();
            }
            else if (option == 2)
            {
                SocketHandler Client = new SocketHandler();
                Client.StartSocket();
                string Input;
                while(true)
                {
                    Input = Console.ReadLine();
                    Client.Send(Input);
                    var Temp = Client.Listen();
                    Console.WriteLine(Temp.Response);
                    Console.WriteLine("Dict:");
                    foreach(string dictent in Temp.Dictionary)
                    {
                        Console.WriteLine("     -" + dictent);
                    }
                }
            }
            else if (option == 3)
            {
                SocketHandler Client = new SocketHandler();
                Client.StartSocket();
                string RandomString;
                while (true)
                {
                    RandomString = GetRandom(10);
                    Client.Send(RandomString);
                    if(Client.Listen().Response.Contains(RandomString))
                    {
                        Console.WriteLine(RandomString + " : Passed!");
                    }
                    else
                    {
                        Console.WriteLine(RandomString + " : Failed!!");
                        Console.ReadLine();
                    }
                }
            }

            Console.WriteLine("End of Program!");
            Console.Read();
        }
        public static string GetRandom(int size)
        {
            Random random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyzQWERTYUIOPLKJHGFDSAZXCVBNM0123456789";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(size).ToArray());
        }
    }
}
