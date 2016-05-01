using System;
using System.Windows.Forms;
using System.Net;
using Minecraft_Launcher.Forms;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using Minecraft_Launcher.Class;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Diagnostics;

namespace Minecraft_Launcher
{
    public partial class Form1 : Form
    {
        //Variables
        static Form1 instance;
        public static Form1 Instance { get { return instance; } }
        public string nick;
        public string uuid;
        public string accesToken;
        public string ram_minecraft;
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string version_minecraft = "";
        public static string version_pack = "";
        public static string version_launcher = "0.0.1";
        public static string version_forge = "";                                        
        public static string java_patch;
        public static string updater;
        public static WebClient webClient = new WebClient();
        public static bool isOnline;
        public static bool is64bit = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
        public int launcher_state;

        //Console 
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        //Forms
        Login log = new Login();
        Form2 frm2 = new Form2();

        public Form1()
        {
            InitializeComponent();
        }

        public void CloseApplication() { Application.Exit(); } //Close Aplication 

        public void loadFont()
        {
            if (File.Exists(appdata + "\\skymin\\download\\Minecraft.ttf") == false)
            {
                try
                {
                    webClient.DownloadFile("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Minecraft.ttf", appdata + "\\skymin\\download\\Minecraft.ttf");
                }
                catch { }
            }
            if (File.Exists(appdata + "\\skymin\\download\\Minecraft.ttf") == true)
            {
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(appdata + "\\skymin\\download\\Minecraft.ttf");
                foreach (Control c in this.Controls)
                {
                    c.Font = new Font(pfc.Families[0], 9, FontStyle.Regular);
                }
            }
        } //Not usable function

        public static bool IsConnectedToInternet()
        {
            string host = "google.pl";
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return false;
        } //Check internet connection

        public static void editXml(string loc, string name_class, string name, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(loc);
            doc.SelectSingleNode(name_class+"/"+name).InnerText = value;
            doc.Save(loc);
        }//Edit xml

        public static string readXml(string loc, string name)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            FileStream fs = new FileStream(loc, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName(name);
            try
            {
                fs.Close();
                return xmlnode[0].ChildNodes.Item(0).InnerText.Trim();
            }
            catch
            {
                return "false";
            }
        }//Read Xml

