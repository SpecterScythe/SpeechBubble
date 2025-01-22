using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpeechBubble
{//something happens so that this form opens twice
    public partial class HelpScreen : Form
    {
        private Point initialMousePosition;
        private Point initialFormPosition;
        int loop = 1;
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        private const int VK_CONTROL = 0x11;
        private const int G_KEY = 0x47;
        bool mini = false;
        bool Gamemode = false;
        public HelpScreen()
        {
            InitializeComponent();
            this.TopLevel = true;
            SettingsScreen settingsForm = new SettingsScreen();
            settingsForm.FormClosedEvent += SettingsForm_FormClosedEvent;
            timer1.Enabled = true;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
            if (Properties.Settings.Default.HlpScreenTop == false)
            {
                this.TopMost = false;
            }
            if (Properties.Settings.Default.HlpScreenTop == true)
            {
                this.BringToFront();
                this.TopMost = true;
            }
        }
        private void UpdateRichTextBoxTextThreadSafe(RichTextBox richTextBox, string newText)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke(new Action<RichTextBox, string>(UpdateRichTextBoxTextThreadSafe), richTextBox, newText);
            }
            else
            {
                richTextBox.Text = newText;
            }
        }

        private void UpdateAllRichTextBoxesThreadSafe()
        {
            UpdateRichTextBoxTextThreadSafe(richTextBox1, Properties.Settings.Default.OneString);
            UpdateRichTextBoxTextThreadSafe(richTextBox2, Properties.Settings.Default.TwoString);
            UpdateRichTextBoxTextThreadSafe(richTextBox3, Properties.Settings.Default.ThreeString);
            UpdateRichTextBoxTextThreadSafe(richTextBox4, Properties.Settings.Default.FourString);
            UpdateRichTextBoxTextThreadSafe(richTextBox5, Properties.Settings.Default.FiveString);
            UpdateRichTextBoxTextThreadSafe(richTextBox6, Properties.Settings.Default.SixString);
            UpdateRichTextBoxTextThreadSafe(richTextBox7, Properties.Settings.Default.SevenString);
            UpdateRichTextBoxTextThreadSafe(richTextBox8, Properties.Settings.Default.EightString);
            UpdateRichTextBoxTextThreadSafe(richTextBox9, Properties.Settings.Default.NineString);
            UpdateRichTextBoxTextThreadSafe(richTextBox10, Properties.Settings.Default.ZeroString);
        }
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
        }
        private bool IsKeyDown(int virtualKeyCode)
        {
            return ((GetAsyncKeyState(virtualKeyCode) & 0x8000) != 0);
        }
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(mini == false)
            {
                UpdateAllRichTextBoxesThreadSafe();
            }
            if (IsKeyDown(G_KEY) && IsKeyDown(VK_CONTROL))
            {
                if(Gamemode == false)
                {
                    this.Height = 45;
                    this.Width = 70;
                    Rectangle workingArea = Screen.GetWorkingArea(this);
                    this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - richTextBox11.Size.Height);
                    this.Enabled = false;
                    this.BackColor = Color.WhiteSmoke;
                    this.TransparencyKey = Color.WhiteSmoke;
                    Gamemode = true;
                }
                else if(Gamemode == true)
                {
                    if(mini == true)
                    {
                        this.Height = 45;
                        this.Width = 771;
                    }
                    if(mini == false)
                    {
                        this.Height = 182;
                        this.Width = 958;
                    }
                    Rectangle workingArea = Screen.GetWorkingArea(this);
                    this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
                    this.Enabled = true;
                    this.BackColor = Color.Gray;
                    Gamemode = false;
                }
            }
            if(this.Visible == false)
            {
                this.BringToFront();
            }
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
            }
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            SettingsScreen settingsScreen = new SettingsScreen();
            settingsScreen.Show();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            SpeechBubbleFrm form1 = Application.OpenForms.OfType<SpeechBubbleFrm>().FirstOrDefault();
            if (form1 != null)
            {
                form1.NumOfQMarkPress = 0;
            }
            Properties.Settings.Default.Save();
            this.Close();
        }

        public void richTextBox11_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Properties.Settings.Default.QuickText = richTextBox11.Text;
                Properties.Settings.Default.Save();
                richTextBox11.Clear();
                string textFromForm2 = Properties.Settings.Default.QuickText;
                SpeechBubbleFrm form1 = Application.OpenForms.OfType<SpeechBubbleFrm>().FirstOrDefault();
                if (form1 != null)
                {
                    form1.richTextBox1.Text = textFromForm2;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(loop == 1)
            {
                this.Height = 45;
                this.Width = 771;
                Rectangle workingArea = Screen.GetWorkingArea(this);
                this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
                button2.Text = "+";
                loop++;
                mini = true;
            }
            else
            {
                this.Height = 182;
                this.Width = 958;
                Rectangle workingArea = Screen.GetWorkingArea(this);
                this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
                button2.Text = "-";
                loop--;
                mini = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Chat chat = new Chat();
            chat.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                Properties.Settings.Default.VerifiedUsers += Properties.Settings.Default.RecentChatter + "|";
            }
            if (Properties.Settings.Default.AddUser == true)
            {
                Properties.Settings.Default.VerifiedUsers += textBox1.Text + "|";
            }
        }

        private void HelpScreen_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Darkmode == true)
            {
                foreach (var control in this.Controls)
                {
                    if (control is RichTextBox richTextBox)
                    {
                        richTextBox.BackColor = Color.Black;
                        richTextBox.ForeColor = Color.LightGray;
                    }
                    else if (control is System.Windows.Forms.TextBox textBox)
                    {
                        textBox.BackColor = Color.Black;
                        textBox.ForeColor = Color.LightGray;
                    }
                    else if (control is System.Windows.Forms.Button button)
                    {
                        button.BackColor = Color.Black;
                        button.ForeColor = Color.LightGray;
                    }
                }
            }
        }

        private void HelpScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.CanDragHelpmenu == true)
            {
                if (Gamemode == false)//added for 1.0.3
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        initialMousePosition = Cursor.Position;
                        initialFormPosition = this.Location;
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        Rectangle workingArea = Screen.GetWorkingArea(this);
                        this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
                    }
                }
            }
        }

        private void HelpScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if(Properties.Settings.Default.CanDragHelpmenu == true)
            {
                if (Gamemode == false)//added for 1.0.3
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
            }
        }
    }
}