namespace Minecraft_Launcher
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.text = new System.Windows.Forms.Label();
            this.button_pobierz = new System.Windows.Forms.Button();
            this.speedLabel = new System.Windows.Forms.Label();
            this.procentLabel = new System.Windows.Forms.Label();
            this.downloLabel = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 39);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(405, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // text
            // 
            this.text.AutoSize = true;
            this.text.BackColor = System.Drawing.Color.Transparent;
            this.text.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.text.Location = new System.Drawing.Point(12, 13);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(27, 13);
            this.text.TabIndex = 1;
            this.text.Text = "Null";
            // 
            // button_pobierz
            // 
            this.button_pobierz.Location = new System.Drawing.Point(12, 211);
            this.button_pobierz.Name = "button_pobierz";
            this.button_pobierz.Size = new System.Drawing.Size(405, 35);
            this.button_pobierz.TabIndex = 2;
            this.button_pobierz.Text = "button1";
            this.button_pobierz.UseVisualStyleBackColor = true;
            this.button_pobierz.Click += new System.EventHandler(this.button_pobierz_Click);
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.speedLabel.Location = new System.Drawing.Point(110, 124);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(0, 13);
            this.speedLabel.TabIndex = 4;
            // 
            // procentLabel
            // 
            this.procentLabel.AutoSize = true;
            this.procentLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.procentLabel.Location = new System.Drawing.Point(110, 98);
            this.procentLabel.Name = "procentLabel";
            this.procentLabel.Size = new System.Drawing.Size(0, 13);
            this.procentLabel.TabIndex = 5;
            // 
            // downloLabel
            // 
            this.downloLabel.AutoSize = true;
            this.downloLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.downloLabel.Location = new System.Drawing.Point(110, 74);
            this.downloLabel.Name = "downloLabel";
            this.downloLabel.Size = new System.Drawing.Size(0, 13);
            this.downloLabel.TabIndex = 6;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::Minecraft_Launcher.Properties.Resources.image_2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(429, 258);
            this.Controls.Add(this.downloLabel);
            this.Controls.Add(this.procentLabel);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.button_pobierz);
            this.Controls.Add(this.text);
            this.Controls.Add(this.progressBar1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Text = "Pobieranie";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button_pobierz;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label procentLabel;
        private System.Windows.Forms.Label downloLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.Windows.Forms.Label text;
    }
}