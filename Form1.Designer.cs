
namespace SoD2_Reroll
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbSurvivor1 = new System.Windows.Forms.ComboBox();
            this.lblSurvivor1 = new System.Windows.Forms.Label();
            this.lblSurvivor3 = new System.Windows.Forms.Label();
            this.lblSurvivor2 = new System.Windows.Forms.Label();
            this.cbSurvivor2 = new System.Windows.Forms.ComboBox();
            this.cbSurvivor3 = new System.Windows.Forms.ComboBox();
            this.nudWait = new System.Windows.Forms.NumericUpDown();
            this.lblWait = new System.Windows.Forms.Label();
            this.lblResolution = new System.Windows.Forms.Label();
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudWait)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSurvivor1
            // 
            this.cbSurvivor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSurvivor1.FormattingEnabled = true;
            this.cbSurvivor1.Location = new System.Drawing.Point(137, 6);
            this.cbSurvivor1.Name = "cbSurvivor1";
            this.cbSurvivor1.Size = new System.Drawing.Size(284, 37);
            this.cbSurvivor1.TabIndex = 0;
            this.cbSurvivor1.SelectedIndexChanged += new System.EventHandler(this.cbSurvivor1_SelectedIndexChanged);
            // 
            // lblSurvivor1
            // 
            this.lblSurvivor1.AutoSize = true;
            this.lblSurvivor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurvivor1.Location = new System.Drawing.Point(12, 9);
            this.lblSurvivor1.Name = "lblSurvivor1";
            this.lblSurvivor1.Size = new System.Drawing.Size(119, 29);
            this.lblSurvivor1.TabIndex = 1;
            this.lblSurvivor1.Text = "Survivor 1";
            // 
            // lblSurvivor3
            // 
            this.lblSurvivor3.AutoSize = true;
            this.lblSurvivor3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurvivor3.Location = new System.Drawing.Point(12, 101);
            this.lblSurvivor3.Name = "lblSurvivor3";
            this.lblSurvivor3.Size = new System.Drawing.Size(119, 29);
            this.lblSurvivor3.TabIndex = 2;
            this.lblSurvivor3.Text = "Survivor 3";
            // 
            // lblSurvivor2
            // 
            this.lblSurvivor2.AutoSize = true;
            this.lblSurvivor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurvivor2.Location = new System.Drawing.Point(12, 55);
            this.lblSurvivor2.Name = "lblSurvivor2";
            this.lblSurvivor2.Size = new System.Drawing.Size(119, 29);
            this.lblSurvivor2.TabIndex = 3;
            this.lblSurvivor2.Text = "Survivor 2";
            // 
            // cbSurvivor2
            // 
            this.cbSurvivor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSurvivor2.FormattingEnabled = true;
            this.cbSurvivor2.Location = new System.Drawing.Point(137, 52);
            this.cbSurvivor2.Name = "cbSurvivor2";
            this.cbSurvivor2.Size = new System.Drawing.Size(284, 37);
            this.cbSurvivor2.TabIndex = 4;
            this.cbSurvivor2.SelectedIndexChanged += new System.EventHandler(this.cbSurvivor2_SelectedIndexChanged);
            // 
            // cbSurvivor3
            // 
            this.cbSurvivor3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSurvivor3.FormattingEnabled = true;
            this.cbSurvivor3.Location = new System.Drawing.Point(137, 98);
            this.cbSurvivor3.Name = "cbSurvivor3";
            this.cbSurvivor3.Size = new System.Drawing.Size(284, 37);
            this.cbSurvivor3.TabIndex = 5;
            this.cbSurvivor3.SelectedIndexChanged += new System.EventHandler(this.cbSurvivor3_SelectedIndexChanged);
            // 
            // nudWait
            // 
            this.nudWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudWait.Location = new System.Drawing.Point(356, 144);
            this.nudWait.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudWait.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWait.Name = "nudWait";
            this.nudWait.Size = new System.Drawing.Size(65, 35);
            this.nudWait.TabIndex = 6;
            this.nudWait.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWait.ValueChanged += new System.EventHandler(this.nudWait_ValueChanged);
            // 
            // lblWait
            // 
            this.lblWait.AutoSize = true;
            this.lblWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWait.Location = new System.Drawing.Point(12, 146);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(277, 29);
            this.lblWait.TabIndex = 7;
            this.lblWait.Text = "Wait after start (seconds)";
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResolution.Location = new System.Drawing.Point(12, 191);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(128, 29);
            this.lblResolution.TabIndex = 8;
            this.lblResolution.Text = "Resolution";
            // 
            // cbResolution
            // 
            this.cbResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Location = new System.Drawing.Point(146, 188);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(275, 37);
            this.cbResolution.TabIndex = 9;
            this.cbResolution.SelectedIndexChanged += new System.EventHandler(this.cbResolution_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(222, 238);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(199, 65);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(17, 238);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(199, 65);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 318);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cbResolution);
            this.Controls.Add(this.lblResolution);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.nudWait);
            this.Controls.Add(this.cbSurvivor3);
            this.Controls.Add(this.cbSurvivor2);
            this.Controls.Add(this.lblSurvivor2);
            this.Controls.Add(this.lblSurvivor3);
            this.Controls.Add(this.lblSurvivor1);
            this.Controls.Add(this.cbSurvivor1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "SoD2 Reroll";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSurvivor1;
        private System.Windows.Forms.Label lblSurvivor1;
        private System.Windows.Forms.Label lblSurvivor3;
        private System.Windows.Forms.Label lblSurvivor2;
        private System.Windows.Forms.ComboBox cbSurvivor2;
        private System.Windows.Forms.ComboBox cbSurvivor3;
        private System.Windows.Forms.NumericUpDown nudWait;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.ComboBox cbResolution;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
    }
}

