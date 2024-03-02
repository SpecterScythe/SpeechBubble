using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;


namespace SpeechBubble
{
    public partial class Chat : Form
    {
        //string Url = "https://nightdev.com/hosted/obschat/?&channel=";
        public Chat()
        {
            InitializeComponent();
            webView21.DefaultBackgroundColor = System.Drawing.Color.Transparent;

        }

        private void Chat_Load(object sender, EventArgs e)
        {
            panel1.BringToFront();
            webView21.SendToBack();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
            webView21.BringToFront();
            string scheme = "https";
            string host = "nightdev.com";
            string channelName = Uri.EscapeDataString(ChannelNameField.Text);
            string path = $"/hosted/obschat/?&channel={channelName}";

            Uri uri = new Uri($"{scheme}://{host}{path}");
            webView21.Source = uri;
            ChannelNameField.Clear();
            webView21.SendToBack();
            ChannelNameField.Visible = false;
            label1.Visible = false;
            SaveBtn.Visible = false;
            //panel1.Hide();
        }
    }
}
