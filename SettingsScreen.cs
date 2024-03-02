using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpeechBubble
{
    public partial class SettingsScreen : Form
    {
        public SettingsScreen()
        {
            InitializeComponent();
        }
        public event EventHandler FormClosedEvent;
        private void HTopmBtn_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.HlpScreenTop == true)
            {
                Properties.Settings.Default.HlpScreenTop = false;
                HTopmBtn.BackColor = Color.Red;
                Properties.Settings.Default.Save();
            }
            else if(Properties.Settings.Default.HlpScreenTop == false)
            {
                Properties.Settings.Default.HlpScreenTop = true;
                HTopmBtn.BackColor = Color.Lime;
                Properties.Settings.Default.Save();
            }
        }
        private void STopMBtn_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SpBubbleTop == true)
            {
                Properties.Settings.Default.SpBubbleTop = false;
                STopMBtn.BackColor = Color.Red;
                Properties.Settings.Default.Save();
            }
            else if (Properties.Settings.Default.SpBubbleTop == false)
            {
                Properties.Settings.Default.SpBubbleTop = true;
                STopMBtn.BackColor = Color.Lime;
                Properties.Settings.Default.Save();
            }
        }

        private void FontSaveBtn_Click(object sender, EventArgs e)
        {
            int FS = Convert.ToInt32(textBox1.Text);
            Properties.Settings.Default.FontSize = FS;
            Properties.Settings.Default.UserFont = true;
            Properties.Settings.Default.Save();
        }

        private void SettingsScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            FormClosedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void AutoSpkTxtBtn_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.SpeakAll == false)
            {
                Properties.Settings.Default.SpeakAll = true;
                AutoSpkTxtBtn.BackColor = Color.Lime;
                Properties.Settings.Default.Save();
            }
            else if (Properties.Settings.Default.SpeakAll == true)
            {
                Properties.Settings.Default.SpeakAll = false;
                AutoSpkTxtBtn.BackColor = Color.Red;
                Properties.Settings.Default.Save();
            }
        }

        private void SettingsScreen_Load(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.SpeakAll == false)
            {
                AutoSpkTxtBtn.BackColor = Color.Red;
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.SpeakAll == true)
            {
                AutoSpkTxtBtn.BackColor = Color.Lime;
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.SpBubbleTop == true)
            {
                STopMBtn.BackColor = Color.Lime;
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.SpBubbleTop == false)
            {
                STopMBtn.BackColor = Color.Red;
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.HlpScreenTop == true)
            {
                HTopmBtn.BackColor = Color.Lime;
                Properties.Settings.Default.Save();
            }
            else if (Properties.Settings.Default.HlpScreenTop == false)
            {
                HTopmBtn.BackColor = Color.Red;
                Properties.Settings.Default.Save();
            }
            string usersString = Properties.Settings.Default.VerifiedUsers;
            string[] usersArray = usersString.Split('|');
            foreach (string user in usersArray)
            {
                TTsUsersList.Text += user + " ";
            }
            if(Properties.Settings.Default.Darkmode == true)
            {
                this.BackColor = Color.DimGray;
                DarkModeBtn.BackColor = Color.Lime;
                TTsUsersList.BackColor = Color.Black;
                TTsUsersList.ForeColor = Color.LightGray;
                RemoveUserTxt.BackColor = Color.Black;
                RemoveUserTxt.ForeColor = Color.LightGray;
                SaveUserTxt.BackColor = Color.Black;
                SaveUserTxt.ForeColor = Color.LightGray;
                RemoveTTsBtn.BackColor = Color.Black;
                RemoveTTsBtn.ForeColor = Color.LightGray;
                SaveTTsbtn.BackColor = Color.Black;
                SaveTTsbtn.ForeColor = Color.LightGray;
            }
        }

        private void SaveTTsbtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.VerifiedUsers += SaveUserTxt.Text + "|";
            Properties.Settings.Default.Save();
        }

        private void RemoveTTsBtn_Click(object sender, EventArgs e)
        {
            string s = RemoveUserTxt.Text;
            string original = Properties.Settings.Default.VerifiedUsers;
            string remove = original.Replace(s, "");
            int lastIndex = remove.LastIndexOf("|");
            if (lastIndex != -1)
            {
                string resultString = remove.Remove(lastIndex, 1);
                Properties.Settings.Default.VerifiedUsers = resultString;
            }
            else
            {
                Properties.Settings.Default.VerifiedUsers = remove;
            }
        }

        public void DarkModeBtn_Click(object sender, EventArgs e)
        {
            HelpScreen HelpMenu = Application.OpenForms.OfType<HelpScreen>().FirstOrDefault();
            if(Properties.Settings.Default.Darkmode == true)
            {
                Properties.Settings.Default.Darkmode = false;
                Properties.Settings.Default.Save();
                DarkModeBtn.BackColor = Color.Red;
                TTsUsersList.BackColor = default;
                TTsUsersList.ForeColor = default;
                RemoveUserTxt.BackColor = default;
                RemoveUserTxt.ForeColor = default;
                SaveUserTxt.BackColor = default;
                SaveUserTxt.ForeColor = default;
                RemoveTTsBtn.BackColor = default;
                RemoveTTsBtn.ForeColor = default;
                SaveTTsbtn.BackColor = default;
                SaveTTsbtn.ForeColor = default;
                this.BackColor = default;
                if (HelpMenu != null)
                {
                    foreach (var control in HelpMenu.Controls)
                    {
                        if (control is RichTextBox richTextBox)
                        {
                            richTextBox.BackColor = default;
                            richTextBox.ForeColor = default;
                        }
                        else if (control is TextBox textBox)
                        {
                            textBox.BackColor = default;
                            textBox.ForeColor = default;
                        }
                        else if (control is Button button)
                        {
                            button.BackColor = default;
                            button.ForeColor = default;
                        }
                    }
                }
            }
            else if (Properties.Settings.Default.Darkmode == false)
            {
                Properties.Settings.Default.Darkmode = true;
                Properties.Settings.Default.Save();
                this.BackColor = Color.DimGray;
                DarkModeBtn.BackColor = Color.Lime;
                TTsUsersList.BackColor = Color.Black;
                TTsUsersList.ForeColor = Color.LightGray;
                RemoveUserTxt.BackColor = Color.Black;
                RemoveUserTxt.ForeColor = Color.LightGray;
                SaveUserTxt.BackColor = Color.Black;
                SaveUserTxt.ForeColor = Color.LightGray;
                RemoveTTsBtn.BackColor = Color.Black;
                RemoveTTsBtn.ForeColor = Color.LightGray;
                SaveTTsbtn.BackColor = Color.Black;
                SaveTTsbtn.ForeColor = Color.LightGray;
                if (HelpMenu != null)
                {
                    foreach (var control in HelpMenu.Controls)
                    {
                        if (control is RichTextBox richTextBox)
                        {
                            richTextBox.BackColor = Color.Black;
                            richTextBox.ForeColor = Color.LightGray;
                        }
                        else if (control is TextBox textBox)
                        {
                            textBox.BackColor = Color.Black;
                            textBox.ForeColor = Color.LightGray;
                        }
                        else if (control is Button button)
                        {
                            button.BackColor = Color.Black;
                            button.ForeColor = Color.LightGray;
                        }
                    }
                }
            }
        }
        private void ChangeBubblepointBtn_Click(object sender, EventArgs e)
        {
            SpeechBubbleFrm Bubble = Application.OpenForms.OfType<SpeechBubbleFrm>().FirstOrDefault();
            if (Bubble != null)
            {
                if(Properties.Settings.Default.BubblePointLeft == false)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 1)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#422d51");
                        Bubble.richTextBox1.ForeColor = Color.FromArgb(192, 255, 255);
                        Properties.Settings.Default.BubblePointLeft = true;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2;
                        ChangeBubblepointBtn.Text = "Bubble Point = Left";
                        Bubble.richTextBox1.Location = new Point(81, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(81, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                else if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 1)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#422d51");
                        Bubble.richTextBox1.ForeColor = Color.FromArgb(192, 255, 255);
                        Properties.Settings.Default.BubblePointLeft = false;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubble;
                        ChangeBubblepointBtn.Text = "Bubble Point = Right";
                        Bubble.richTextBox1.Location = new Point(12, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(12, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 2)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#3f48cc");
                        Bubble.richTextBox1.ForeColor = Color.Black;
                        Properties.Settings.Default.BubblePointLeft = true;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2Blue;
                        ChangeBubblepointBtn.Text = "Bubble Point = Left";
                        Bubble.richTextBox1.Location = new Point(81, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(81, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                else if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 2)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#3f48cc");
                        Bubble.richTextBox1.ForeColor = Color.Black;
                        Properties.Settings.Default.BubblePointLeft = false;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubbleBlue;
                        ChangeBubblepointBtn.Text = "Bubble Point = Right";
                        Bubble.richTextBox1.Location = new Point(12, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(12, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 3)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#fff200");
                        Bubble.richTextBox1.ForeColor = Color.DarkBlue;
                        Properties.Settings.Default.BubblePointLeft = true;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2Yellow;
                        ChangeBubblepointBtn.Text = "Bubble Point = Left";
                        Bubble.richTextBox1.Location = new Point(81, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(81, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                else if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 3)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#fff200");
                        Bubble.richTextBox1.ForeColor = Color.DarkBlue;
                        Properties.Settings.Default.BubblePointLeft = false;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubbleYellow;
                        ChangeBubblepointBtn.Text = "Bubble Point = Right";
                        Bubble.richTextBox1.Location = new Point(12, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(12, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 4)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#000000");
                        Bubble.richTextBox1.ForeColor = Color.White;
                        Properties.Settings.Default.BubblePointLeft = true;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2black;
                        ChangeBubblepointBtn.Text = "Bubble Point = Left";
                        Bubble.richTextBox1.Location = new Point(81, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(81, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                else if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 4)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#000000");
                        Bubble.richTextBox1.ForeColor = Color.White;
                        Properties.Settings.Default.BubblePointLeft = false;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubbleblack;
                        ChangeBubblepointBtn.Text = "Bubble Point = Right";
                        Bubble.richTextBox1.Location = new Point(12, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(12, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 5)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#b83dba");
                        Bubble.richTextBox1.ForeColor = Color.DarkMagenta;
                        Properties.Settings.Default.BubblePointLeft = true;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2Pink;
                        ChangeBubblepointBtn.Text = "Bubble Point = Left";
                        Bubble.richTextBox1.Location = new Point(81, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(81, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                else if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 5)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#b83dba");
                        Bubble.richTextBox1.ForeColor = Color.DarkMagenta;
                        Properties.Settings.Default.BubblePointLeft = false;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblePink;
                        ChangeBubblepointBtn.Text = "Bubble Point = Right";
                        Bubble.richTextBox1.Location = new Point(12, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(12, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 6)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#ffffff");
                        Bubble.richTextBox1.ForeColor = Color.Gray;
                        Properties.Settings.Default.BubblePointLeft = true;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2White;
                        ChangeBubblepointBtn.Text = "Bubble Point = Left";
                        Bubble.richTextBox1.Location = new Point(81, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(81, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                else if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 6)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#ffffff");
                        Bubble.richTextBox1.ForeColor = Color.Gray;
                        Properties.Settings.Default.BubblePointLeft = false;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubbleWhite;
                        ChangeBubblepointBtn.Text = "Bubble Point = Right";
                        Bubble.richTextBox1.Location = new Point(12, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(12, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 7)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#422d51");
                        Bubble.richTextBox1.ForeColor = Color.FromArgb(192, 255, 255);
                        Properties.Settings.Default.BubblePointLeft = true;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2;
                        ChangeBubblepointBtn.Text = "Bubble Point = Left";
                        Bubble.richTextBox1.Location = new Point(81, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(81, 12);
                        Properties.Settings.Default.Save();
                    }
                }
                else if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    if (Properties.Settings.Default.BubbleColorInt == 7)
                    {
                        Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#422d51");
                        Bubble.richTextBox1.ForeColor = Color.FromArgb(192, 255, 255);
                        Properties.Settings.Default.BubblePointLeft = false;
                        Bubble.pictureBox1.Image = Properties.Resources.SpeechBubble;
                        ChangeBubblepointBtn.Text = "Bubble Point = Right";
                        Bubble.richTextBox1.Location = new Point(12, 12);
                        Properties.Settings.Default.BubbleLocation = new Point(12, 12);
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }

        private void BubbleColorChangeBtn_Click(object sender, EventArgs e)
        {
            SpeechBubbleFrm Bubble = Application.OpenForms.OfType<SpeechBubbleFrm>().FirstOrDefault();
            Properties.Settings.Default.BubbleColorInt++;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.BubbleColorInt == 2)
            {
                Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#3f48cc");
                Bubble.richTextBox1.ForeColor = Color.Black;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubbleBlue;
                    Bubble.richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2Blue;
                    Bubble.richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt == 3)
            {
                Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#fff200");
                Bubble.richTextBox1.ForeColor = Color.DarkBlue;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubbleYellow;
                    Bubble.richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2Yellow;
                    Bubble.richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt == 4)
            {
                Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#000000");
                Bubble.richTextBox1.ForeColor = Color.White;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubbleblack;
                    Bubble.richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2black;
                    Bubble.richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt == 5)
            {
                Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#b83dba");
                Bubble.richTextBox1.ForeColor = Color.DarkMagenta;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblePink;
                    Bubble.richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2Pink;
                    Bubble.richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt == 6)
            {
                Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#ffffff");
                Bubble.richTextBox1.ForeColor = Color.Gray;
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubbleWhite;
                    Bubble.richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2White;
                    Bubble.richTextBox1.Location = new Point(81, 12);
                }
            }
            if (Properties.Settings.Default.BubbleColorInt == 7)
            {
                Bubble.richTextBox1.BackColor = ColorTranslator.FromHtml("#422d51");
                Bubble.richTextBox1.ForeColor = Color.FromArgb(192, 255, 255);
                if (Properties.Settings.Default.BubblePointLeft == true)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubble;
                    Bubble.richTextBox1.Location = new Point(12, 12);
                }
                if (Properties.Settings.Default.BubblePointLeft == false)
                {
                    Bubble.pictureBox1.Image = Properties.Resources.SpeechBubblev2;
                    Bubble.richTextBox1.Location = new Point(81, 12);
                    Properties.Settings.Default.BubbleColorInt = 1;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void SaveOauthBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.TwitchName = "#"+ChannelnameTxt.Text;
            Properties.Settings.Default.OAuth = "oauth:"+OauthTxt.Text;
            OauthTxt.Clear();
            ChannelnameTxt.Clear();
            Properties.Settings.Default.FirstStart = false;
            Properties.Settings.Default.Save();
            string usersString = Properties.Settings.Default.VerifiedUsers;
            string[] usersArray = usersString.Split('|');
            foreach (string user in usersArray)
            {
                TTsUsersList.Text += user + " ";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OauthTxt.Text = Properties.Settings.Default.OAuth;
            ChannelnameTxt.Text = Properties.Settings.Default.TwitchName;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                Properties.Settings.Default.VoiceType = "SteffanNeural(Male)";
                Properties.Settings.Default.VoiceLocal = "English (United States) [en-US]";
                Properties.Settings.Default.Save();
            }
            if (listBox1.SelectedIndex == 1)
            {
                Properties.Settings.Default.VoiceType = "LiamNeural(Male)";
                Properties.Settings.Default.VoiceLocal = "English (Canada) [en-CA]";
                Properties.Settings.Default.Save();
            }
            if (listBox1.SelectedIndex == 2)
            {
                Properties.Settings.Default.VoiceType = "ChristopherNeural(Male)";
                Properties.Settings.Default.VoiceLocal = "English (United States) [en-US]";
                Properties.Settings.Default.Save();
            }
            if (listBox1.SelectedIndex == 3)
            {
                Properties.Settings.Default.VoiceType = "KeitaNeural(Male)";
                Properties.Settings.Default.VoiceLocal = "Japanese (Japan) [ja-JP]";
                Properties.Settings.Default.Save();
            }
            if (listBox1.SelectedIndex == 4)
            {
                Properties.Settings.Default.VoiceType = "AriaNeural(Female)";
                Properties.Settings.Default.VoiceLocal = "English (United States) [en-US]";
                Properties.Settings.Default.Save();
            }
            if (listBox1.SelectedIndex == 5)
            {
                Properties.Settings.Default.VoiceType = "SoniaNeural(Female)";
                Properties.Settings.Default.VoiceLocal = "English (United Kingdom) [en-GB]";
                Properties.Settings.Default.Save();
            }
            if (listBox1.SelectedIndex == 6)
            {
                Properties.Settings.Default.VoiceType = "SteffanNeural(Male)";
                Properties.Settings.Default.VoiceLocal = "Japanese (Japan) [ja-JP]";
                Properties.Settings.Default.Save();
            }
        }
    }
}
