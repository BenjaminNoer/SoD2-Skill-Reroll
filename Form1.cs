using System;
using System.IO;
using System.Drawing;
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
        private bool reroll = false, firstIteration = false;
        private short wait = 10000, survivor = 1;
        private readonly short interval = 50;
        private Size resolution = new Size(1920, 1080);
        StreamWriter sw;

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
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\output.txt", String.Empty);
            sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\output.txt");

            cbSurvivor1.Items.AddRange(skills);
            cbSurvivor2.Items.AddRange(skills);
            cbSurvivor3.Items.AddRange(skills);

            cbResolution.Items.AddRange(new string[] { "1280x720", "1360x720", "1366x720", "1600x900", "1920x1080", "2560x1440" });
            cbResolution.SelectedIndex = 4;

            rerollTimer = new System.Timers.Timer(wait);
            rerollTimer.Elapsed += RerollTimer_Elapsed;

            Stop();
        }

        private void RerollTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            rerollTimer.Enabled = false;
            rerollTimer.Interval = interval;

            if (firstIteration)
            {
                sim.Keyboard.KeyPress(VirtualKeyCode.UP);
                sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                firstIteration = false;
            }

            int left = 0;
            int top = (int)Math.Round(resolution.Height / 1.55);

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

            Bitmap img = new Bitmap(375, 35);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(left, top, 0, 0, new Size(375, 35), CopyPixelOperation.SourceCopy);
            //bmp.Save(Directory.GetCurrentDirectory() + "\\SoD2Screenshot.jpg");

            using (var objOcr = OcrApi.Create())
            {
                objOcr.Init(Patagames.Ocr.Enums.Languages.English);
                string plainText = objOcr.GetTextFromImage(img);
                string formattedText = Regex.Replace(plainText, @"\s+", "").ToUpper();

                sw.WriteLine(formattedText);

                if (survivor == 3 && (formattedText.Contains(active[survivor - 1]) || ComputeStringDistance(active[survivor - 1], formattedText) <= (formattedText.Length - active[survivor - 1].Length) + 1))
                {
                    Stop();
                }
                else if (formattedText.Contains(active[survivor - 1]) || ComputeStringDistance(active[survivor - 1], formattedText) <= (formattedText.Length - active[survivor - 1].Length) + 1)
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

            img.Dispose();
        }

        private void EnableTimer()
        {
            System.Threading.Thread.Sleep(250);

            if (reroll)
            {
                rerollTimer.Enabled = true;
            }
        }

        public static int ComputeStringDistance(string input1, string input2)
        {
            var bounds = new { Height = input1.Length + 1, Width = input2.Length + 1 };
            int[,] matrix = new int[bounds.Height, bounds.Width];
            for (int height = 0; height < bounds.Height; height++)
            {
                matrix[height, 0] = height;
            }
            for (int width = 0; width < bounds.Width; width++)
            {
                matrix[0, width] = width;
            }
            for (int height = 1; height < bounds.Height; height++)
            {
                for (int width = 1; width < bounds.Width; width++)
                {
                    int cost = (input1[height - 1] == input2[width - 1]) ? 0 : 1;
                    int insertion = matrix[height, width - 1] + 1;
                    int deletion = matrix[height - 1, width] + 1;
                    int substitution = matrix[height - 1, width - 1] + cost;
                    int distance = Math.Min(insertion, Math.Min(deletion, substitution));
                    if (height > 1 && width > 1 && input1[height - 1] == input2[width - 2] && input1[height - 2] == input2[width - 1])
                    {
                        distance = Math.Min(distance, matrix[height - 2, width - 2] + cost);
                    }
                    matrix[height, width] = distance;
                }
            }
            return matrix[bounds.Height - 1, bounds.Width - 1];
        }

        private void Stop()
        {
            survivor = 1;
            rerollTimer.Interval = wait;
            reroll = false;
            rerollTimer.Enabled = false;
            ToggleButtons(true);
        }

        private void ToggleButtons(bool toggle)
        {
            btnStop.BeginInvoke((Action)delegate() { btnStop.Enabled = !toggle; });
            btnStart.BeginInvoke((Action)delegate() { btnStart.Enabled = toggle; });
        }

        private void cbSurvivor1_SelectedIndexChanged(object sender, EventArgs e) 
        { 
            active[0] = Regex.Replace(cbSurvivor1.GetItemText(cbSurvivor1.SelectedItem).ToUpper(), @"\s+", ""); 
        }

        private void cbSurvivor2_SelectedIndexChanged(object sender, EventArgs e) 
        { 
            active[1] = Regex.Replace(cbSurvivor2.GetItemText(cbSurvivor2.SelectedItem).ToUpper(), @"\s+", ""); 
        }

        private void cbSurvivor3_SelectedIndexChanged(object sender, EventArgs e) 
        { 
            active[2] = Regex.Replace(cbSurvivor3.GetItemText(cbSurvivor3.SelectedItem).ToUpper(), @"\s+", ""); 
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ToggleButtons(false);
            firstIteration = true;
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