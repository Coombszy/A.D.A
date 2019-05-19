using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Globalization;
using System.Threading;

namespace A.D.A_Host
{
    class CommandHandler
    {
        private Thread JobThread;
        public string HandleCommand(string IncomingCommand)
        {
            if (IncomingCommand.Contains("#S:"))
            {
                return Command_ServerHardCoded(StripCommandClassification(IncomingCommand));
            }
            return "ERROR IN HandleCommand";
        }
        private string StripCommandClassification(string IncomingCommand)
        {
            string CommandData = IncomingCommand.Remove(0, 3);
            CommandData = CommandData.Remove(CommandData.Length - 1, 1);
            return CommandData;
        }

        private string Command_ServerHardCoded(string CommandData)
        {
            switch (CommandData)
            {
                case "STARTESXI":
                    JobThread = new Thread(StartESXI);
                    JobThread.Start();
                    return "";
                case "SHUTDOWN":
                    Environment.Exit(1);
                    return "";
                case "STOPJOB":
                    JobThread.Abort();
                    return "";



                    /*case "DEBUGTOGGLE":
                        debugMode_Main = !debugMode_Main;//toggle debug state
                        Console.WriteLine("TOGGLED! :"+debugMode_Main);
                        return "";*/
            }
            return "ERROR IN Command_HardCoded";
        }



        //--------- FUNCTIONS FOR RUNNNING JOBS HERE----------

        //NETWORK COMMANDS
        private void SendMagicPacketA(string MacAddress, string IpAddress)
        {
            MacAddress = Regex.Replace(MacAddress, "[-|:]", "");       // Remove any semicolons or minus characters present in our MAC address

            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                EnableBroadcast = true
            };

            int PayloadIndex = 0;

            /* The magic packet is a broadcast frame containing anywhere within its payload 6 bytes of all 255 (FF FF FF FF FF FF in hexadecimal), followed by sixteen repetitions of the target computer's 48-bit MAC address, for a total of 102 bytes. */
            byte[] Payload = new byte[1024];    // Our packet that we will be broadcasting

            // Add 6 bytes with value 255 (FF) in our payload
            for (int i = 0; i < 6; i++)
            {
                Payload[PayloadIndex] = 255;
                PayloadIndex++;
            }

            // Repeat the device MAC address sixteen times
            for (int j = 0; j < 16; j++)
            {
                for (int k = 0; k < MacAddress.Length; k += 2)
                {
                    var s = MacAddress.Substring(k, 2);
                    Payload[PayloadIndex] = byte.Parse(s, NumberStyles.HexNumber);
                    PayloadIndex++;
                }
            }

            sock.SendTo(Payload, new IPEndPoint(IPAddress.Parse(IpAddress), 0));  // Broadcast our packet
            sock.Close(10000);
        }
        private void SendMagicPacketAA(string MacAddress)
        {
            MacAddress = Regex.Replace(MacAddress, "[-|:]", "");
            WOLClass client = new WOLClass();
            client.Connect(new
               IPAddress(0xffffffff),  //255.255.255.255  i.e broadcast
               0x2fff); // port=12287 let's use this one 
            client.SetClientToBrodcastMode();
            //set sending bites
            int counter = 0;
            //buffer to be send
            byte[] bytes = new byte[1024];   // more than enough :-)
                                             //first 6 bytes should be 0xFF
            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            //now repeate MAC 16 times
            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    bytes[counter++] =
                        byte.Parse(MacAddress.Substring(i, 2),
                        NumberStyles.HexNumber);
                    i += 2;
                }
            }

            //now send wake up packet
            int reterned_value = client.Send(bytes, 1024);
        }
        public void SendMagicPacket(string macAddress, string ipAddress, string subnetMask)
        {
            UdpClient client = new UdpClient();

            Byte[] datagram = new byte[102];

            for (int i = 0; i <= 5; i++)
            {
                datagram[i] = 0xff;
            }

            string[] macDigits = null;
            if (macAddress.Contains("-"))
            {
                macDigits = macAddress.Split('-');
            }
            else
            {
                macDigits = macAddress.Split(':');
            }

            if (macDigits.Length != 6)
            {
                throw new ArgumentException("Incorrect MAC address supplied!");
            }

            int start = 6;
            for (int i = 0; i < 16; i++)
            {
                for (int x = 0; x < 6; x++)
                {
                    datagram[start + i * 6 + x] = (byte)Convert.ToInt32(macDigits[x], 16);
                }
            }

            IPAddress address = IPAddress.Parse(ipAddress);
            IPAddress mask = IPAddress.Parse(subnetMask);
            //IPAddress broadcastAddress = address.GetBroadcastAddress(mask);
            IPAddress broadcastAddress = IPAddress.Parse(subnetMask);


            client.Send(datagram, datagram.Length, broadcastAddress.ToString(), 3);
        }
        private void StartESXI()
        {
            SendMagicPacket("3c:97:0e:a9:71:02", "192.168.1.205","255.255.255.0");
            Console.WriteLine("MAGIC PACKET SENT!");
        }
       
    }
    public class WOLClass : UdpClient
    {
        public WOLClass() : base()
        { }
        //this is needed to send broadcast packet
        public void SetClientToBrodcastMode()
        {
            if (this.Active)
                this.Client.SetSocketOption(SocketOptionLevel.Socket,
                                          SocketOptionName.Broadcast, 0);
        }
    }
}
