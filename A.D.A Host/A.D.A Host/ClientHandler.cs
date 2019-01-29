using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Threading;

namespace A.D.A_Host
{
    public struct ActiveNodeData
    {
        public string Command, Response;
        public List<string> Dictionary;
        public ActiveNodeData(string Command_, string Response_, List<string> Dictionary_)
        {
            Command = Command_;
            Response = Response_;
            Dictionary = Dictionary_;
        }
    }
    class ClientHandler
    {
        public TcpClient ClientSocket;
        public NetworkStream DataStream;
        private MemoryHandler MemoryUnit;
        private bool Connected;
        
        public ClientHandler(TcpClient MySocket, ref MemoryHandler MainMemoryUnit)
        {
            //per client instancing - Put ai structure connection here!
            this.ClientSocket = MySocket;
            this.MemoryUnit = MainMemoryUnit;
            //-----------------------------
            Console.WriteLine(" >> Client Successfully connected!");
            Connected = true;
            MainLoop();
        }
        public void MainLoop()
        {
            string Received;
            while (Connected)
            {
                Received = Listen();
                if (ClientSocket.Connected == false)
                {
                    Connected = false;
                    break;
                }
                var temp = MemoryUnit.Response(Received);
                SendActiveNodeData(new ActiveNodeData(temp.Command, temp.Response, temp.Dictionary));

                //Response = "A.D.A ~ " + MemoryUnit.Response(Received);
                //Send(Response);
            }
            Console.WriteLine(" >> A Client has Disconnected");
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
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data); //+ "$");
                SendStream.Write(outStream, 0, outStream.Length);
                SendStream.Flush();
            }
            catch { }
        }
        private void SendActiveNodeData(ActiveNodeData ToSend)
        {
            //try
            //{
                NetworkStream SendStream = ClientSocket.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(Serialize(ToSend)); //+ "$");
                //Console.WriteLine("ISent=" + ToSend.Command);
                SendStream.Write(outStream, 0, outStream.Length);
                SendStream.Flush();
            /*}
            catch(Exception e)
            {
                Console.WriteLine("FAILED TO SEND:");
                Console.WriteLine(e.ToString());
            }*/
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

        //Serializing Objects
        private object Deserialize(string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(object));
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                return (object)xmlSerializer.Deserialize(textReader);
            }
        }
        private string Serialize(object toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ActiveNodeData));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
    }
}
