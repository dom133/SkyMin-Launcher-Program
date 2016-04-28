using Minecraft_Launcher.Class;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Minecraft_Launcher.Forms
{
    public partial class Login : Form
    {
        private String type;      

        public Login()
        {
            InitializeComponent();
        }

        private void changeLoc()
        {
            if(type=="Premium")
            {
                typ.Location = new Point(87, 95);
                type_account.Location = new Point(139, 92);
            } else
            {
                typ.Location = new Point(87, 60);
                type_account.Location = new Point(168, 60);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Form1.isOnline)
            {
                type = Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "account_type");
                if (type == "No-Premium") {
                    type_account.Text = "No-Premium";
                    Form1.WriteLine("Logowanie NoPremium");
                    nick_name.Visible = false;
                    nick_txt.Visible = false;
                    pass.Text = "Nick:";
                    pass_txt.PasswordChar = '\0';
                    pass_txt.Text = Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Nick");
                    remember.Visible = false;
                    changeLoc();
                } else if (type == "Premium") {
                    type_account.Text = "Premium";
                    Form1.WriteLine("Logowanie Premium");
                    nick_name.Visible = true;
                    nick_txt.Visible = true;
                    remember.Visible = true;
                    pass_txt.PasswordChar = '*';
                    pass.Text = "Haslo:";
                    nick_txt.Text = Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Email");
                    pass_txt.Text = Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Haslo");
                    if (Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Remember") == "True") remember.Checked = true;
                    changeLoc();
                } else if (type == "SkyMin") {
                    type_account.Text = "SkyMin";
                    changeLoc();
                } else {
                    MessageBox.Show("Wystąpił błąd w pliku xml, aplkacja zostanie wyłączona", "SkyMin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                }               
            }
            else
            {
                type_account.Text = "No-Premium";
                type = "No-Premium";
                Form1.WriteLine("Logowanie NoPremium");
                nick_name.Visible = false;
                nick_txt.Visible = false;
                pass.Text = "Nick:";
                pass_txt.PasswordChar = '\0';
                pass_txt.Text = Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Nick");
                remember.Visible = false;
                changeLoc();
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Czy na pewno chcesz wyjść?", "Logowanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            if (type=="Premium")
            {
                Form1.WriteLine("Trwa laczenie z serwerami mojang");
                var response = Minecraft.getResponse(this.nick_txt.Text, this.pass_txt.Text);
                dynamic decoded = SimpleJson.DeserializeObject(response);
                if (response == "403")
                {
                    MessageBox.Show("Podałeś błędny login lub/i hasło", "Logowanie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Form1.WriteLine("Bledny login lub/i haslo");
                }
                else if (decoded["availableProfiles"].Count == 0)
                {
                    MessageBox.Show("Jestes graczem No-Premium, przełączam w tryb dla No-Premium", "No-Premium", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.WriteLine("Jestes graczem No-Premium");
                    type_account.Text = "No-Premium";
                    type = "No-Premium";
                    nick_name.Visible = false;
                    nick_txt.Visible = false;
                    pass.Text = "Nick:";
                    pass_txt.PasswordChar = '\0';
                    pass_txt.Text = Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Nick");
                    remember.Visible = false;
                    changeLoc();
                }
                else
                {
                    Form1.WriteLine("Poprawnie zalogowano na serwerze mojang");
                    Form1.editXml(Form1.appdata+"\\skymin\\config\\user.xml", "User", "Nick", Convert.ToString(decoded["selectedProfile"]["name"]));
                    if (remember.Checked)
                    {
                        Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "Email", nick_txt.Text);
                        Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "Haslo", pass_txt.Text);
                    }
                    Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "Remember", Convert.ToString(remember.Checked));
                    Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "uuid", Convert.ToString(decoded["selectedProfile"]["id"]));
                    Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "accessToken", Convert.ToString(decoded["accessToken"]));
                    Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "account_type", "Premium");
                    frm1.Activate();
                    this.Hide();
                }
            } else if(type=="No-Premium") {
                Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "Nick", pass_txt.Text);
                Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "uuid", "null");
                Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "accessToken", "null");
                Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "account_type", "No-Premium");
                frm1.Activate();
                this.Hide();
            } else if (type == "SkyMin") {
                Form1.editXml(Form1.appdata + "\\skymin\\config\\user.xml", "User", "account_type", "SkyMin");
            }
        }

        private void type_account_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (type_account.Text == "No-Premium")
            {
                type_account.Text = "No-Premium";
                Form1.WriteLine("Logowanie NoPremium");
                nick_name.Visible = false;
                nick_txt.Visible = false;
                pass.Text = "Nick:";
                pass_txt.PasswordChar = '\0';
                pass_txt.Text = Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Nick");
                remember.Visible = false;
                changeLoc();
            }
            else if (type_account.Text == "Premium")
            {
                type_account.Text = "Premium";
                Form1.WriteLine("Logowanie Premium");
                nick_name.Visible = true;
                nick_txt.Visible = true;
                remember.Visible = true;
                pass_txt.PasswordChar = '*';
                pass.Text = "Haslo:";
                nick_txt.Text = Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Email");
                pass_txt.Text = Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Haslo");
                if (Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Remember") == "True") remember.Checked = true;
                changeLoc();
            }
            else if (type_account.Text == "SkyMin")
            {
                type_account.Text = "SkyMin";
                changeLoc();
            }
            else
            {
                MessageBox.Show("Wystąpił błąd w pliku xml, aplkacja zostanie wyłączona", "SkyMin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            type = type_account.Text;
        }
    }
}