        private void onLoadXml()
        {

            if (Directory.Exists(appdata + "\\skymin") == false)
            {
                Directory.CreateDirectory(appdata + "\\skymin");
                Directory.CreateDirectory(appdata + "\\skymin\\download");
                Directory.CreateDirectory(appdata + "\\skymin\\assets");
                Directory.CreateDirectory(appdata + "\\skymin\\config");
                Directory.CreateDirectory(appdata + "\\skymin\\minecraft");
                Directory.CreateDirectory(appdata + "\\skymin\\logs");
                Directory.CreateDirectory(appdata + "\\minecraft\\launcher-mods");

                using (XmlWriter writer = XmlWriter.Create(appdata + "\\skymin\\config\\launcher.xml"))
                {
                    writer.WriteStartElement("Launcher");
                    writer.WriteElementString("Minecraft_Version", version_minecraft);
                    writer.WriteElementString("ModPack_Version", version_pack);
                    writer.WriteElementString("Launcher_Version", version_launcher);
                    writer.WriteElementString("Forge_Version", version_forge);
                    writer.WriteElementString("Instaled_Minecraft", "false");
                    writer.WriteElementString("Minecraft_Launch_Code", "");
                    writer.WriteElementString("App_Loc", Directory.GetCurrentDirectory() + "\\" + Process.GetCurrentProcess().ProcessName + ".exe");
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
        }//On load function for xml

        private void loadRam()
        {
            if(is64bit)
            {
                ram_txt.Items.Add("2048");
                ram_txt.Items.Add("3072");
                ram_txt.Items.Add("4092");
                ram_txt.Items.Add("5120");
                ram_txt.Items.Add("6144");
                ram_txt.Items.Add("7168");
                ram_txt.Items.Add("8192");
                ram_txt.Text = readXml(appdata+"\\skymin\\config\\user.xml", "Ram");
            }
            else
            {
                ram_txt.Text = "1024";
            }
        }//Load ram 

        private void loadSettings()
        {
            //Java settings
            java_patch_txt.Text = java_patch;
            if (string.IsNullOrEmpty(readXml(appdata + "\\skymin\\config\\user.xml", "Java-args")) || readXml(appdata + "\\skymin\\config\\user.xml", "Java-args").Equals("false")) java_args_txt.Text = "";
            else java_args_txt.Text = readXml(appdata + "\\skymin\\config\\user.xml", "Java-args");
            //Console settings
            if (readXml(appdata + "\\skymin\\config\\user.xml", "Console") == "true") console_yes.Checked = true;
            else if(readXml(appdata + "\\skymin\\config\\user.xml", "Console") == "false") console_no.Checked = true;
            //Launcher settings
            if (readXml(appdata + "\\skymin\\config\\user.xml", "Launch_Setting") == "1") { launcher_1.Checked = true; launcher_state = 1; }
            else if (readXml(appdata + "\\skymin\\config\\user.xml", "Launch_Setting") == "2") { launcher_2.Checked = true; launcher_state = 2; }
            else if (readXml(appdata + "\\skymin\\config\\user.xml", "Launch_Setting") == "3") { launcher_3.Checked = true; launcher_state = 3; }
            //Changelog settings
            if (readXml(appdata + "\\skymin\\config\\user.xml", "Show_Changelog") == "true") yes_changelog.Checked = true;
            else no_changelog.Checked = true;
        }//Load settings

        public static void ShowConsoleWindow()
        {
            var handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
            }
            else
            {
                ShowWindow(handle, SW_SHOW);
            }
        }

        public static void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();

            ShowWindow(handle, SW_HIDE);
        }

        public static void WriteLine(string value)
        {
            DateTime now = DateTime.Now;
            string hour;
            string min;
            string sec;
            if (now.Hour >= 0 & now.Hour < 10) hour = "0" + now.Hour;
            else hour = Convert.ToString(now.Hour);
            if (now.Minute >= 0 & now.Minute < 10) min = "0" + now.Minute;
            else min = Convert.ToString(now.Minute);
            if (now.Second >= 0 & now.Second < 10) sec = "0" + now.Second;
            else sec = Convert.ToString(now.Second);

            Console.WriteLine("["+hour+":"+min+":"+sec+"]"+" "+value);
        } //Send message to console

