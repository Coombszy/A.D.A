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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Interface
{
    public partial class Interface : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-GB"));
        bool Listening = false;
        bool Connected = false;
        Synthesizer I_Voice;
        Networker Host;
        string HostData;
        string Command;
        public Interface()
        {
            InitializeComponent();
            int id = 0;     // The id of the hotkey. 
            RegisterHotKey(this.Handle, id, (int)KeyModifier.Alt, Keys.Down.GetHashCode());       // Register Shift + A as global hotkey. 
        }

        private void Interface_Load(object sender, EventArgs e)
        {
            btn.BackColor = Color.WhiteSmoke;
            Host = new Networker();
            Choices inputs = new Choices();
            inputs.Add(new string[] { "Hello", "What are you?", "How are you?", "Good", "Bad", "Shutdown interface", "Yes", "No", "Calculate", "Goto a youtube channel", "Open google" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(inputs);
            Grammar G = new Grammar(gb);
            Choices FakeWords = new Choices();
            FakeWords.Add(new string[] {"The yogscast", "Hat films" });
            GrammarBuilder fb = new GrammarBuilder();
            fb.Append(FakeWords);
            Grammar F = new Grammar(fb);
            F.Weight = 0.45f;
            Grammar Dict = new DictationGrammar();
            Dict.Weight = 0.3f;
            recEngine.LoadGrammarAsync(G);
            recEngine.LoadGrammarAsync(Dict);
            recEngine.LoadGrammarAsync(F);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
        }
        public void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Host.Send(e.Result.Text);
            recEngine.RecognizeAsyncStop();
            btn.Text = "Listen";
            Listening = false;
            HostData = Host.ListenAndWait();
            if (HostData.Contains("$COMMAND$"))
            {
                I_Voice.Say(HostData.Substring(0, HostData.IndexOf("$COMMAND$")));
                Command = HostData.Remove(0, HostData.IndexOf("$COMMAND$")+9);
                //Variable containing commands
                if (Command.Contains("YOUTUBE:"))
                {
                    Process.Start("www.youtube.co.uk/user/" + Command.Remove(0, Command.IndexOf("YOUTUBE:") + 8));
                }
                else
                {
                    //Normal Commands
                    switch (Command)
                    {
                        case "SHUTDOWN":
                            this.Close();
                            break;
                        case "GOOGLE":
                            Process.Start("www.google.co.uk");
                            break;
                    }
                }
            }
            else
            {
                I_Voice.Say(HostData);
            }
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btn.Text = "...Listening...";
            Listening = true;

        }

        private void btn_Click(object sender, EventArgs e)
        {
            btn.BackColor = Color.WhiteSmoke;
            if (!Connected)
            {
                try
                {
                    Host.ConnectDefault();
                    Connected = true;
                    I_Voice = new Synthesizer(ref sender);
                    btn.Text = "";
                    I_Voice.Say(Host.ListenAndWait());
                    btn.Text = "Listen";
                }
                catch
                {
                    btn.BackColor = Color.Red;
                }
            }
            else if (!Listening)
            {
                Listening = true;
                btn.Text = "...Listening...";
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                recEngine.RecognizeAsyncStop();
                btn.Text = "Listen";
                Listening = false;
            }
        }
        private void HotkeyPress()
        {
            btn.BackColor = Color.WhiteSmoke;
            if (!Connected)
            {
                try
                {
                    Host.ConnectDefault();
                    Connected = true;
                    I_Voice = new Synthesizer(ref btn);
                    btn.Text = "";
                    I_Voice.Say(Host.ListenAndWait());
                    btn.Text = "Listen";
                }
                catch
                {
                    btn.BackColor = Color.Red;
                }
            }
            else if (!Listening)
            {
                Listening = true;
                btn.Text = "...Listening...";
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                recEngine.RecognizeAsyncStop();
                btn.Text = "Listen";
                Listening = false;
            }
        }



        //HotkeyDetection
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                /* Note that the three lines below are not needed if you only want to register one hotkey.
                 * The below lines are useful in case you want to register multiple keys, which you can use a switch with the id as argument, or if you want to know which key/modifier was pressed for some particular reason. */

                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.


                HotkeyPress();
                // do something
            }
        }
        private void ExampleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, 0);       // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.
        }
    }
}
