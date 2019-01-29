using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace SocketHandler
{
    class ClientHandler
    {
        public TcpClient ClientSocket;
        public NetworkStream DataStream;
        
        public ClientHandler(TcpClient MySocket)
        {
            //per client instancing - Put ai structure connection here!
            this.ClientSocket = MySocket;

            //-----------------------------
            MainLoop();
        }
        public void MainLoop()
        {
            string Received;
            while (true)
            {
                Received = Listen();
                Received = "BOUNCEBACK : " + Received;
                Send(Received);
            }
        }

        //Basic Functions for Socket Interaction
        public void Dispose()
        {
            ClientSocket.Close();
            ClientSocket = null;
            Console.WriteLine("CLIENTSOCKETCLOSED");
        }
        private void Disconnect()
        {
            Console.WriteLine("Disconnecting now!");
            Send("/DISCONNECT");
        }
        private void Send(string ToSend)
        {
            try
            {
                NetworkStream SendStream = ClientSocket.GetStream();
                string data = ToSend;
                Console.WriteLine("ISent=" + data);
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data + "$");
                SendStream.Write(outStream, 0, outStream.Length);
                SendStream.Flush();
            }
            catch { }
        }
        private string ListenOnInstant()
        {
            byte[] BytesReceived = new byte[1280];
            string DataReceived = null;
            while (true)
            {
                try
                {
                    DataStream = ClientSocket.GetStream();
                    if (DataStream.DataAvailable)
                    {
                        DataStream.Read(BytesReceived, 0, BytesReceived.Length);
                        DataReceived = System.Text.Encoding.ASCII.GetString(BytesReceived);
                        DataReceived = DataReceived.Substring(0, DataReceived.IndexOf("$"));
                    }
                    else { DataReceived = ""; }
                    DataStream.Flush();
                    break;
                }
                catch { break; }
            }
            //Method for checking if a client is still connected
            if (DataReceived == "/AREYOUTHERE")
            {
                Send("/IMHERE");
                DataReceived = "";
            }
            return DataReceived;
        }
        private string Listen()
        {
            byte[] BytesReceived = new byte[1280];
            string DataReceived = null;
            while (true)
            {
                try
                {
                    DataStream = ClientSocket.GetStream();
                    DataStream.Read(BytesReceived, 0, BytesReceived.Length);
                    DataReceived = System.Text.Encoding.ASCII.GetString(BytesReceived);
                    DataReceived = DataReceived.Substring(0, DataReceived.IndexOf("$"));
                    DataStream.Flush();
                    break;
                }
                catch { break; }
            }
            //Method for checking if a client is still connected
            if (DataReceived == "/AREYOUTHERE")
            {
                Send("/IMHERE");
                DataReceived = "";
            }
            return DataReceived;
        }
    }
}
