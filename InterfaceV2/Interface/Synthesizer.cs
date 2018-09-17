using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Drawing;
using System.Windows.Forms;

namespace Interface
{
    class Synthesizer
    {
        SpeechSynthesizer MySynthesizer;
        Button ControllerButton;
        public Synthesizer(ref object btn)
        {
            MySynthesizer = new SpeechSynthesizer();
            LoadConfig();
            ControllerButton = (Button)btn;
        }
        public Synthesizer(ref Button btn)
        {
            MySynthesizer = new SpeechSynthesizer();
            LoadConfig();
            ControllerButton = btn;
        }
        private void LoadConfig()
        {
            MySynthesizer.Volume = 70;  // 0...100
            MySynthesizer.Rate = 0;     // -10...10
        }
        public void Say(string TTS)
        {
            //ControllerButton.BackColor = Color.Cyan;
            MySynthesizer.Speak(TTS);
            //ControllerButton.BackColor = Color.WhiteSmoke;
        }
        public void SayAync(string TTS)
        {
            //ControllerButton.BackColor = Color.Cyan;
            MySynthesizer.SpeakAsync(TTS);
            //ControllerButton.BackColor = Color.WhiteSmoke;
        }
    }
}
