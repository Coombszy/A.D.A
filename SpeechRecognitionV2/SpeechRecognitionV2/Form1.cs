using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace SpeechRecognitionV2
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-UK"));
        bool Listening = false;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices inputs = new Choices();
            inputs.Add(new string[] { "Hello", "What are you?", "How are you?", "Good", "Bad", "Shutdown Interface", "Yes", "No", "Calculate" });
            string[] Numbers = new string[] {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety", "Hundred", "Thousand" };
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(inputs);
            Choices Nums = new Choices();
            Nums.Add(Numbers);
            //gb.Append(Nums);
            Grammar G = new Grammar(gb);
            Grammar Dict = new DictationGrammar();
            Dict.Weight = 0.4f;
            recEngine.LoadGrammarAsync(G);
            recEngine.LoadGrammarAsync(Dict);
            recEngine.SetInputToDefaultAudioDevice();

            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
        }
        public void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.Text += e.Result.Text+"\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!Listening)
            {
                Listening = true;
                button1.Text = "...Listening...";
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                recEngine.RecognizeAsyncStop();
                button1.Text = "Listen";
                Listening = false;
            }
        }
    }
}
