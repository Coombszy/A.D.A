using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Threading;

namespace SpeechRecognition
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

            Choices inputs = new Choices();
            inputs.Add(new string[] { "hello", "goodbye", "my name is" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(inputs);
            Grammar G = new Grammar(gb);
            recognizer.LoadGrammarAsync(G);
            recognizer.SetInputToDefaultAudioDevice();

            recognizer.SpeechRecognized += recognizer_SpeechRecognized;

            recognizer.RecognizeAsync(RecognizeMode.Multiple);

            while(true)
            {

            }

            void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
            {
                Console.WriteLine("RESULT= "+ e.Result.Text);
    
            }
        }
    }
}
