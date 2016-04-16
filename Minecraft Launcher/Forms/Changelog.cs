using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Launcher.Forms
{
    public partial class Changelog : Form
    {
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string value;

        public Changelog(string value)
        {
            InitializeComponent();
            this.value = value;
        }

        private void Changelog_Load(object sender, EventArgs e)
        {
            if(value=="modpack")
            {
                String text = Form1.webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/changelog_modpack.txt");
                textBox1.Text = text;
            }
            else if(value == "launcher")
            {
                //https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/changelog_launcher.txt                                                                                                                                                                                                                       
            }
        }
    }
}
