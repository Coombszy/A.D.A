using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient HostSocket = new TcpClient();
            NetworkStream DataStream;
            bool Active = true;
            while (true)
            {
                try
                {
                    HostSocket.Connect(IPAddress.Parse("127.0.0.1"), 25565);

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = 0;     // -10...10



            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-UK"));

            Choices inputs = new Choices();
            inputs.Add(new string[] { "Hello", "What are you?", "How are you?", "Good", "Bad", "Shutdown Interface", "Yes", "No", "Calculate" });
            string[] Numbers = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety", "Hundred", "Thousand" };
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(inputs);
            Choices Nums = new Choices();
            Nums.Add(Numbers);
            //gb.Append(Nums);
            Grammar G = new Grammar(gb);
            Grammar Dict = new DictationGrammar();
            Dict.Weight = 0.4f;
            recognizer.LoadGrammarAsync(G);
            recognizer.LoadGrammarAsync(Dict);
            recognizer.SetInputToDefaultAudioDevice();

            recognizer.SpeechRecognized += recognizer_SpeechRecognized;

            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            synthesizer.SpeakAsync("Interface has been successfully initialised, greetings user!");
            Console.WriteLine("Listening");
            while (Active)
            {

            }





            
            void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
            {
                Console.WriteLine("VOICERECOGNIZED= " + e.Result.Text);
                if (e.Result.Text == "Shutdown Interface")
                {
                    synthesizer.SpeakAsync("Are you sure?");
                    if (e.Result.Text == "Yes")
                    {
                        synthesizer.Speak("Shutting down interface, Goodbye");
                        Active = false;
                    }
                    else
                    {
                        synthesizer.Speak("Shutdown cancel");
                    }
                }
                else { 
                Send(e.Result.Text);
                synthesizer.SpeakAsync(ListenAndWait());
                }
            }
            void Send(string ToSend)
            {
                NetworkStream ServerStream = HostSocket.GetStream();
                string data = ToSend;
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data + "$END$");
                ServerStream.Write(outStream, 0, outStream.Length);
                ServerStream.Flush();
            }

            string ListenAndWait()
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

            string Listen()
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
            }
        }

    }
}
