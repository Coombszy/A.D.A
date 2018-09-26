using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void tempDebug_RecEngine_EventUserResponse(string D_INPUT)
        {
            if(EventDictionary.Contains(D_INPUT))
            {
                MemoryUnit.Navigate(D_INPUT);
                tempDebug_SendToUser(GetAResponse());
                buildEventDictionary();
            }
        }//To be completly Overhauled to use RecEngine for user voice to string 
        private void tempDebug_SendToUser(string text)
        {
            if (text != "")
            {
                Console.WriteLine(text);
            }
        }//To be overhauled for sending the text info to the interface
        private string GetAResponse()
        {
            string Response = MemoryUnit.GetTrigger();
            if (Response.Contains("#"))
            {
                HandleTriggerCode(Response);
                int start = Response.IndexOf('#') + 1;
                int end = Response.IndexOf('#', start);
                Response = Response.Substring(end+1, Response.Length-(end+1));
            }
            else if(Response != "")
            {
                return(Response);
            }
            return "";
        }
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
            switch (Command)
            {
                case "WAKESOUND":
                    Console.WriteLine("CONSOLEBEEP!");//TEMP WILL PLAY SOUND EFFECT
                    break;
            }
        }//This system needs to be moved to the Interface connecting to the host
    }
}
