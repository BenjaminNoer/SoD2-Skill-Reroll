using System;
using System.IO;
using WindowsInput;
using Patagames.Ocr;
using System.Timers;
using System.Drawing;
using WindowsInput.Native;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SoD2_Reroll
{
    public partial class SoD2Reroll : Form
    {
        private System.Timers.Timer rerollTimer;
        private readonly InputSimulator sim = new InputSimulator();
        private bool reroll = false, firstIteration = false;
        private short wait = 10000, survivor = 1;
        private readonly short interval = 50;
        private Size resolution = new Size(1920, 1080);
        private static readonly int heightTrait = 125, heightSkill = 35;
        //StreamWriter sw;

        //Array that holds the currently selected skills and traits in combo boxes
        private string[] activeSkills = { "", "", "" };
        private string[,] activeTraits = { { "", "", "" }, { "", "", "" }, { "", "", "" } };

        //Array that shows which set of skills and traits still need to be found
        private int[] activeSurvivors = { 1, 2, 3};

        public SoD2Reroll() 
        { 
            InitializeComponent(); 
        }

        private void SoD2Reroll_Load(object sender, EventArgs e)
        {
            //Arrays of all skills and traits obtainable by random characters, excludes red talon and heartland exlusives
            string[] skills = Properties.Resources.skills.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string[] traits = Properties.Resources.traits.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            //Create output text file or delete contents if it already exists
            //File.WriteAllText(Directory.GetCurrentDirectory() + "\\output.txt", String.Empty);
            //sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\output.txt");

            cbSurvivor1.Items.AddRange(skills);
            cbSurvivor2.Items.AddRange(skills);
            cbSurvivor3.Items.AddRange(skills);

            cbS1Trait1.Items.AddRange(traits);
            cbS1Trait2.Items.AddRange(traits);
            cbS1Trait3.Items.AddRange(traits);
            cbS2Trait1.Items.AddRange(traits);
            cbS2Trait2.Items.AddRange(traits);
            cbS2Trait3.Items.AddRange(traits);
            cbS3Trait1.Items.AddRange(traits);
            cbS3Trait2.Items.AddRange(traits);
            cbS3Trait3.Items.AddRange(traits);

            cbResolution.Items.AddRange(new string[] { "1280x720", "1360x720", "1366x720", "1600x900", "1920x1080", "2560x1440" });
            cbResolution.SelectedIndex = 4;

            rerollTimer = new System.Timers.Timer(wait);
            rerollTimer.Elapsed += RerollTimer_Elapsed;

            Stop();
        }

        private void RerollTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Disable the timer to stop it from running again before an iteration is done
            rerollTimer.Enabled = false;
            rerollTimer.Interval = interval;

            //Disable reroll if control is held
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                Stop();
            }

            if (firstIteration)
            {
                //Move cursor to the first button
                sim.Keyboard.KeyPress(VirtualKeyCode.UP);
                sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);

                firstIteration = false;
            }

            //define variables for screenshot position
            int leftTrait = 0, leftSkill = 0;
            int topTrait = (int)Math.Round(resolution.Height / 3.65), topSkill = (int)Math.Round(resolution.Height / 1.55);

            //Sets left and top vaiables for each survivor based on resoltuion
            switch (survivor)
            {
                case 1:
                    leftTrait = (int)Math.Round(resolution.Width / 5.47);
                    leftSkill = resolution.Width / 5;
                    break;
                case 2:
                    leftTrait = (int)Math.Round(resolution.Width / 1.995);
                    leftSkill = (int)Math.Round(resolution.Width / 1.925);
                    break;
                case 3:
                    leftTrait = (int)Math.Round(4.1 * resolution.Width / 5);
                    leftSkill = (int)Math.Round(4.2 * resolution.Width / 5);
                    break;
            }

            //Take screenshots for OCR to read
            Bitmap traitsImg = Screenshot(leftTrait, topTrait, heightTrait);
            Bitmap skillsImg = Screenshot(leftSkill, topSkill, heightSkill);

            //If a set of triats / skills have not been found already check if they are in the current screenshot
            if (activeSurvivors[0] == 1 && TextMatch(leftTrait, topTrait, leftSkill, topSkill, traitsImg, skillsImg, 1))
            {
                activeSurvivors[0] = 0;
                survivor++;
                sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                EnableTimer();
            }
            else if (activeSurvivors[1] == 2 && TextMatch(leftTrait, topTrait, leftSkill, topSkill, traitsImg, skillsImg, 2))
            {
                activeSurvivors[1] = 0;
                survivor++;
                sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                EnableTimer();
            }
            else if (activeSurvivors[2] == 3 && TextMatch(leftTrait, topTrait, leftSkill, topSkill, traitsImg, skillsImg, 3))
            {
                activeSurvivors[2] = 0;
                survivor++;
                sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                EnableTimer();
            }
            else
            {
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                EnableTimer();
            }

            //traitsImg.Save(Directory.GetCurrentDirectory() + "\\TestImages\\SoD2TraitScreenshot.jpg");
            //skillsImg.Save(Directory.GetCurrentDirectory() + "\\TestImages\\SoD2SkillScreenshot.jpg");

            traitsImg.Dispose();
            skillsImg.Dispose();

            if (survivor > 3)
            {
                Stop();
            }
        }

        private Bitmap Screenshot(int left, int top, int height)
        {
            Bitmap img = new Bitmap(375, height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(left, top, 0, 0, new Size(375, height), CopyPixelOperation.SourceCopy);
            return img;
        }

        private bool TextMatch(int leftTrait, int topTrait, int leftSkill, int topSkill, Bitmap traits, Bitmap skills, int survivorNumber)
        {
            bool match;

            using (var objOcr = OcrApi.Create())
            {
                objOcr.Init(Patagames.Ocr.Enums.Languages.English);
                string plainTextTraits = objOcr.GetTextFromImage(traits);
                string plainTextSkills = objOcr.GetTextFromImage(skills);
                string formattedTextTraits = Regex.Replace(plainTextTraits, @"\s+", "").ToUpper();
                string formattedTextSkills = Regex.Replace(plainTextSkills, @"\s+", "").ToUpper();

                if (ComputeStringDistance(activeTraits[survivorNumber - 1, 0], formattedTextTraits) <= (formattedTextTraits.Length - activeTraits[survivorNumber - 1, 0].Length) + 1 &&
                    ComputeStringDistance(activeTraits[survivorNumber - 1, 1], formattedTextTraits) <= (formattedTextTraits.Length - activeTraits[survivorNumber - 1, 1].Length) + 1 &&
                    ComputeStringDistance(activeTraits[survivorNumber - 1, 2], formattedTextTraits) <= (formattedTextTraits.Length - activeTraits[survivorNumber - 1, 2].Length) + 1 &&
                    ComputeStringDistance(activeSkills[survivorNumber - 1], formattedTextSkills) <= (formattedTextSkills.Length - activeSkills[survivorNumber - 1].Length) + 1)
                {
                    match = true;
                }
                else
                {
                    match = false;
                }

                //sw.WriteLine(formattedTextTraits);
                //sw.WriteLine(formattedTextSkills);
            }

            return match;
        }
        
        private void EnableTimer()
        {
            ////Program waits 50ms before iterating to prevent misreading by the OCR
            System.Threading.Thread.Sleep(50);

            //Disable reroll if control is held
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                Stop();
            }
            else if (reroll)
            {
                rerollTimer.Enabled = true;
            }
        }

        //Get the distance between two strings of different lengths
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
            //Reset and disable the timer
            survivor = 1;
            rerollTimer.Interval = wait;
            reroll = false;
            rerollTimer.Enabled = false;
            ToggleButtons(true);
        }

        private void ToggleButtons(bool toggle)
        {
            //Enable and disable buttons
            btnStop.BeginInvoke((Action)delegate() { btnStop.Enabled = !toggle; });
            btnStart.BeginInvoke((Action)delegate() { btnStart.Enabled = toggle; });
        }

        private void SkillChanged(object sender, EventArgs e)
        {
            int index = 0;
            ComboBox cb = (ComboBox)sender;

            switch (cb.Name)
            {
                case "cbSurvivor1":
                    index = 0;
                    break;
                case "cbSurvivor2":
                    index = 1;
                    break;
                case "cbSurvivor3":
                    index = 2;
                    break;
            }

            activeSkills[index] = Regex.Replace(cb.GetItemText(cb.SelectedItem).ToUpper(), @"\s+", "");
        }

        private void TraitChanged(object sender, EventArgs e)
        {
            int i = 0, j = 0;
            ComboBox cb = (ComboBox)sender;

            switch (cb.Name)
            {
                case "cbS1Trait1":
                    i = 0;
                    j = 0;
                    break;
                case "cbS1Trait2":
                    i = 0;
                    j = 1;
                    break;
                case "cbS1Trait3":
                    i = 0;
                    j = 2;
                    break;
                case "cbS2Trait1":
                    i = 1;
                    j = 0;
                    break;
                case "cbS2Trait2":
                    i = 1;
                    j = 1;
                    break;
                case "cbS2Trait3":
                    i = 1;
                    j = 2;
                    break;
                case "cbS3Trait1":
                    i = 2;
                    j = 0;
                    break;
                case "cbS3Trait2":
                    i = 2;
                    j = 1;
                    break;
                case "cbS3Trait3":
                    i = 2;
                    j = 2;
                    break;
            }

            activeTraits[i, j] = Regex.Replace(cb.GetItemText(cb.SelectedItem).ToUpper(), @"\s+", "");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            activeSurvivors = new int[] { 1, 2, 3 };
            survivor = 1;
            ToggleButtons(false);
            firstIteration = true;
            reroll = true;
            rerollTimer.Enabled = true;
        }

        private void SoD2Reroll_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //Wait
            System.Threading.Thread.Sleep(wait);

            //Take test images
            Bitmap traitsImg1 = Screenshot((int)Math.Round(resolution.Width / 5.47), (int)Math.Round(resolution.Height / 3.65), heightTrait);
            Bitmap traitsImg2 = Screenshot((int)Math.Round(resolution.Width / 1.995), (int)Math.Round(resolution.Height / 3.65), heightTrait);
            Bitmap traitsImg3 = Screenshot((int)Math.Round(4.1 * resolution.Width / 5), (int)Math.Round(resolution.Height / 3.65), heightTrait);
            Bitmap skillsImg1 = Screenshot(resolution.Width / 5, (int)Math.Round(resolution.Height / 1.55), heightSkill);
            Bitmap skillsImg2 = Screenshot((int)Math.Round(resolution.Width / 1.925), (int)Math.Round(resolution.Height / 1.55), heightSkill);
            Bitmap skillsImg3 = Screenshot((int)Math.Round(4.2 * resolution.Width / 5), (int)Math.Round(resolution.Height / 1.55), heightSkill);

            //Save test images
            traitsImg1.Save(Directory.GetCurrentDirectory() + "\\TestImages\\Survivor1Traits.jpg");
            traitsImg2.Save(Directory.GetCurrentDirectory() + "\\TestImages\\Survivor2Traits.jpg");
            traitsImg3.Save(Directory.GetCurrentDirectory() + "\\TestImages\\Survivor3Traits.jpg");
            skillsImg1.Save(Directory.GetCurrentDirectory() + "\\TestImages\\Survivor1Skills.jpg");
            skillsImg2.Save(Directory.GetCurrentDirectory() + "\\TestImages\\Survivor2Skills.jpg");
            skillsImg3.Save(Directory.GetCurrentDirectory() + "\\TestImages\\Survivor3Skills.jpg");

            //Display finish notification
            MessageBox.Show("Test complete." + Environment.NewLine + "Images saved in " + Directory.GetCurrentDirectory() + "\\TestImages");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void nudWait_ValueChanged(object sender, EventArgs e) 
        { 
            wait = Convert.ToInt16(nudWait.Value * 100); 
        }

        private void cbResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] dimensions = cbResolution.GetItemText(cbResolution.SelectedItem).Split('x');
            resolution.Width = Convert.ToInt16(dimensions[0]);
            resolution.Height = Convert.ToInt16(dimensions[1]);
        }
    }
}