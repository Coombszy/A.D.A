using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Interface
{
    class Networker
    {
        IPAddress DefaultIp = IPAddress.Parse("127.0.0.1");
        int DefaultPort = 25565;
        TcpClient HostSocket = new TcpClient();
        NetworkStream DataStream;
        public void ConnectDefault()
        {
            HostSocket.Connect(DefaultIp, DefaultPort);
        }


        public void Send(string ToSend)
        {
            NetworkStream ServerStream = HostSocket.GetStream();
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
                    DataStream = HostSocket.GetStream();
                    DataStream.Read(BytesReceived, 0, BytesReceived.Length);
                    DataReceived = System.Text.Encoding.ASCII.GetString(BytesReceived);
                    DataReceived = DataReceived.Substring(0, DataReceived.IndexOf("$END$"));
                    break;
                }
                catch { }
            }
            return DataReceived;
        }
        private string Listen()
        {
            byte[] BytesReceived = new byte[2048];
            string DataReceived = null;

            while (true)
            {
                try
                {
                    DataStream = HostSocket.GetStream();
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
        }//Unused------------
    }
}
