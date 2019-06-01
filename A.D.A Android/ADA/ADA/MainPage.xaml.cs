using Android;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Speech;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Java.Interop;

namespace ADA
{
    public partial class MainPage : ContentPage
    {
        SocketHandler Client;
        public MainPage()
        {
            InitializeComponent();

            buttonSend.Clicked += testButtonClicked;


            //Set up Socket Connection
            Client = new SocketHandler();
            try
            {
                Client.StartSocket();
            }
            catch (Exception e)
            {
               output.Text = e.ToString();
            }

        }
        private void testButtonClicked(object sender, EventArgs e)
        {
            string data = entry.Text;
            Client.Send(data);
            var Temp = Client.Listen();
            output.Text = ":"+Temp.Response;
        }

    }
}
