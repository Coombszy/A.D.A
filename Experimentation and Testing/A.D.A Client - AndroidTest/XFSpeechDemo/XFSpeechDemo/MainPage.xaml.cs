using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XFSpeechDemo
{
    public partial class MainPage : ContentPage
    {
        private ISpeechToText _speechRecongnitionInstance;
        SocketHandler Client;

        public MainPage()
        {
            InitializeComponent();
            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch(Exception ex)
            {
                recon.Text = ex.Message;
            }
            connect.Clicked += connectButtonClicked;



            MessagingCenter.Subscribe<ISpeechToText, IList<string>>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) =>
            {
                start.IsEnabled = true;
            });

            MessagingCenter.Subscribe<IMessageSender, IList<string>>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });
            
        }

        private void SpeechToTextFinalResultRecieved(IList<string> args)
        {
            string dataAll = "";
            //bool matched = false;
            foreach(string data in args)
            {
                dataAll += data + ", ";
            }
            recon.Text = ">>>>"+dataAll+"<<<<";

            Client.Send(args[0].ToLower());
            var Temp = Client.Listen();
            output.Text = ":" + Temp.Response;

        }

        private void connectButtonClicked(object sender, EventArgs e)
        {
            //Set up Socket Connection
            Client = new SocketHandler();
            if (Client.StartSocket())
            { 
                output.Text = "CONNECTED!";
            }
            else
            {
                output.Text = "FAILED TO CONNECT!";
            }
        }

        private void Start_Clicked(object sender, EventArgs e)
        {
            try
            {
                _speechRecongnitionInstance.StartSpeechToText();
            }
            catch(Exception ex)
            {
                recon.Text = ex.Message;
            }
            
            if (Device.RuntimePlatform == Device.iOS)
            {
                start.IsEnabled = false;
            }

            

        }

        
    }
}
