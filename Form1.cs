using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;
using System.Net.Sockets;
using TwitchLib.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace SpeechBubble
{
    public partial class SpeechBubbleFrm : Form
    {
        private Point initialMousePosition;
        private Point initialFormPosition;
        private TcpClient tcpClient;
        private TwitchClient twitchClient;
        string MyMessage = "";
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        public int NumOfQMarkPress = 0;
        private const int OneKey = 0x31;
        private const int TwoKey = 0x32;
        private const int ThreeKey = 0x33;
        private const int FourKey = 0x34;
        private const int FiveKey = 0x35;
        private const int SixKey = 0x36;
        private const int SevenKey = 0x37;
        private const int EightKey = 0x38;
        private const int NineKey = 0x39;
        private const int ZeroKey = 0x30;
        private const int VK_CONTROL = 0x11;
        private const int VK_OEM_2 = 0xBF;
        private const int VK_NUMLOCK = 0x90;
        private const int VK_OEM_MINUS = 0xBD;
        private const int Q_Key = 0x51;
        private const int V_Key = 0x42;
        private bool OnePressed = false;
        private bool TwoPressed = false;
        private bool ThreePressed = false;
        private bool FourPressed = false;
        private bool FivePressed = false;
        private bool SixPressed = false;
        private bool SevenPressed = false;
        private bool EightPressed = false;
        private bool NinePressed = false;
        private bool ZeroPressed = false;
        private bool ctrlPressed = false;
        private bool QMarkPressed = false;
        bool Connected = false;
        public bool Capital = false;
        bool EmptyBubble = false;
        bool Speaking = false;
        private IWebDriver driver;
        SettingsScreen settingsForm = new SettingsScreen();
        private readonly SpeechSynthesizer TTs = new SpeechSynthesizer();
        string url = "https://www.text-to-speech.online/";

        public SpeechBubbleFrm()
        {
            InitializeComponent();
            if (Properties.Settings.Default.BubblePointLeft == false)
            {
                richTextBox1.Location = Properties.Settings.Default.BubbleLocation;
            }
            settingsForm.FormClosedEvent += SettingsForm_FormClosedEvent;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height - 95);
            if (Properties.Settings.Default.SpBubbleTop == true)
            {
                this.TopMost = true;
            }
            if (Properties.Settings.Default.SpBubbleTop == false)
            {
                this.TopMost = false;
            }
            richTextBox1.Font = new Font("Microsoft Sans Serif", Properties.Settings.Default.FontSize);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 88;
            timer.Tick += new EventHandler(CheckKeyState);
            timer.Start();
            InitializeChromeDriver();
            if(Properties.Settings.Default.OAuth == "oauth:")
            {
                Properties.Settings.Default.FirstStart = true;
                Properties.Settings.Default.Save();
            }
            if(Properties.Settings.Default.FirstStart == true)
            {
                MessageBox.Show("Please Enter Your Twich OAuth and Channel name In the Settings");
                settingsForm.Show();
            }
        }
        private void InitializeChromeDriver()
        {
            var options = new ChromeOptions();
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(url);
        }
        private bool IsKeyDown(int virtualKeyCode)
        {
            return ((GetAsyncKeyState(virtualKeyCode) & 0x8000) != 0);
        }
        private void CheckKeyState(object sender, EventArgs e)
        {
            if(IsKeyDown(VK_NUMLOCK))
            {
                if (Capital == true)
                {
                    Capital = false;
                }
                else if (Capital == false)
                {
                    Capital = true;
                }
            }
            if (IsKeyDown(OneKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!OnePressed)
                {
                    OnePressed = true;
                    richTextBox1.Text = Properties.Settings.Default.OneString;
                }
            }
            else
            {
                OnePressed = false;
            }
            if (IsKeyDown(TwoKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!TwoPressed)
                {
                    TwoPressed = true;
                    richTextBox1.Text = Properties.Settings.Default.TwoString;
                }
            }
            else
            {
                TwoPressed = false;
            }
            if (IsKeyDown(ThreeKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!ThreePressed)
                {
                    ThreePressed = true;
                    richTextBox1.Text = Properties.Settings.Default.ThreeString;
                }
            }
            else
            {
                ThreePressed = false;
            }
            if (IsKeyDown(FourKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!FourPressed)
                {
                    FourPressed = true;
                    richTextBox1.Text = Properties.Settings.Default.FourString;
                }
            }
            else
            {
                FourPressed = false;
            }
            if (IsKeyDown(FiveKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!FivePressed)
                {
                    FivePressed = true;
                    richTextBox1.Text = Properties.Settings.Default.FiveString;
                }
            }
            else
            {
                FivePressed = false;
            }
            if (IsKeyDown(SixKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!SixPressed)
                {
                    SixPressed = true;
                    richTextBox1.Text = Properties.Settings.Default.SixString;
                }
            }
            else
            {
                SixPressed = false;
            }
            if (IsKeyDown(SevenKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!SevenPressed)
                {
                    SevenPressed = true;
                    richTextBox1.Text = Properties.Settings.Default.SevenString;
                }
            }
            else
            {
                SevenPressed = false;
            }
            if (IsKeyDown(EightKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!EightPressed)
                {
                    EightPressed = true;
                    richTextBox1.Text = Properties.Settings.Default.EightString;
                }
            }
            else
            {
                EightPressed = false;
            }
            if (IsKeyDown(NineKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!NinePressed)
                {
                    NinePressed = true;
                    richTextBox1.Text = Properties.Settings.Default.NineString;
                }
            }
            else
            {
                NinePressed = false;
            }
            if (IsKeyDown(ZeroKey) && !IsKeyDown(VK_CONTROL) && Capital == true)
            {
                if (!ZeroPressed)
                {
                    ZeroPressed = true;
                    richTextBox1.Text = Properties.Settings.Default.ZeroString;
                }
            }
            else
            {
                ZeroPressed = false;
            }
            if(IsKeyDown(VK_CONTROL) && IsKeyDown(Q_Key))
            {
                if (IsKeyDown(VK_CONTROL) && IsKeyDown(Q_Key))
                {
                    if (TTs.State != SynthesizerState.Speaking)
                    {
                        TTs.SpeakAsync(richTextBox1.Text);
                    }
                }
            }
            if (IsKeyDown(VK_CONTROL) && IsKeyDown(VK_OEM_MINUS))
            {
                ActiveControl = null;
            }
            if (IsKeyDown(VK_CONTROL) && IsKeyDown(OneKey))
            {
                if (!ctrlPressed)
                {
                    ctrlPressed = true;
                    OnePressed = true;
                    Properties.Settings.Default.OneString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                OnePressed = false;
            }
            if (IsKeyDown(VK_CONTROL) && IsKeyDown(TwoKey))
            {
                if (!ctrlPressed)
                {
                    ctrlPressed = true;
                    TwoPressed = true;
                    Properties.Settings.Default.TwoString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                TwoPressed = false;
            }
            if (IsKeyDown(VK_CONTROL) && IsKeyDown(ThreeKey))
            {
                if (!ctrlPressed)
                {
                    ctrlPressed = true;
                    ThreePressed = true;
                    Properties.Settings.Default.ThreeString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                ThreePressed = false;
            }
            if (IsKeyDown(VK_CONTROL) && IsKeyDown(FourKey))
            {
                if (!ctrlPressed)
                {
                    ctrlPressed = true;
                    FourPressed = true;
                    Properties.Settings.Default.FourString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                FourPressed = false;
            }
            if (IsKeyDown(VK_CONTROL) && IsKeyDown(FiveKey))
            {
                if (!ctrlPressed)
                {
                    ctrlPressed = true;
                    FivePressed = true;
                    Properties.Settings.Default.FiveString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                FivePressed = false;
            }
            if (IsKeyDown(VK_CONTROL) && IsKeyDown(SixKey))
            {
                if (!ctrlPressed)
                {
                    ctrlPressed = true;
                    SixPressed = true;
                    Properties.Settings.Default.SixString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                SixPressed = false;
            }
            if(IsKeyDown(VK_CONTROL) && IsKeyDown(SevenKey))
            {
                if (!ctrlPressed)
                {
                    ctrlPressed = true;
                    SevenPressed = true;
                    Properties.Settings.Default.SevenString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                SevenPressed = false;
            }
            if(IsKeyDown(VK_CONTROL) && IsKeyDown(EightKey))
            {
                if(!ctrlPressed)
                {
                    ctrlPressed = true;
                    EightPressed = true;
                    Properties.Settings.Default.EightString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                EightPressed = false;
            }
            if(IsKeyDown(VK_CONTROL) && IsKeyDown(NineKey))
            {
                if(!ctrlPressed)
                {
                    ctrlPressed = true;
                    NinePressed = true;
                    Properties.Settings.Default.NineString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                NinePressed = false;
            }
            if(IsKeyDown(VK_CONTROL) && IsKeyDown(ZeroKey))
            {
                if (!ctrlPressed)
                {
                    ctrlPressed = true;
                    ZeroPressed = true;
                    Properties.Settings.Default.ZeroString = richTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                ctrlPressed = false;
                ZeroPressed = false;
            }
            if (IsKeyDown(VK_OEM_2) && IsKeyDown(VK_CONTROL))
            {
                if (!QMarkPressed && NumOfQMarkPress < 2)
                {
                    QMarkPressed = true;
                    SettingsScreen helpScreen = new SettingsScreen();
                    helpScreen.Show();
                    NumOfQMarkPress++;
                }
            }
            else
            {
                QMarkPressed = false;
            }
        }
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private void SettingsForm_FormClosedEvent(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SpBubbleTop == false)
            {
                this.TopMost = false;
            }
            if (Properties.Settings.Default.SpBubbleTop == true)
            {
                this.TopMost = true;
            }
            if(Properties.Settings.Default.UserFont == true)
            {
                richTextBox1.Font = new Font("Microsoft Sans Serif", Properties.Settings.Default.FontSize);
            }    
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length <= 48 && Properties.Settings.Default.UserFont == false)
            {
                richTextBox1.Font = new Font("Microsoft Sans Serif", 48);
            }
            if (richTextBox1.Text.Length >= 48 && richTextBox1.Text.Length < 52 && Properties.Settings.Default.UserFont == false)
            {
                richTextBox1.Font = new Font("Microsoft Sans Serif", 40);
            }
            if (richTextBox1.Text.Length >= 52 && richTextBox1.Text.Length < 56 && Properties.Settings.Default.UserFont == false)
            {
                richTextBox1.Font = new Font("Microsoft Sans Serif", 38);
            }
            if (richTextBox1.Text.Length >= 56 && richTextBox1.Text.Length < 58 && Properties.Settings.Default.UserFont == false)
            {
                richTextBox1.Font = new Font("Microsoft Sans Serif", 34);
            }
            if (richTextBox1.Text.Length >= 58 && Properties.Settings.Default.UserFont == false)
            {
                richTextBox1.Font = new Font("Microsoft Sans Serif", 28);
            }
            if (Properties.Settings.Default.SpeakAll == true && EmptyBubble == false)
            {
                MyMessage = richTextBox1.Text;
                QuickText_TwitchChat();
                try
                {
                    var payload = new
                    {
                        locale = Properties.Settings.Default.VoiceLocal,
                        voice = Properties.Settings.Default.VoiceType,
                        speed = 1.0,//test changing this to make sure it works
                        pitch = 1.0,//test changing thisto make sure it works
                        text = richTextBox1.Text
                    };
                    if (richTextBox1.Text != "")
                    {
                        TextToSpeech(driver, payload);
                    }
                }
                catch (Exception){ }
            }
        }
        private void TextToSpeech(IWebDriver driver, dynamic payload)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");
            IWebElement localeSelect = driver.FindElement(By.Id("locale"));
            localeSelect.SendKeys(payload.locale);

            IWebElement voiceSelect = driver.FindElement(By.Id("voice"));
            voiceSelect.SendKeys(payload.voice);

            IWebElement textArea = driver.FindElement(By.Id("text"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", textArea);
            textArea.Clear();
            textArea.SendKeys(payload.text);

            IWebElement playButton = driver.FindElement(By.Id("quick-play"));
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 730);");
            playButton.Click();
            Speaking = true;
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 730);");
        }
        private async void QuickText_TwitchChat() 
        {
            try 
            {
                string ip = "irc.chat.twitch.tv";
                int port = 6667;
                string password = Properties.Settings.Default.OAuth;
                string botUsername = "simple_Bot";
                string ChName = Properties.Settings.Default.TwitchName;
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(ip, port);

                var streamReader = new StreamReader(tcpClient.GetStream());
                var streamWriter = new StreamWriter(tcpClient.GetStream()) { NewLine = "\r\n", AutoFlush = true };
                try
                {
                    await streamWriter.WriteLineAsync($"PASS {password}");
                    await streamWriter.WriteLineAsync($"NICK {botUsername}");
                    await streamWriter.WriteLineAsync($"PRIVMSG {ChName} :{MyMessage}");
                    string message = await streamReader.ReadLineAsync();

                    if (message != null)
                    {
                        if (message.StartsWith("PING"))
                        {
                            await streamWriter.WriteLineAsync("PONG" + message.Substring(4));
                        }
                    }
                }
                catch (Exception){ }
            }
            catch(Exception) { }
        }
        private void GetTwitchChats()
        {
            if(!Connected && twitchClient == null)
            {
                string ChName = Properties.Settings.Default.TwitchName.Replace("#", "");
                string s = Properties.Settings.Default.OAuth;
                s = s.Replace("oauth:", "");
                var credentials = new ConnectionCredentials("simple_Bot_bot", s);
                twitchClient = new TwitchClient();
                twitchClient.Initialize(credentials, ChName);

                twitchClient.OnMessageReceived += OnMessageReceived;
                twitchClient.OnRaidNotification += OnRaidNotification;
                twitchClient.OnNewSubscriber += OnNewSubscriber;

                twitchClient.Connect();
                Connected = true;
            }
        }

        private void OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            var raidData = e.Subscriber;
            richTextBox1.Text = ($"User {raidData.DisplayName} Gave a subscription {raidData.SubscriptionPlanName} Thanks");
        }

        private void OnRaidNotification(object sender, OnRaidNotificationArgs e)
        {
            var raidData = e.RaidNotification;
            richTextBox1.Text = ($"Channel {raidData.MsgParamDisplayName} raided with {raidData.MsgParamViewerCount} viewers!, Thanks");
        }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            string username = e.ChatMessage.Username;
            string message = e.ChatMessage.Message;

            string usersString = Properties.Settings.Default.VerifiedUsers;
            string[] usersArray = usersString.Split('|');
            foreach(string user in usersArray)
            {
                if (e.ChatMessage.Username == user.ToLower())
                {

                    TTs.SpeakAsync($"{username}: {message}");
                    Properties.Settings.Default.RecentChatter = username;
                }
            }
            Properties.Settings.Default.AddUser = true;
        }
        private void SpeechBubbleFrm_Load(object sender, EventArgs e)
        {
            HelpScreen HelpForm = new HelpScreen();
            HelpForm.Owner = this;
            HelpForm.Show();
            if (Properties.Settings.Default.BubbleColorInt  > 7)
            {
                Properties.Settings.Default.BubbleColorInt = 1;
            }
            if (Properties.Settings.Default.BubbleColorInt == 2)
            {
                richTextBox1.BackColor = ColorTranslator.FromHtml("#3f48cc");
                richTextBox1.ForeColor = Color.Black;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubbleBlue;
                    richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubblev2Blue;
                    richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt == 3)
            {
                richTextBox1.BackColor = ColorTranslator.FromHtml("#fff200");
                richTextBox1.ForeColor = Color.DarkBlue;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubbleYellow;
                    richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubblev2Yellow;
                    richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt == 4)
            {
                richTextBox1.BackColor = ColorTranslator.FromHtml("#000000");
                richTextBox1.ForeColor = Color.White;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubbleblack;
                    richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubblev2black;
                    richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt == 5)
            {
                richTextBox1.BackColor = ColorTranslator.FromHtml("#b83dba");
                richTextBox1.ForeColor = Color.DarkMagenta;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubblePink;
                    richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubblev2Pink;
                    richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt == 6)
            {
                richTextBox1.BackColor = ColorTranslator.FromHtml("#ffffff");
                richTextBox1.ForeColor = Color.Gray;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubbleWhite;
                    richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubblev2White;
                    richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt >= 7)
            {
                richTextBox1.BackColor = ColorTranslator.FromHtml("#422d51");
                richTextBox1.ForeColor = Color.FromArgb(192, 255, 255);
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubble;
                    richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    pictureBox1.Image = Properties.Resources.SpeechBubblev2;
                    richTextBox1.Location = new Point(81, 12);
                    Properties.Settings.Default.BubbleColorInt = 1;
                    Properties.Settings.Default.Save();
                }
            }
            ActiveControl = null;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.FirstStart == false)
            {
                timer2.Interval = 8200;
                GetTwitchChats();
                EmptyBubble = true;
                richTextBox1.Clear();
                EmptyBubble = false;
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.FirstStart == false)
            {
                IWebElement downloadingElement = driver.FindElement(By.Id("download"));
                if (downloadingElement.Displayed && downloadingElement.Enabled)
                {
                    Speaking = false;
                }
                timer3.Interval = 300;
                if (Speaking == true)
                {
                    timer2.Stop();
                    timer2.Interval = 6000;
                }
                if (Speaking == false)
                {
                    timer2.Start();
                }
            }
        }

        private void SpeechBubbleFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            driver?.Quit();
        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point currentMousePosition = Cursor.Position;
                int offsetX = currentMousePosition.X - initialMousePosition.X;
                int offsetY = currentMousePosition.Y - initialMousePosition.Y;
                int newFormX = initialFormPosition.X + offsetX;
                int newFormY = initialFormPosition.Y + offsetY;
                this.Location = new Point(newFormX, newFormY);
            }
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                initialMousePosition = Cursor.Position;
                initialFormPosition = this.Location;
            }
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                ActiveControl = null;
            }
        }
    }
}