        private void createXmlUser()
        {
            Form1.WriteLine("Tworzenie glownych plikow i folderow");
            using (XmlWriter writer = XmlWriter.Create(Form1.appdata + "\\skymin\\config\\user.xml"))
            {
                writer.WriteStartElement("User");
                writer.WriteElementString("Nick", "Nick");
                writer.WriteElementString("Email", "Email");
                writer.WriteElementString("Haslo", "Haslo");
                writer.WriteElementString("Remember", "false");
                writer.WriteElementString("Ram", "1024");
                String javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment";
                using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(javaKey))
                {
                    String currentVersion = baseKey.GetValue("CurrentVersion").ToString();
                    using (var homeKey = baseKey.OpenSubKey(currentVersion))
                    {
                        writer.WriteElementString("Java-patch", homeKey.GetValue("JavaHome").ToString() + "\\bin\\javaw.exe");
                        Form1.java_patch = homeKey.GetValue("JavaHome").ToString() + "\\bin\\javaw.exe";
                    }
                }
                writer.WriteElementString("Java-args", " ");
                writer.WriteElementString("Launch_Setting", "1");
                writer.WriteElementString("Console", "false");
                writer.WriteElementString("accessToken", "null");
                writer.WriteElementString("uuid", "null");
                writer.WriteElementString("account_type", "No-Premium");
                writer.WriteElementString("Show_Changelog", "true");
                writer.WriteEndElement();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            instance = this;
            onLoadXml();

            if (File.Exists(Form1.appdata + "\\skymin\\config\\user.xml") == false)
            {
                createXmlUser();
            }

            if (readXml(appdata + "\\skymin\\config\\user.xml", "Console") == "true")
            {
                ShowConsoleWindow();
                HideConsoleWindow();
                Konsola kns = new Konsola();
                kns.Show();
                consola_tray.Text = "Showaj konsole";
            }

            WriteLine("Ladowanie aplikacji");

            version_launcher = readXml(appdata + "\\skymin\\config\\launcher.xml", "Launcher_Version");
            version_minecraft = readXml(appdata + "\\skymin\\config\\launcher.xml", "Minecraft_Version");
            version_pack = readXml(appdata + "\\skymin\\config\\launcher.xml", "ModPack_Version");
            version_forge = readXml(appdata + "\\skymin\\config\\launcher.xml", "Forge_Version");
            version_pack = readXml(appdata + "\\skymin\\config\\launcher.xml", "ModPack_Version");

            if (IsConnectedToInternet() == true)
            {
                isOnline = true;
                string readMinecraft = webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/minecraft_version.txt");
                string readlauncher = webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/launcher_version.txt");
                string readModPack = webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/pack_version.txt");
                string readForge = webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/forge_version.txt");
                if (version_minecraft != readMinecraft || readXml(appdata + "\\skymin\\config\\launcher.xml", "Instaled_Minecraft") == "false")
                {                 
                    updater = "Minecraft";
                    if (readXml(appdata + "\\skymin\\config\\launcher.xml", "Instaled_Minecraft") == "false") { Form1.WriteLine("Nie znaleziono minecraft'a"); MessageBox.Show("Nie masz pobranego minecraft", "Aktualizacja", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else { Form1.WriteLine("Znaleziono nowa wersje minecraft"); MessageBox.Show("Dostepna jest nowa wersja minecraft", "Aktualizacja", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    if (readMinecraft[readMinecraft.Length - 1] == 'F')
                    {
                        version_forge = readForge;
                        version_pack = readModPack;
                    }
                    version_minecraft = readMinecraft;
                    if (frm2.ShowDialog() == DialogResult.OK) { }
                }
                else if (readlauncher != version_launcher)
                {
                    Form1.WriteLine("Znaleziono nowa wersje launchera"); 
                    editXml(Form1.appdata + "\\skymin\\config\\launcher.xml", "Launcher", "App_Loc", Directory.GetCurrentDirectory() + "\\" + Process.GetCurrentProcess().ProcessName + ".exe");
                    MessageBox.Show("Dostepna jest nowa wersja launchera", "Aktualizacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(appdata+"//skymin//assets//updater.exe");
                    Application.Exit();
                }

                if(version_minecraft[version_minecraft.Length-1]=='F') //sprawdzanie wersji modack i forge
                {
                    if (readModPack != version_pack)
                    {
                        Form1.WriteLine("Znaleziono nowa wersje modpacka");
                        updater = "ModPack";
                        MessageBox.Show("Dostepna jest nowa wersja modpacka", "Aktualizacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        version_pack = readModPack;
                        if (frm2.ShowDialog() == DialogResult.OK) { }
                    }
                    else if (version_forge != readForge)
                    {
                        Form1.WriteLine("Znaleziono nowa wersje forga");
                        updater = "Forge";
                        MessageBox.Show("Dostepna jest nowa wersja forga", "Aktualizacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        version_forge = readForge;
                        if (frm2.ShowDialog() == DialogResult.OK) { }
                    }
                }

                if (readXml(appdata + "\\skymin\\config\\user.xml", "Remember") == "True" && readXml(appdata + "\\skymin\\config\\user.xml", "account_type")=="Premium") //Logowanie do Mojang
                {
                    WriteLine("Trwa laczenie z serwerami mojang");
                    var response = Minecraft.getResponse(Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Email"), Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Haslo"));
                    dynamic decoded = SimpleJson.DeserializeObject(response);
                    if (response == "403")
                    {
                        MessageBox.Show("Podałeś błędny login lub/i hasło", "Logowanie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        WriteLine("Bledny login lub/i haslo");
                    }
                    else if (decoded["availableProfiles"].Count == 0)
                    {
                        WriteLine("Podany login i hasło nie posiada premium");
                        log.ShowDialog();
                    }
                    else
                    {
                        WriteLine("Poprawnie zalogowano na serwerze mojang");
                        editXml(appdata + "\\skymin\\config\\user.xml", "User", "Nick", Convert.ToString(decoded["selectedProfile"]["name"]));
                        editXml(appdata + "\\skymin\\config\\user.xml", "User", "uuid", Convert.ToString(decoded["selectedProfile"]["id"]));
                        editXml(appdata + "\\skymin\\config\\user.xml", "User", "accessToken", Convert.ToString(decoded["accessToken"]));
                        this.Activate();
                    }
                }
                else log.ShowDialog();

                nick = readXml(appdata + "\\skymin\\config\\user.xml", "Nick");
                accesToken = readXml(appdata + "\\skymin\\config\\user.xml", "accessToken");
                uuid = readXml(appdata + "\\skymin\\config\\user.xml", "uuid");
                java_patch = readXml(appdata + "\\skymin\\config\\user.xml", "Java-patch");
                nick_log.Text = string.Format("Witaj, {0}", nick);
                loadRam();
                loadSettings();
                this.Text = "Skymin " + version_launcher + "V";
                if(version_minecraft[version_minecraft.Length - 1] != 'F') tabControl1.TabPages.Remove(Dodatki);
            }
            else
            {
                this.Text = "Skymin " + version_launcher + "V" + " (Offline)";
                MessageBox.Show("Brak polaczenia z internetem, włączam w trybie offline", "Internet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isOnline = false;
                Form1.WriteLine("Brak polaczenia z internetem");
                log.ShowDialog();
                nick = readXml(appdata + "\\skymin\\config\\user.xml", "Nick");
                accesToken = readXml(appdata + "\\skymin\\config\\user.xml", "accessToken");
                uuid = readXml(appdata + "\\skymin\\config\\user.xml", "uuid");
                java_patch = readXml(appdata + "\\skymin\\config\\user.xml", "Java-patch");
                nick_log.Text = string.Format("Witaj, {0}", nick);
                loadRam();
                loadSettings();
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(Dodatki);
            }
        }//On Load Form1

        private void button1_Click(object sender, EventArgs e)
        {
            if (readXml(appdata + "\\skymin\\config\\launcher.xml", "Instaled_Minecraft") == "false") { MessageBox.Show("Aktualnie nie masz pobranego minecrafta!!", "SkyMin"); }
            else
            {
                ram_minecraft = ram_txt.Text;
                if (launcher_state == 1)
                {
                    button4.Enabled = false;
                    button3.Enabled = false;
                    button2.Enabled = false;
                    button1.Enabled = false;
                }
                else if (launcher_state == 2)
                {
                    this.Visible = false;
                }
                else if(launcher_state == 3)
                {
                    this.Visible = false;
                }

                MinecraftWorker.RunWorkerAsync();
            }
        }//Open Minecraft

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.WriteLine("Poprawnie wylogowano");
            this.Visible = false;
            if (log.ShowDialog() == DialogResult.OK) { }
            nick = readXml(appdata + "\\skymin\\config\\user.xml", "Nick");
            accesToken = readXml(appdata + "\\skymin\\config\\user.xml", "accessToken");
            uuid = readXml(appdata + "\\skymin\\config\\user.xml", "uuid");
            java_patch = readXml(appdata + "\\skymin\\config\\user.xml", "Java-patch");
            nick_log.Text = string.Format("Witaj, {0}", nick);
            this.Visible = true;
        }//Logout

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                if(MessageBox.Show("Czy na pewno chcesz wyjść?", "SkyMin", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    editXml(appdata + "\\skymin\\config\\user.xml", "User", "Ram", ram_txt.Text);
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }//Form Closing

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            java_patch_txt.Text = openFileDialog1.FileName;
            Form1.WriteLine("Zmieniono pole java patch na "+ java_patch_txt.Text);
        }//JavaPatch

        private void console_yes_CheckedChanged(object sender, EventArgs e)
        {
            if (console_no.Checked) console_no.Checked = false;
            else console_yes.Checked = true;
        }//On change yes button

        private void console_no_CheckedChanged(object sender, EventArgs e)
        {
            if (console_yes.Checked) console_yes.Checked = false;
            else console_no.Checked = true;
        }//On change no button

        private void button4_Click(object sender, EventArgs e)
        {
            Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "Java-patch", java_patch_txt.Text);
            java_patch = java_patch_txt.Text;
            Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "Java-args", java_args_txt.Text);
            //Save console settings
            if (console_yes.Checked)
            {                        
                Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "Console", "true");
                if (Konsola.Instance == null)
                {
                    ShowConsoleWindow();
                    HideConsoleWindow();
                    Konsola kns = new Konsola();
                    kns.Show();
                }
                else
                {
                    Konsola.Instance.Visible = true;
                }
                consola_tray.Text = "Showaj konsole";
            }
            else if (console_no.Checked)
            {
                editXml(appdata + "\\skymin\\config\\user.xml", "User", "Console", "false");
                if(Konsola.Instance != null) Konsola.Instance.Visible = false;
                consola_tray.Text = "Pokarz konsole";
            }
            //Save launcher settings
            if (launcher_1.Checked == true)
            {
                launcher_state = 1;
                editXml(appdata + "\\skymin\\config\\user.xml", "User", "Launch_Setting", "1");
            }
            else if (launcher_2.Checked == true)
            {
                launcher_state = 2;
                editXml(appdata + "\\skymin\\config\\user.xml", "User", "Launch_Setting", "2");
            }
            else if (launcher_3.Checked == true)
            {
                launcher_state = 3;
                editXml(appdata + "\\skymin\\config\\user.xml", "User", "Launch_Setting", "3");
            }
            //Save changelog settings
            if(yes_changelog.Checked) editXml(appdata + "\\skymin\\config\\user.xml", "User", "Show_Changelog", "true");
            else editXml(appdata + "\\skymin\\config\\user.xml", "User", "Show_Changelog", "false");

            Form1.WriteLine("Ustawienia zostalu zapisane");
            MessageBox.Show("Ustawienia zostały poprawnie zapisane", "Ustawienia", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } //Save Button

        private void ram_txt_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.WriteLine("Pole ram zostalo zmienione na "+ram_txt.Text);
        }

        private void MinecraftWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            WriteLine("Minecraft zostal uruchomiony");
            Minecraft.startMinecraft(version_minecraft, nick, ram_minecraft, uuid, accesToken);
        }

        private void MinecraftWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Konsola.Instance.AppendText(Environment.NewLine);
        }

        private void consola_tray_Click(object sender, EventArgs e)
        {
            if (Konsola.Instance != null)
            {
                if (Konsola.Instance.Visible == true)
                {
                    Konsola.Instance.Visible = false;
                    console_no.Checked = true;
                    consola_tray.Text = "Pokarz konsole";
                }
                else
                {
                    Konsola.Instance.Visible = true;
                    console_yes.Checked = true;
                    consola_tray.Text = "Showaj konsole";
                }  
            }
            else
            {
                Konsola kns = new Konsola();
                kns.Show();
                console_yes.Checked = true;
                consola_tray.Text = "Showaj konsole";
            }
        }

        private void close_aplication_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz wyjść?", "SkyMin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                editXml(appdata + "\\skymin\\config\\user.xml", "User", "Ram", ram_txt.Text);
                Application.Exit();
            }
        }
    }
}
