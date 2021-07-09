using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Patagames.Ocr;
using System.Timers;
using WindowsInput.Native;
using WindowsInput;
using System.Text.RegularExpressions;

namespace SoD2_Reroll
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer rerollTimer;
        private readonly InputSimulator sim = new InputSimulator();
        private string path;
        private bool reroll = false;
        private short wait = 10000, interval = 100, survivor = 1;
        private Size resolution = new Size(1920, 1080);

        private readonly static string[] skills = 
        { 
            "Acting", "Administration", "Animal Facts", "Bartending", "Business", "Combat Medicine", "Comedy", "Demolitions", 
            "Design", "Driving", "Excuses", "Farting Around", "Firearms Maintenance", "Fishing", "Foraging", "Fortifications", 
            "Geek Trivia", "Gut Packing", "Hacking", "Hairdressing", "Hygiene", "Ikebana", "Infrastructure", "Law", "Lichenology", 
            "Literature", "Logistics", "Making Coffee", "Mobile Operations", "Movie Trivia", "Music", "Painting", "People Skills", 
            "Pinball", "Poker Face", "Police Procedure", "Political Science", "Recycling", "Scrum Certification", "Self-Promotion", 
            "Sewing", "Sexting", "Shopping", "Shopping", "Sleep Psychology", "Sports Trivia", "Soundproofing", "Tattoos", 
            "TV Trivia", "Chemistry", "Computers", "Cooking", "Craftsmanship", "Gardening", "Mechanics", "Medicine", "Utilities" 
        };

        private string[] active = { "", "", "" };

        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbSurvivor1.Items.AddRange(skills);
            cbSurvivor2.Items.AddRange(skills);
            cbSurvivor3.Items.AddRange(skills);

            cbResolution.Items.AddRange(new string[] { "1280x720", "1360x720", "1366x720", "1600x900", "1920x1080", "2560x1440" });
            cbResolution.SelectedIndex = 4;

            rerollTimer = new System.Timers.Timer(wait);
            rerollTimer.Elapsed += RerollTimer_Elapsed;

            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SoD2Screenshot.jpg";

            Stop();
        }

        private void RerollTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            rerollTimer.Enabled = false;
            rerollTimer.Interval = interval;

            int left = 0;
            int top = (int)Math.Round(1440 / 1.55);

            switch (survivor)
            {
                case 1:
                    left = resolution.Width / 5;
                    break;
                case 2:
                    left = (int)Math.Round(resolution.Width / 1.925);
                    break;
                case 3:
                    left = (int)Math.Round(4.2 * resolution.Width / 5);
                    break;
            }

            Bitmap bmp = new Bitmap(375, 35);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(left, top, 0, 0, new Size(375, 35), CopyPixelOperation.SourceCopy);
            bmp.Save(@Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SoD2Screenshot.jpg");

            Reroll(@path);
            bmp.Dispose();
        }

        private void Reroll(string BO3img)
        {
            using (var objOcr = OcrApi.Create())
            {
                objOcr.Init(Patagames.Ocr.Enums.Languages.English);
                string plainText = objOcr.GetTextFromImage(@BO3img);
                string formattedText = Regex.Replace(plainText, @"\s+", "").ToUpper();

                if (survivor == 3 && formattedText.ToUpper().Contains(active[survivor - 1]))
                {
                    Stop();
                }
                else if (formattedText.ToUpper().Contains(active[survivor - 1]))
                {
                    survivor += 1;
                    sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                    EnableTimer();
                }
                else
                {
                    sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                    EnableTimer();
                }
            }
        }

        private void EnableTimer()
        {
            System.Threading.Thread.Sleep(250);

            if (reroll)
            {
                rerollTimer.Enabled = true;
            }
        }


        private void Stop()
        {
            survivor = 1;
            rerollTimer.Interval = wait;
            reroll = false;
            rerollTimer.Enabled = false;
        }

        private void cbSurvivor1_SelectedIndexChanged(object sender, EventArgs e) { active[0] = Regex.Replace(cbSurvivor1.GetItemText(cbSurvivor1.SelectedItem).ToUpper(), @"\s+", ""); }
        private void cbSurvivor2_SelectedIndexChanged(object sender, EventArgs e) { active[1] = Regex.Replace(cbSurvivor2.GetItemText(cbSurvivor2.SelectedItem).ToUpper(), @"\s+", ""); }
        private void cbSurvivor3_SelectedIndexChanged(object sender, EventArgs e) { active[2] = Regex.Replace(cbSurvivor3.GetItemText(cbSurvivor3.SelectedItem).ToUpper(), @"\s+", ""); }

        private void btnStart_Click(object sender, EventArgs e)
        {
            reroll = true;
            rerollTimer.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void nudWait_ValueChanged(object sender, EventArgs e) { wait = Convert.ToInt16(nudWait.Value * 100); }

        private void cbResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] dimensions = cbResolution.GetItemText(cbResolution.SelectedItem).Split('x');
            resolution.Width = Convert.ToInt16(dimensions[0]);
            resolution.Height = Convert.ToInt16(dimensions[1]);
        }
    }
}