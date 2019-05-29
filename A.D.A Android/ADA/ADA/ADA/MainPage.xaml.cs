using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ADA
{
    public partial class MainPage : ContentPage
    {
        SocketHandler Client;
        public MainPage()
        {
            InitializeComponent();

            buttonSend.Clicked += testButtonClicked;

            Client = new SocketHandler();
            Client.StartSocket();

        }
        private void testButtonClicked(object sender, EventArgs e)
        {
            string data = entry.Text;
            Client.Send(data);
            var Temp = Client.Listen();
            output.Text = "---"+Temp.Response+"---";
        }
    }
}
