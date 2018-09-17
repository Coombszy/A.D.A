using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace AI_Host
{
    class InterfaceHandler
    {
        private TcpClient InterfaceSocket;
        private NetworkStream DataStream;

        public InterfaceHandler(TcpClient Socket)
        {
            this.InterfaceSocket = Socket;
            //Verfication 
            Console.WriteLine(" >> Interface verification successful, Bringing AI Instance online!");
            Send("Interface successfully initialised and connected, I am Online and Ready sir");

            //Temporary Session of AI
            string Command;
            Random rnd = new Random();
            int tempInt;
            string tempString;
            while(true)
            {
                try
                {
                    Command = ListenAndWait();
                    if (Command.Contains("Calculate"))
                    {
                    }
                    else
                    {
                        switch (Command)
                        {
                            case "What are you?":
                                Send("I am a basic AI system, using a Interface to Host model. This allows Multiple Instances of myself across multiple devices. Hope I can help!");
                                break;
                            case "Hello":
                                tempInt = rnd.Next(1, 4);
                                if (tempInt == 1)
                                    Send("Hello!");
                                else if (tempInt == 2)
                                    Send("Hey!");
                                else if (tempInt == 3)
                                    Send("Hi there!");
                                else if (tempInt == 4)
                                    Send("Greetings!");
                                break;
                            case "How are you?":
                                Send("I'm great thanks! What about You?");
                                Command = ListenAndWait();
                                if (Command == "Good")
                                {
                                    Send("I'm glad!");
                                }
                                else
                                {
                                    Send("I'm Sorry.");
                                }
                                break;
                            case "Shutdown interface":
                                Send("Are you sure?");
                                if(ListenAndWait() == "Yes")
                                {
                                    Send("Interface Instance shutting down, Goodbye sir$COMMAND$SHUTDOWN");
                                    Socket.Close();
                                    Console.WriteLine(" >> Interface disconnecting, Shutting down AI Instance.");
                                    Thread.CurrentThread.Abort();
                                }
                                else
                                {
                                    Send("Shutdown aborted!");
                                }
                                break;
                            case "Goto a youtube channel":
                                Send("And what Channel would you like to visit?");
                                tempString = ListenAndWait();
                                if(tempString == "The yogscast")
                                {
                                    Send("Ofcourse sir, Accessing The Yogscast$COMMAND$YOUTUBE:BlueXephos");
                                }
                                else if (tempString =="Hat films")
                                {
                                    Send("The troublesome trio, Accessing Hat Films$COMMAND$YOUTUBE:HaatFilms");
                                }
                                else
                                {
                                    Send("Channel not found in Databanks, Accessing the channel " + tempString + "$COMMAND$YOUTUBE:"+tempString);
                                }
                                break;
                            case "Open google":
                                Send("Accessing Google$COMMAND$GOOGLE");
                                break;
                            default:
                                Send("Sorry, I don't understand the Command. " + Command);
                                break;
                        }
                    }
                }
                catch(Exception e)
                {
                    //Console.WriteLine("MAJOR ERROR! "+ e.ToString());
                }
            }
        }
        public void Send(string ToSend)
        {
            NetworkStream ServerStream = InterfaceSocket.GetStream();
            string data = ToSend;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data + "$END$");
            ServerStream.Write(outStream, 0, outStream.Length);
            ServerStream.Flush();
        }

        public string ListenAndWait()
        {
            byte[] BytesReceived = new byte[2048];
            string DataReceived = null;

            while (true)
            {
                try
                {
                    DataStream = InterfaceSocket.GetStream();
                    DataStream.Read(BytesReceived, 0, BytesReceived.Length);
                    DataReceived = System.Text.Encoding.ASCII.GetString(BytesReceived);
                    DataReceived = DataReceived.Substring(0, DataReceived.IndexOf("$END$"));
                    break;
                }
                catch { }
            }
            return DataReceived;
        }

        public string Listen()
        {
            byte[] BytesReceived = new byte[2048];
            string DataReceived = null;

            while (true)
            {
                try
                {
                    DataStream = InterfaceSocket.GetStream();
                    if (DataStream.DataAvailable)
                    {
                        DataStream.Read(BytesReceived, 0, BytesReceived.Length);
                        DataReceived = System.Text.Encoding.ASCII.GetString(BytesReceived);
                        DataReceived = DataReceived.Substring(0, DataReceived.IndexOf("$END$"));
                        DataStream.Flush();
                    }
                    else { DataReceived = ""; }
                    break;
                }
                catch
                {
                    DataReceived = "";
                    break;
                }
            }
            return DataReceived;
        }
    }
}
