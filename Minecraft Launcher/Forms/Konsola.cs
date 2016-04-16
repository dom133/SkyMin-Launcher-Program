using Minecraft_Launcher.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;
using System.Threading.Tasks;

namespace Minecraft_Launcher.Forms
{
    public partial class Konsola : Form
    {
        TextWriter _writer = null;
        static Konsola instance;
        public static Konsola Instance { get { return instance; } }

        public Konsola()
        {
            InitializeComponent();
        }

        public void FileStreamWriter(string value)
        {
            using (TextWriter Writer = new StreamWriter(Form1.appdata+"\\skymin\\logs\\test.txt", true, Encoding.UTF8, 1000))
            {
                Writer.Write(value);
            }
        }

        public void AppendText(String text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendText), new object[] { text });
                return;
            }
            this.textBox1.AppendText(text);
        }

        private void Console_Load(object sender, EventArgs e)
        {
            instance = this;
            _writer = new TextStreamWriter();
            Console.SetOut(_writer);         
        }

        private void Konsola_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Czy na pewno chcesz zamknac konsole?", "SkyMin-Konsola", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Konsola.Instance.Visible = false;
                    Form1.Instance.console_no.Checked = true;
                    Form1.Instance.consola_tray.Text = "Pokarz konsole";
                }
                else
                {
                    e.Cancel = true;
                }
            }

            instance = null;
        }
    }
}
