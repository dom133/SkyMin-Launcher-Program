namespace Minecraft_Launcher.Forms
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.nick_name = new System.Windows.Forms.Label();
            this.nick_txt = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.Label();
            this.pass_txt = new System.Windows.Forms.TextBox();
            this.remember = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.typ = new System.Windows.Forms.Label();
            this.type_account = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // nick_name
            // 
            this.nick_name.AutoSize = true;
            this.nick_name.BackColor = System.Drawing.Color.Transparent;
            this.nick_name.ForeColor = System.Drawing.SystemColors.Info;
            this.nick_name.Location = new System.Drawing.Point(87, 95);
            this.nick_name.Name = "nick_name";
            this.nick_name.Size = new System.Drawing.Size(45, 16);
            this.nick_name.TabIndex = 0;
            this.nick_name.Text = "Email:";
            // 
            // nick_txt
            // 
            this.nick_txt.Location = new System.Drawing.Point(139, 92);
            this.nick_txt.Name = "nick_txt";
            this.nick_txt.Size = new System.Drawing.Size(184, 22);
            this.nick_txt.TabIndex = 1;
            // 
            // pass
            // 
            this.pass.AutoSize = true;
            this.pass.BackColor = System.Drawing.Color.Transparent;
            this.pass.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.pass.Location = new System.Drawing.Point(87, 135);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(47, 16);
            this.pass.TabIndex = 2;
            this.pass.Text = "Haslo:";
            // 
            // pass_txt
            // 
            this.pass_txt.Location = new System.Drawing.Point(139, 132);
            this.pass_txt.Name = "pass_txt";
            this.pass_txt.PasswordChar = '*';
            this.pass_txt.Size = new System.Drawing.Size(184, 22);
            this.pass_txt.TabIndex = 3;
            // 
            // remember
            // 
            this.remember.AutoSize = true;
            this.remember.BackColor = System.Drawing.Color.Transparent;
            this.remember.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.remember.Location = new System.Drawing.Point(91, 164);
            this.remember.Name = "remember";
            this.remember.Size = new System.Drawing.Size(95, 20);
            this.remember.TabIndex = 4;
            this.remember.Text = "Zapamietaj";
            this.remember.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(302, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 41);
            this.button1.TabIndex = 5;
            this.button1.Text = "Zaloguj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // typ
            // 
            this.typ.AutoSize = true;
            this.typ.BackColor = System.Drawing.Color.Transparent;
            this.typ.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.typ.Location = new System.Drawing.Point(87, 60);
            this.typ.Name = "typ";
            this.typ.Size = new System.Drawing.Size(74, 16);
            this.typ.TabIndex = 6;
            this.typ.Text = "Typ konta: ";
            // 
            // type_account
            // 
            this.type_account.FormattingEnabled = true;
            this.type_account.Items.AddRange(new object[] {
            "No-Premium",
            "Premium",
            "SkyMin"});
            this.type_account.Location = new System.Drawing.Point(168, 60);
            this.type_account.Name = "type_account";
            this.type_account.Size = new System.Drawing.Size(155, 24);
            this.type_account.TabIndex = 7;
            this.type_account.Text = "No-Premium";
            this.type_account.SelectedIndexChanged += new System.EventHandler(this.type_account_SelectedIndexChanged);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::Minecraft_Launcher.Properties.Resources.loginpicture;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(421, 257);
            this.Controls.Add(this.type_account);
            this.Controls.Add(this.typ);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.remember);
            this.Controls.Add(this.pass_txt);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.nick_txt);
            this.Controls.Add(this.nick_name);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Text = "SkyMin Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nick_name;
        private System.Windows.Forms.TextBox nick_txt;
        private System.Windows.Forms.Label pass;
        private System.Windows.Forms.TextBox pass_txt;
        private System.Windows.Forms.CheckBox remember;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label typ;
        private System.Windows.Forms.ComboBox type_account;
    }
}