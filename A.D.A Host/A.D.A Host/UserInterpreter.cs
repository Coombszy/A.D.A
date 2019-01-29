using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace A.D.A_Host
{
    class UserInterpreter
    {
        private MemoryHandler MemoryUnit;
        private List<string> EventDictionary;
        public UserInterpreter(MemoryHandler activeMemoryHandler)
        {
            this.MemoryUnit = activeMemoryHandler;
            buildEventDictionary();
        }
        public void buildEventDictionary()
        {
            EventDictionary = MemoryUnit.GetEventsofActiveNode();
        }//To be used by the Voice recog for Language Dictionary to be sent

        public string Response(string D_INPUT)
        {
            buildEventDictionary();
            if (EventDictionary.Contains(D_INPUT))
            {
                MemoryUnit.Navigate(D_INPUT);
                //Console.WriteLine("NAVIGATED! : " + MemoryUnit.ActiveNode.MyName);//------------- SEE WHICH NODES ARE BEING NAVIGATED TOO ------------ 
                buildEventDictionary();
                return GetAResponse();
                /*
                foreach (string aa in EventDictionary)
                {
                    Console.WriteLine("     str:" + aa);
                }*/ // --------------------------------------------------------SEE WHAT PHRASES ARE CURRENTLY ACCEPTABLE----------------
            }
            else
            {
                return "";
            }
        }//To be completly Overhauled to use RecEngine for user voice to string 
        private string GetAResponse()
        {
            string Command = MemoryUnit.GetCommand();
            string Response = MemoryUnit.GetResponse();
            if (Command.Contains("#"))
            {
                HandleTriggerCode(Command);
                //int start = Response.IndexOf('#') + 1;
                //int end = Response.IndexOf('#', start);
                //Response = Response.Substring(end+1, Command.Length-(end+1));
            }
            if (Response != "")
            {
                return (Response);
            }
            return "";
        }

        public void tempDebug_UserResponse(string D_INPUT)
        {
            buildEventDictionary();
            if(EventDictionary.Contains(D_INPUT))
            {
                MemoryUnit.Navigate(D_INPUT);
                Console.WriteLine("NAVIGATED! : " +MemoryUnit.ActiveNode.MyName);//------------- SEE WHICH NODES ARE BEING NAVIGATED TOO ------------ 
                tempDebug_SendToUser(GetAResponse());
                buildEventDictionary();
                /*
                foreach (string aa in EventDictionary)
                {
                    Console.WriteLine("     str:" + aa);
                }*/ // --------------------------------------------------------SEE WHAT PHRASES ARE CURRENTLY ACCEPTABLE----------------
            }
        }//To be completly Overhauled to use RecEngine for user voice to string 
        private void tempDebug_SendToUser(string text)
        {
            if (text != "")
            {
                Console.WriteLine("A.D.A ~ "+text);
            }
        }//To be overhauled for sending the text info to the interface
        private void HandleTriggerCode(string Data)
        {
            int start = Data.IndexOf('#') + 1;
            int end = Data.IndexOf('#', start);
            string result = Data.Substring(start, end - start);
            if(result[0] == 'H')
            {
                HardCodeCommands(result.Remove(0, 2));
            }

        }
        private void HardCodeCommands(string Command)
        {
            Console.WriteLine(":" + Command);
            switch (Command)
            {
                case "WAKESOUND":
                    //Console.WriteLine("CONSOLEBEEP!");//TEMP WILL PLAY SOUND EFFECT
                    Console.Beep();
                    break;
                case "MARIOTIME":
                    {
                        Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(375); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125); Thread.Sleep(1125); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125);
                    }
                    break;
            }
        }//This system needs to be moved to the Interface connecting to the host
    }
}
