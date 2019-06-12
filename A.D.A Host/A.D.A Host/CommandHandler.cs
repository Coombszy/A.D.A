﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Globalization;
using System.Threading;
using Renci.SshNet;

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
                case "STARTLINUXLITE":
                    JobThread = new Thread(StartLinuxLite);
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
        private void SendMagicPacket(string TargetMac)
        {

            PhysicalAddress target = PhysicalAddress.Parse(TargetMac.ToUpper());
            IPAddress senderAddress = IPAddress.Any;

            byte[] payload = new byte[102]; // 6 bytes of ff, plus 16 repetitions of the 6-byte target
            byte[] targetMacBytes = target.GetAddressBytes();

            // Set first 6 bytes to ff
            for (int i = 0; i < 6; i++)
                payload[i] = 0xff;

            // Repeat the target mac 16 times
            for (int i = 6; i < 102; i += 6)
                targetMacBytes.CopyTo(payload, i);

            // Create a socket to send the packet, and send it
            using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                sock.Bind(new IPEndPoint(senderAddress, 0));
                sock.SendTo(payload, new IPEndPoint(IPAddress.Broadcast, 7));
            }
        }
        private bool SendCommandSHH(string host, string userName, string psw, string finalCommand)
        {
            new KeyboardInteractiveAuthenticationMethod(userName);

            ConnectionInfo conInfo = new ConnectionInfo(host, 22, userName, new AuthenticationMethod[]{
                new PasswordAuthenticationMethod(userName,psw)
            });
            SshClient client = new SshClient(conInfo);

            client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(60);
            try
            {
                client.Connect();
                var outptu = client.RunCommand(finalCommand);
                client.Disconnect();
                client.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" >> SSH Job Failed: " + ex.Message);
                client.Disconnect();
                client.Dispose();
                return false;
            }

            
        }
        private bool IsLive(string IpAddress)
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(IpAddress);

            if (pingReply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void StartESXI()
        {
            if (IsLive("192.168.1.200") == false)
            {
                Console.WriteLine(" >> ESXI Start Job has started!");
                SendMagicPacket("04-92-26-C3-BF-BB");
                Thread.Sleep(200000);
                if (IsLive("192.168.1.200"))
                {
                    Console.WriteLine(" >> ESXI Start Job has finished");
                }
                else
                {
                    Console.WriteLine(" >> ESXI Start Job has finished but failed");
                }
            }
        }
        /*private void ShutdownESXI()
        {
            if (IsESXILive() == true)
            {
                Console.WriteLine(" >> ESXI Shutdown Job has started!");
                SendMagicPacket("04-92-26-C3-BF-BB");
                Thread.Sleep(200000);
                if (IsESXILive())
                {
                    Console.WriteLine(" >> ESXI Start Job has finished");
                }
                else
                {
                    Console.WriteLine(" >> ESXI Start Job has finished but failed");
                }
            }
            else
            {
                Console.WriteLine("");
            }
        }*/
        private void StartLinuxLite()
        {
            Console.WriteLine(" >> Linux Lite Start Job has started!");
            if (IsLive("192.168.1.200") == false)
            {
                Console.WriteLine(" >> Linux Lite Start Job paused, ESXI is not runnning. Running ESXI Start Job");
                StartESXI();
                Console.WriteLine(" >> Linux Lite Job resumed");
            }

            //Console.WriteLine(" >> FAILED - SET PASSWORD IN SOURCECODE!");
            if(SendCommandSHH("192.168.1.200", "ada", "ada", @"vim-cmd vmsvc/power.on 3"))
                {
                Thread.Sleep(120000);
                if (IsLive("192.168.1.240"))
                {
                    Console.WriteLine(" >> Linux Lite Start Job has finished");
                }
                else
                {
                    Console.WriteLine(" >> Linux Lite Start Job has finished but failed");
                }
            }
            else
            {
                Console.WriteLine(" >> Linux Lite Start Job has finished but failed");
            }
        }

    }
}
