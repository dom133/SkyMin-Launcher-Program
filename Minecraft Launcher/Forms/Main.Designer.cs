using System;

namespace Minecraft_Launcher
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda wsparcia projektanta - nie należy modyfikować
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.nick_log = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.ram = new System.Windows.Forms.Label();
            this.ram_txt = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Ustawienia = new System.Windows.Forms.TabPage();
            this.changelog_show_group = new System.Windows.Forms.GroupBox();
            this.no_changelog = new System.Windows.Forms.RadioButton();
            this.yes_changelog = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.tryb_lanch = new System.Windows.Forms.GroupBox();
            this.launcher_3 = new System.Windows.Forms.RadioButton();
            this.launcher_2 = new System.Windows.Forms.RadioButton();
            this.launcher_1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.console_yes = new System.Windows.Forms.RadioButton();
            this.console_no = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.consola = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.java_args_txt = new System.Windows.Forms.TextBox();
            this.java_patch_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Dodatki = new System.Windows.Forms.TabPage();
            this.MinecraftWorker = new System.ComponentModel.BackgroundWorker();
            this.SkyTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.consola_tray = new System.Windows.Forms.ToolStripMenuItem();
            this.close_aplication = new System.Windows.Forms.ToolStripMenuItem();
            this.Ustawienia.SuspendLayout();
            this.changelog_show_group.SuspendLayout();
            this.tryb_lanch.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(303, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 54);
            this.button1.TabIndex = 1;
            this.button1.Text = "Graj";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nick_log
            // 
            this.nick_log.AutoSize = true;
            this.nick_log.BackColor = System.Drawing.Color.Transparent;
            this.nick_log.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nick_log.Location = new System.Drawing.Point(607, 366);
            this.nick_log.Name = "nick_log";
            this.nick_log.Size = new System.Drawing.Size(57, 15);
            this.nick_log.TabIndex = 3;
            this.nick_log.Text = "Witaj, {0}";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(610, 393);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 36);
            this.button2.TabIndex = 4;
            this.button2.Text = "Wyloguj";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ram
            // 
            this.ram.AutoSize = true;
            this.ram.BackColor = System.Drawing.Color.Transparent;
            this.ram.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ram.Location = new System.Drawing.Point(22, 393);
            this.ram.Name = "ram";
            this.ram.Size = new System.Drawing.Size(36, 15);
            this.ram.TabIndex = 5;
            this.ram.Text = "Ram:";
            // 
            // ram_txt
            // 
            this.ram_txt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ram_txt.FormattingEnabled = true;
            this.ram_txt.Items.AddRange(new object[] {
            "1024"});
            this.ram_txt.Location = new System.Drawing.Point(77, 390);
            this.ram_txt.Name = "ram_txt";
            this.ram_txt.Size = new System.Drawing.Size(96, 23);
            this.ram_txt.TabIndex = 6;
            this.ram_txt.SelectedIndexChanged += new System.EventHandler(this.ram_txt_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "C:\\";
            this.openFileDialog1.FileName = "java.exe";
            this.openFileDialog1.Filter = "Plik java (.exe)|*.exe";
            // 
            // Ustawienia
            // 
            this.Ustawienia.Controls.Add(this.changelog_show_group);
            this.Ustawienia.Controls.Add(this.label4);
            this.Ustawienia.Controls.Add(this.tryb_lanch);
            this.Ustawienia.Controls.Add(this.groupBox2);
            this.Ustawienia.Controls.Add(this.label3);
            this.Ustawienia.Controls.Add(this.button4);
            this.Ustawienia.Controls.Add(this.consola);
            this.Ustawienia.Controls.Add(this.button3);
            this.Ustawienia.Controls.Add(this.label2);
            this.Ustawienia.Controls.Add(this.java_args_txt);
            this.Ustawienia.Controls.Add(this.java_patch_txt);
            this.Ustawienia.Controls.Add(this.label1);
            this.Ustawienia.Location = new System.Drawing.Point(4, 24);
            this.Ustawienia.Name = "Ustawienia";
            this.Ustawienia.Padding = new System.Windows.Forms.Padding(3);
            this.Ustawienia.Size = new System.Drawing.Size(706, 314);
            this.Ustawienia.TabIndex = 3;
            this.Ustawienia.Text = "Ustawienia";
            this.Ustawienia.UseVisualStyleBackColor = true;
            // 
            // changelog_show_group
            // 
            this.changelog_show_group.Controls.Add(this.no_changelog);
            this.changelog_show_group.Controls.Add(this.yes_changelog);
            this.changelog_show_group.Location = new System.Drawing.Point(155, 224);
            this.changelog_show_group.Name = "changelog_show_group";
            this.changelog_show_group.Size = new System.Drawing.Size(173, 40);
            this.changelog_show_group.TabIndex = 21;
            this.changelog_show_group.TabStop = false;
            // 
            // no_changelog
            // 
            this.no_changelog.AutoSize = true;
            this.no_changelog.Location = new System.Drawing.Point(117, 15);
            this.no_changelog.Name = "no_changelog";
            this.no_changelog.Size = new System.Drawing.Size(44, 19);
            this.no_changelog.TabIndex = 1;
            this.no_changelog.TabStop = true;
            this.no_changelog.Text = "Nie";
            this.no_changelog.UseVisualStyleBackColor = true;
            // 
            // yes_changelog
            // 
            this.yes_changelog.AutoSize = true;
            this.yes_changelog.Location = new System.Drawing.Point(6, 15);
            this.yes_changelog.Name = "yes_changelog";
            this.yes_changelog.Size = new System.Drawing.Size(46, 19);
            this.yes_changelog.TabIndex = 0;
            this.yes_changelog.TabStop = true;
            this.yes_changelog.Text = "Tak";
            this.yes_changelog.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Pokazywać changelog: ";
            // 
            // tryb_lanch
            // 
            this.tryb_lanch.Controls.Add(this.launcher_3);
            this.tryb_lanch.Controls.Add(this.launcher_2);
            this.tryb_lanch.Controls.Add(this.launcher_1);
            this.tryb_lanch.Location = new System.Drawing.Point(103, 151);
            this.tryb_lanch.Name = "tryb_lanch";
            this.tryb_lanch.Size = new System.Drawing.Size(589, 50);
            this.tryb_lanch.TabIndex = 19;
            this.tryb_lanch.TabStop = false;
            // 
            // launcher_3
            // 
            this.launcher_3.AutoSize = true;
            this.launcher_3.Location = new System.Drawing.Point(384, 17);
            this.launcher_3.Name = "launcher_3";
            this.launcher_3.Size = new System.Drawing.Size(189, 19);
            this.launcher_3.TabIndex = 2;
            this.launcher_3.TabStop = true;
            this.launcher_3.Text = "Ukryj po włączeniu Minecraft";
            this.launcher_3.UseVisualStyleBackColor = true;
            // 
            // launcher_2
            // 
            this.launcher_2.AutoSize = true;
            this.launcher_2.Location = new System.Drawing.Point(145, 17);
            this.launcher_2.Name = "launcher_2";
            this.launcher_2.Size = new System.Drawing.Size(200, 19);
            this.launcher_2.TabIndex = 1;
            this.launcher_2.TabStop = true;
            this.launcher_2.Text = "Zamknij po włączeniu Mincraft";
            this.launcher_2.UseVisualStyleBackColor = true;
            // 
            // launcher_1
            // 
            this.launcher_1.AutoSize = true;
            this.launcher_1.Location = new System.Drawing.Point(3, 17);
            this.launcher_1.Name = "launcher_1";
            this.launcher_1.Size = new System.Drawing.Size(95, 19);
            this.launcher_1.TabIndex = 0;
            this.launcher_1.TabStop = true;
            this.launcher_1.Text = "Na wierzchu";
            this.launcher_1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.console_yes);
            this.groupBox2.Controls.Add(this.console_no);
            this.groupBox2.Location = new System.Drawing.Point(106, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 39);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // console_yes
            // 
            this.console_yes.AutoSize = true;
            this.console_yes.Location = new System.Drawing.Point(6, 14);
            this.console_yes.Name = "console_yes";
            this.console_yes.Size = new System.Drawing.Size(46, 19);
            this.console_yes.TabIndex = 9;
            this.console_yes.TabStop = true;
            this.console_yes.Text = "Tak";
            this.console_yes.UseVisualStyleBackColor = true;
            this.console_yes.CheckedChanged += new System.EventHandler(this.console_yes_CheckedChanged);
            // 
            // console_no
            // 
            this.console_no.AutoSize = true;
            this.console_no.Location = new System.Drawing.Point(81, 14);
            this.console_no.Name = "console_no";
            this.console_no.Size = new System.Drawing.Size(44, 19);
            this.console_no.TabIndex = 10;
            this.console_no.TabStop = true;
            this.console_no.Text = "Nie";
            this.console_no.UseVisualStyleBackColor = true;
            this.console_no.CheckedChanged += new System.EventHandler(this.console_no_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Tryb launchera:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(605, 276);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(92, 29);
            this.button4.TabIndex = 8;
            this.button4.Text = "Zapisz";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // consola
            // 
            this.consola.AutoSize = true;
            this.consola.Location = new System.Drawing.Point(3, 110);
            this.consola.Name = "consola";
            this.consola.Size = new System.Drawing.Size(97, 15);
            this.consola.TabIndex = 7;
            this.consola.Text = "Pokarz console:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(591, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 31);
            this.button3.TabIndex = 5;
            this.button3.Text = "Otwórz";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Java args:";
            // 
            // java_args_txt
            // 
            this.java_args_txt.Location = new System.Drawing.Point(77, 57);
            this.java_args_txt.Multiline = true;
            this.java_args_txt.Name = "java_args_txt";
            this.java_args_txt.Size = new System.Drawing.Size(615, 26);
            this.java_args_txt.TabIndex = 2;
            // 
            // java_patch_txt
            // 
            this.java_patch_txt.Location = new System.Drawing.Point(77, 13);
            this.java_patch_txt.Multiline = true;
            this.java_patch_txt.Name = "java_patch_txt";
            this.java_patch_txt.Size = new System.Drawing.Size(507, 31);
            this.java_patch_txt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Java patch:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.webBrowser2);
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(706, 314);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "News";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // webBrowser2
            // 
            this.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser2.Location = new System.Drawing.Point(3, 3);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(23, 23);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.Size = new System.Drawing.Size(700, 308);
            this.webBrowser2.TabIndex = 1;
            this.webBrowser2.Url = new System.Uri("http://skymin-launcher.tumblr.com/", System.UriKind.Absolute);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(23, 23);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(700, 308);
            this.webBrowser1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.Ustawienia);
            this.tabControl1.Controls.Add(this.Dodatki);
            this.tabControl1.Location = new System.Drawing.Point(14, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(714, 342);
            this.tabControl1.TabIndex = 2;
            // 
            // Dodatki
            // 
            this.Dodatki.Location = new System.Drawing.Point(4, 24);
            this.Dodatki.Name = "Dodatki";
            this.Dodatki.Padding = new System.Windows.Forms.Padding(3);
            this.Dodatki.Size = new System.Drawing.Size(706, 314);
            this.Dodatki.TabIndex = 4;
            this.Dodatki.Text = "Dodatki";
            this.Dodatki.UseVisualStyleBackColor = true;
            // 
            // MinecraftWorker
            // 
            this.MinecraftWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MinecraftWorker_DoWork);
            this.MinecraftWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MinecraftWorker_RunWorkerCompleted);
            // 
            // SkyTray
            // 
            this.SkyTray.ContextMenuStrip = this.contextMenuStrip1;
            this.SkyTray.Icon = ((System.Drawing.Icon)(resources.GetObject("SkyTray.Icon")));
            this.SkyTray.Text = "SkyMin Launcher";
            this.SkyTray.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consola_tray,
            this.close_aplication});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 48);
            // 
            // consola_tray
            // 
            this.consola_tray.Name = "consola_tray";
            this.consola_tray.Size = new System.Drawing.Size(166, 22);
            this.consola_tray.Text = "Pokaz konsole";
            this.consola_tray.Click += new System.EventHandler(this.consola_tray_Click);
            // 
            // close_aplication
            // 
            this.close_aplication.Name = "close_aplication";
            this.close_aplication.Size = new System.Drawing.Size(166, 22);
            this.close_aplication.Text = "Zamknij aplikacje";
            this.close_aplication.Click += new System.EventHandler(this.close_aplication_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::Minecraft_Launcher.Properties.Resources.maxresdefault;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(742, 443);
            this.Controls.Add(this.ram_txt);
            this.Controls.Add(this.ram);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.nick_log);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SkyMin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Ustawienia.ResumeLayout(false);
            this.Ustawienia.PerformLayout();
            this.changelog_show_group.ResumeLayout(false);
            this.changelog_show_group.PerformLayout();
            this.tryb_lanch.ResumeLayout(false);
            this.tryb_lanch.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal void editXml()
        {
            throw new NotImplementedException();
        }

        #endregion
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label nick_log;
        private System.Windows.Forms.Label ram;
        private System.Windows.Forms.ComboBox ram_txt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage Ustawienia;
        private System.Windows.Forms.Label consola;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox java_args_txt;
        private System.Windows.Forms.TextBox java_patch_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.ComponentModel.BackgroundWorker MinecraftWorker;
        public System.Windows.Forms.RadioButton console_no;
        public System.Windows.Forms.RadioButton console_yes;
        public System.Windows.Forms.NotifyIcon SkyTray;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem consola_tray;
        public System.Windows.Forms.ToolStripMenuItem close_aplication;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage Dodatki;
        private System.Windows.Forms.GroupBox tryb_lanch;
        private System.Windows.Forms.RadioButton launcher_3;
        private System.Windows.Forms.RadioButton launcher_2;
        private System.Windows.Forms.RadioButton launcher_1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox changelog_show_group;
        private System.Windows.Forms.RadioButton no_changelog;
        private System.Windows.Forms.RadioButton yes_changelog;
        private System.Windows.Forms.Label label4;
    }
}

