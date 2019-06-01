using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XFSpeechDemo
{
    class CommandHandler
    {
        //ref bool debugMode_Main;
        public CommandHandler()//ref bool debugMode)
        {
            //debugMode_Main = debugMode;
        }//Parameters are variables crucial to the running of the client
        public string HandleCommand(string IncomingCommand)
        {
            if(IncomingCommand.Contains("#H:"))
            {
                return Command_HardCoded(StripCommandClassification(IncomingCommand));
            }
            else if (IncomingCommand.Contains("#L:"))
            {
                return Command_Local(StripCommandClassification(IncomingCommand));
            }
            return "ERROR IN HandleCommand";
        }
        private string StripCommandClassification(string IncomingCommand)
        {
            string CommandData = IncomingCommand.Remove(0, 3);
            CommandData = CommandData.Remove(CommandData.Length - 1, 1);
            return CommandData;
        }

        private string Command_HardCoded(string CommandData)
        {
            switch (CommandData)
            {
                case "WAKESOUND":
                    //doo something - play a wake sound or change UI colour to alert that ada is listening
                    Console.WriteLine("WAKESOUND TRIGGERED!");
                    return "";
                case "SHUTDOWN":
                    Environment.Exit(1);
                    return "";
                /*case "DEBUGTOGGLE":
                    debugMode_Main = !debugMode_Main;//toggle debug state
                    Console.WriteLine("TOGGLED! :"+debugMode_Main);
                    return "";*/
            }
            return "ERROR IN Command_HardCoded";
        }
        private string Command_Local(string CommandData)
        {
            switch(CommandData)
            {
                case "OPENGOOGLE":
                    System.Diagnostics.Process.Start("https://www.google.co.uk/");
                    return "";
                case "EXTERNALIP":
                    string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                    //Clipboard.SetText(pubIp);
                    return pubIp;
                case "TIMENOW":
                    DateTime now = DateTime.Now;
                    return (string.Format("{0:h:mm}", now));
            }
            return "ERROR IN Command_Local";
        }
    }
}
