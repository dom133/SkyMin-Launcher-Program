using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Compression;
using System.Drawing;
using System.Collections.Generic;
using System.Net;
using System.Drawing.Text;
using Minecraft_Launcher.Forms;

namespace Minecraft_Launcher
{
    public partial class Form2 : Form
    {
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        int num = 1;
        int long_num = 1;
        double recivied;
        double total;
        double previous;
        long ctime;
        long ptime;
        int links_long = 0;
        Stopwatch sw = new Stopwatch();

        public Form2()
        {
            InitializeComponent();
        }

        public void DownloadFile(string sUrlToReadFileFrom, string sFilePathToWriteFileTo)
        {
            // first, we need to get the exact size (in bytes) of the file we are downloading
            Uri url = new Uri(sUrlToReadFileFrom);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            response.Close();
            // gets the size of the file in bytes
            Int64 iSize = response.ContentLength;
            sw.Start();
            // keeps track of the total bytes downloaded so we can update the progress bar
            Int64 iRunningByteTotal = 0;

            // use the webclient object to download the file
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                try
                {
                    using (System.IO.Stream streamRemote = client.OpenRead(new Uri(sUrlToReadFileFrom)))
                    {
                        using (Stream streamLocal = new FileStream(sFilePathToWriteFileTo, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            // loop the stream and get the file into the byte buffer
                            int iByteSize = 0;
                            byte[] byteBuffer = new byte[iSize];
                            while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                            {
                                // write the bytes to the file system at the file path specified
                                streamLocal.Write(byteBuffer, 0, iByteSize);
                                iRunningByteTotal += iByteSize;

                                // calculate the progress out of a base "100"
                                double dIndex = (double)(iRunningByteTotal);
                                double dTotal = (double)byteBuffer.Length;
                                double dProgressPercentage = (dIndex / dTotal);
                                int iProgressPercentage = (int)(dProgressPercentage * 100);

                                recivied = dIndex;
                                total = dTotal;

                                // update the progress bar
                                backgroundWorker1.ReportProgress(iProgressPercentage);
                            }

                            // clean up the file stream
                            streamLocal.Close();
                        }
                        // close the connection to the remote server
                        streamRemote.Close();

                    }
                }
                catch (WebException e)
                {
                    Form1.WriteLine("Error: " + e);
                    MessageBox.Show("Error: "+ e);                  
                    Application.Exit();
                }
            }
        }

        private static System.IO.FileStream GetFileStream(string pathName)
        {
            return (new System.IO.FileStream(pathName, System.IO.FileMode.Open,
                      System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
        }

        public static string GetSHA1Hash(string pathName)
        {
            string strResult = "";
            string strHashData = "";

            byte[] arrbytHashValue;
            System.IO.FileStream oFileStream = null;

            System.Security.Cryptography.SHA1CryptoServiceProvider oSHA1Hasher =
                       new System.Security.Cryptography.SHA1CryptoServiceProvider();

            try
            {
                oFileStream = GetFileStream(pathName);
                arrbytHashValue = oSHA1Hasher.ComputeHash(oFileStream);
                oFileStream.Close();

                strHashData = System.BitConverter.ToString(arrbytHashValue);
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error!",
                         System.Windows.Forms.MessageBoxButtons.OK,
                         System.Windows.Forms.MessageBoxIcon.Error,
                         System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }

            return (strResult);
        }

        public bool checkFile(string filePatch, string url, string hash)
        {
            Form1.WriteLine("Sprawdzanie checksum");
            try
            {
                Action updateLabel1 = () => text.Text = "Sprawdzanie checksum";
                text.Invoke(updateLabel1);
            }
            catch { }

            if (url != "false")
            {
                if (url[url.Length - 1] == 'r' | url[url.Length - 1] == 'n') return false;
                Uri URL = new Uri(url);
                string result = Form1.webClient.DownloadString(URL);
                string check;

                if (File.Exists(filePatch + ".sha1") == false) check = GetSHA1Hash(filePatch);
                else
                {
                    using (StreamReader sr = new StreamReader(filePatch + ".sha1"))
                    {
                        check = sr.ReadToEnd();
                        sr.Close();
                    }
                }

                if (check.ToLower() == result) return true;
                else return false;
            }
            else
            {
                string result = Path.GetFileName(filePatch);
                if (result.ToLower() == hash.ToLower()) return true;
                else return false;
            }
        }

        public List<string> CreateDownladsLinks(string version)
        {
            if (version[version.Length - 1] != 'F') //if
            {
                string libraries_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + version + "/" + version + ".json");
                dynamic decoded = SimpleJson.DeserializeObject(libraries_json);
                string resorces_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/indexes/" + decoded["assets"] + ".json");
                dynamic decoded_resorces = SimpleJson.DeserializeObject(resorces_json);

                string temp = "";
                string temp_resorces = "";
                string temp_natives = "";

                List<string> links = new List<string>();

                links.Add("http://s3.amazonaws.com/Minecraft.Download/versions/" + version + "/" + version + ".jar");
                links.Add("http://s3.amazonaws.com/Minecraft.Download/versions/" + version + "/" + version + ".json");
                links.Add("http://s3.amazonaws.com/Minecraft.Download/indexes/" + decoded["assets"] + ".json");
                links.Add("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Assets/updater.exe");

                for (int i = 0; i <= (decoded["libraries"].Count) - 1; i++) //libraries minecraft
                {
                    temp = decoded["libraries"][i]["name"];
                    string[] temp_lines = temp.Split(' ', ':');

                    try
                    {
                        temp_natives = decoded["libraries"][i]["natives"]["windows"];
                        if (Form1.is64bit)
                        {
                            temp_natives = temp_natives.Replace("${arch}", "64");
                            links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                            links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar.sha1");
                        }
                        else
                        {
                            temp_natives = temp_natives.Replace("${arch}", "32");
                            links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                            links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar.sha1");
                        }
                    }
                    catch
                    {
                        temp_lines = temp.Split(' ', ':');
                        links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                        links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                    }
                }

                links_long = links.Count;

                for (int i = 0; i <= (decoded_resorces["objects"].Count) - 1; i++) //assets minecraft
                {
                    temp_resorces = decoded_resorces["objects"][i]["hash"];
                    links.Add("http://resources.download.minecraft.net/" + temp_resorces[0] + temp_resorces[1] + "/" + temp_resorces);
                }

                return links;
            }
            else //else
            {
                    string forge_json = Form1.webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/" + version + ".json");
                    dynamic forge_decode = SimpleJson.DeserializeObject(forge_json);
                    string libraries_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + forge_decode["jar"] + "/" + forge_decode["jar"] + ".json");
                    dynamic decoded = SimpleJson.DeserializeObject(libraries_json);
                    string resorces_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/indexes/" + decoded["assets"] + ".json");
                    dynamic decoded_resorces = SimpleJson.DeserializeObject(resorces_json);

                    string temp = "";
                    string temp_resorces = "";
                    string temp_natives = "";

                    List<string> links = new List<string>();

                    links.Add("http://s3.amazonaws.com/Minecraft.Download/versions/" + forge_decode["jar"] + "/" + forge_decode["jar"] + ".jar");
                    links.Add("http://s3.amazonaws.com/Minecraft.Download/versions/" + forge_decode["jar"] + "/" + forge_decode["jar"] + ".json");
                    links.Add("http://s3.amazonaws.com/Minecraft.Download/indexes/" + decoded["assets"] + ".json");
                    links.Add(Form1.webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/config_download.txt"));
                    links.Add(Form1.webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/modpack_download.txt"));
                    links.Add("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Assets/updater.exe");

                    for (int i = 0; i <= (decoded["libraries"].Count) - 1; i++) //libraries minecraft
                    {
                        temp = decoded["libraries"][i]["name"];
                        string[] temp_lines = temp.Split(' ', ':');

                        try
                        {
                            temp_natives = decoded["libraries"][i]["natives"]["windows"];
                            if (Form1.is64bit)
                            {
                                temp_natives = temp_natives.Replace("${arch}", "64");
                                links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                                links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar.sha1");
                            }
                            else
                            {
                                temp_natives = temp_natives.Replace("${arch}", "32");
                                links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                                links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar.sha1");
                            }
                        }
                        catch
                        {
                            temp_lines = temp.Split(' ', ':');
                            links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                            links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                        }
                    }

                    for (int i = 0; i <= forge_decode["libraries"].Count - 1; i++)
                    {
                        temp = forge_decode["libraries"][i]["name"];
                        string[] temp_lines = temp.Split(' ', ':');

                        try
                        {
                            string temp_forge_url = forge_decode["libraries"][i]["url"];
                            string temp_forge = forge_decode["libraries"][i]["name"];
                            string[] temp_forge_lines = temp_forge.Split(' ', ':');
                            if (i == 0)
                            {
                                links.Add(temp_forge_url + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-universal.jar");
                                links.Add(temp_forge_url + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-universal.jar.sha1");
                            }
                            else
                            {
                                links.Add(temp_forge_url + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                                links.Add(temp_forge_url + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                            }
                        }
                        catch
                        {
                            temp_lines = temp.Split(' ', ':');
                            links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                            links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                        }
                    }

                    links_long = links.Count;

                    for (int i = 0; i <= (decoded_resorces["objects"].Count) - 1; i++) //assets minecraft
                    {
                        temp_resorces = decoded_resorces["objects"][i]["hash"];
                        links.Add("http://resources.download.minecraft.net/" + temp_resorces[0] + temp_resorces[1] + "/" + temp_resorces);
                    }

                    return links;
            }
        }

        public List<string> CreateDirectoryList(string version)
        {
            if (version[version.Length - 1] != 'F') //if
            {
                string libraries_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + version + "/" + version + ".json");
                dynamic decoded = SimpleJson.DeserializeObject(libraries_json);
                string resorces_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/indexes/" + decoded["assets"] + ".json");
                dynamic decoded_resorces = SimpleJson.DeserializeObject(resorces_json);
                string temp = "";
                string temp_resorces = "";
                string temp_natives = "";
                string minecraft_dir = Form1.appdata + "\\skymin\\minecraft\\";

                List<string> dir = new List<string>();


                dir.Add(minecraft_dir + "versions\\" + version + "\\" + version + ".jar");
                dir.Add(minecraft_dir + "versions\\" + version + "\\" + version + ".json");
                dir.Add(minecraft_dir + "assets\\indexes\\" + decoded["assets"] + ".json");
                dir.Add(appdata + "\\skymin\\assets\\updater.exe");

                for (int i = 0; i <= (decoded["libraries"].Count) - 1; i++)
                {
                    temp = decoded["libraries"][i]["name"];
                    string[] temp_lines = temp.Split(' ', ':');

                    try
                    {
                        temp_natives = decoded["libraries"][i]["natives"]["windows"];
                        if (Form1.is64bit)
                        {
                            temp_natives = temp_natives.Replace("${arch}", "64");
                            dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                            dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar.sha1");
                        }
                        else
                        {
                            temp_natives = temp_natives.Replace("${arch}", "32");
                            dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                            dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar.sha1");
                        }
                    }
                    catch
                    {
                        temp_lines = temp.Split(' ', ':');
                        dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                        dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                    }
                }

                for (int i = 0; i <= (decoded_resorces["objects"].Count) - 1; i++)
                {
                    temp_resorces = decoded_resorces["objects"][i]["hash"];
                    dir.Add(minecraft_dir + "assets\\objects\\" + temp_resorces[0] + temp_resorces[1] + "\\" + temp_resorces);
                }

                return dir;
            }
            else //Else
            {
                string forge_json = Form1.webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/" + version + ".json");
                dynamic forge_decode = SimpleJson.DeserializeObject(forge_json);
                string libraries_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + forge_decode["jar"] + "/" + forge_decode["jar"] + ".json");
                dynamic decoded = SimpleJson.DeserializeObject(libraries_json);
                string resorces_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/indexes/" + decoded["assets"] + ".json");
                dynamic decoded_resorces = SimpleJson.DeserializeObject(resorces_json);
                string temp = "";
                string temp_resorces = "";
                string temp_natives = "";
                string minecraft_dir = Form1.appdata + "\\skymin\\minecraft\\";

                List<string> dir = new List<string>();


                dir.Add(minecraft_dir + "versions\\" + forge_decode["jar"] + "\\" + forge_decode["jar"] + ".jar");
                dir.Add(minecraft_dir + "versions\\" + forge_decode["jar"] + "\\" + forge_decode["jar"] + ".json");
                dir.Add(minecraft_dir + "assets\\indexes\\" + decoded["assets"] + ".json");
                dir.Add(Form1.appdata + "\\skymin\\download\\config.zip");
                dir.Add(Form1.appdata + "\\skymin\\download\\mods.zip");
                dir.Add(appdata + "\\skymin\\assets\\updater.exe");

                for (int i = 0; i <= (decoded["libraries"].Count) - 1; i++)
                {
                    temp = decoded["libraries"][i]["name"];
                    string[] temp_lines = temp.Split(' ', ':');

                    try
                    {
                        temp_natives = decoded["libraries"][i]["natives"]["windows"];
                        if (Form1.is64bit)
                        {
                            temp_natives = temp_natives.Replace("${arch}", "64");
                            dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                            dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar.sha1");
                        }
                        else
                        {
                            temp_natives = temp_natives.Replace("${arch}", "32");
                            dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                            dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar.sha1");
                        }
                    }
                    catch
                    {
                        temp_lines = temp.Split(' ', ':');
                        dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                        dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                    }
                }

                for (int i = 0; i <= forge_decode["libraries"].Count - 1; i++)
                {
                    temp = forge_decode["libraries"][i]["name"];
                    string[] temp_lines = temp.Split(' ', ':');

                    try
                    {
                        string temp_forge_url = forge_decode["libraries"][i]["url"];
                        string temp_forge = forge_decode["libraries"][i]["name"];
                        string[] temp_forge_lines = temp_forge.Split(' ', ':');
                        dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                        dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                    }
                    catch
                    {
                        temp_lines = temp.Split(' ', ':');
                        dir.Add(minecraft_dir+"libraries\\" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                        dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                    }
                }

                for (int i = 0; i <= (decoded_resorces["objects"].Count) - 1; i++)
                {
                    temp_resorces = decoded_resorces["objects"][i]["hash"];
                    dir.Add(minecraft_dir + "assets\\objects\\" + temp_resorces[0] + temp_resorces[1] + "\\" + temp_resorces);
                }

                return dir;
            }
        }

        public List<string> CreateAssetsHash(string version)
        {
            if(version[version.Length-1]=='F')version = version.Replace("F", "");
            string libraries_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + version + "/" + version + ".json");
            dynamic decoded = SimpleJson.DeserializeObject(libraries_json);
            string json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/indexes/" + decoded["assets"] + ".json");
            dynamic decoded_resorces = SimpleJson.DeserializeObject(json);

            List<string> hashs = new List<string>();
            for (int i = 0; i <= (decoded_resorces["objects"].Count) - 1; i++)
            {
                hashs.Add(decoded_resorces["objects"][i]["hash"]);
            }
            return hashs;
        }

        public List<string> CreateDownloadForgeLinks(string version)
        {
            string forge_json = Form1.webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/" + version + ".json");
            dynamic forge_decode = SimpleJson.DeserializeObject(forge_json);
            string temp = "";
            List<string> links = new List<string>();


            for (int i = 0; i <= forge_decode["libraries"].Count - 1; i++)
            {
                temp = forge_decode["libraries"][i]["name"];
                string[] temp_lines = temp.Split(' ', ':');

                try
                {
                    string temp_forge_url = forge_decode["libraries"][i]["url"];
                    string temp_forge = forge_decode["libraries"][i]["name"];
                    string[] temp_forge_lines = temp_forge.Split(' ', ':');
                    if (i == 0)
                    {
                        links.Add(temp_forge_url + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-universal.jar");
                        links.Add(temp_forge_url + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + "-universal.jar.sha1");
                    }
                    else
                    {
                        links.Add(temp_forge_url + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                        links.Add(temp_forge_url + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                    }
                }
                catch
                {
                    temp_lines = temp.Split(' ', ':');
                    links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                    links.Add("https://libraries.minecraft.net/" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                }
            }

            return links;
        }

        public List<string> CreateDirectoryForgeList(string version)
        {
            string forge_json = Form1.webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/" + version + ".json");
            dynamic forge_decode = SimpleJson.DeserializeObject(forge_json);
            string temp = "";
            string minecraft_dir = Form1.appdata + "\\skymin\\minecraft\\";
            List<string> dir = new List<string>();

            for (int i = 0; i <= forge_decode["libraries"].Count - 1; i++)
            {
                temp = forge_decode["libraries"][i]["name"];
                string[] temp_lines = temp.Split(' ', ':');

                try
                {
                    string temp_forge_url = forge_decode["libraries"][i]["url"];
                    string temp_forge = forge_decode["libraries"][i]["name"];
                    string[] temp_forge_lines = temp_forge.Split(' ', ':');
                    dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                    dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                }
                catch
                {
                    temp_lines = temp.Split(' ', ':');
                    dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar");
                    dir.Add(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "/") + "/" + temp_lines[1] + "/" + temp_lines[2] + "/" + temp_lines[1] + "-" + temp_lines[2] + ".jar.sha1");
                }
            }
            return dir;
        }

        public void ExtractNatives(string version)
        {
            if (version[version.Length - 1] == 'F') version = version.Replace("F", "");
            string libraries_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + version + "/" + version + ".json");
            dynamic decoded = SimpleJson.DeserializeObject(libraries_json);
            string temp = "";
            string temp_natives = "";
            string minecraft_dir = Form1.appdata + "\\skymin\\minecraft\\";

            for (int i = 0; i <= (decoded["libraries"].Count) - 1; i++)
            {
                temp = decoded["libraries"][i]["name"];
                string[] temp_lines = temp.Split(' ', ':');
                try
                {
                    temp_natives = decoded["libraries"][i]["natives"]["windows"];
                    if (Form1.is64bit)
                    {
                        temp_natives = temp_natives.Replace("${arch}", "64");
                        ZipFile.ExtractToDirectory(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar", minecraft_dir +"versions\\"+version+"\\natives");
                        Form1.WriteLine("Wypakowywanie pliku: " + minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                    }
                    else
                    {
                        temp_natives = temp_natives.Replace("${arch}", "32");
                        ZipFile.ExtractToDirectory(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar", minecraft_dir + "versions\\" + version + "\\natives");
                        Form1.WriteLine("Wypakowywanie pliku: "+ minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + "-" + temp_natives + ".jar");
                    }
                }
                catch
                {

                }
            }
        }

        public void CreateDownloadDirectory(string version)
        {
            if (version[version.Length - 1] != 'F')
            {
                string libraries_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + version + "/" + version + ".json");
                dynamic decoded = SimpleJson.DeserializeObject(libraries_json);
                string resorces_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/indexes/" + decoded["assets"] + ".json");
                dynamic decoded_resorces = SimpleJson.DeserializeObject(resorces_json);
                string temp = "";
                string temp_resorces = "";
                string minecraft_dir = Form1.appdata + "\\skymin\\minecraft\\";

                for (int i = 0; i <= (decoded["libraries"].Count) - 1; i++)
                {
                    temp = decoded["libraries"][i]["name"];
                    string[] temp_lines = temp.Split(' ', ':');
                    Directory.CreateDirectory(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2]);

                }

                for (int i = 0; i <= (decoded_resorces["objects"].Count) - 1; i++)
                {
                    temp_resorces = decoded_resorces["objects"][i]["hash"];
                    Directory.CreateDirectory(minecraft_dir + "assets\\objects\\" + temp_resorces[0] + temp_resorces[1]);
                }


                Directory.CreateDirectory(minecraft_dir + "versions\\" + version);
                Directory.CreateDirectory(minecraft_dir + "assets\\indexes");
                Directory.CreateDirectory(minecraft_dir + "saves");
                Directory.CreateDirectory(minecraft_dir + "resourcepacks");
                Directory.CreateDirectory(minecraft_dir + "server-resource-packs");
                Directory.CreateDirectory(minecraft_dir + "logs");
                Directory.CreateDirectory(minecraft_dir + "versions\\" + version + "\\natives");
            }
            else //Else
            {
                string forge_json = Form1.webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/"+version+".json");
                dynamic forge_decode = SimpleJson.DeserializeObject(forge_json);
                string libraries_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + forge_decode["jar"] + "/" + forge_decode["jar"] + ".json");
                dynamic decoded = SimpleJson.DeserializeObject(libraries_json);
                string resorces_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/indexes/" + decoded["assets"] + ".json");
                dynamic decoded_resorces = SimpleJson.DeserializeObject(resorces_json);
                string temp = "";
                string temp_resorces = "";
                string minecraft_dir = Form1.appdata + "\\skymin\\minecraft\\";

                for (int i = 0; i <= (decoded["libraries"].Count) - 1; i++)
                {
                    temp = decoded["libraries"][i]["name"];
                    string[] temp_lines = temp.Split(' ', ':');
                    Directory.CreateDirectory(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2]);

                }

                for (int i = 0; i <= (forge_decode["libraries"].Count) - 1; i++)
                {
                    temp = forge_decode["libraries"][i]["name"];
                    string[] temp_lines = temp.Split(' ', ':');
                    Directory.CreateDirectory(minecraft_dir + "libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2]);

                }

                for (int i = 0; i <= (decoded_resorces["objects"].Count) - 1; i++)
                {
                    temp_resorces = decoded_resorces["objects"][i]["hash"];
                    Directory.CreateDirectory(minecraft_dir + "assets\\objects\\" + temp_resorces[0] + temp_resorces[1]);
                }


                Directory.CreateDirectory(minecraft_dir + "versions\\" + forge_decode["jar"]);
                Directory.CreateDirectory(minecraft_dir + "assets\\indexes");
                Directory.CreateDirectory(minecraft_dir + "saves");
                Directory.CreateDirectory(minecraft_dir + "resourcepacks");
                Directory.CreateDirectory(minecraft_dir + "server-resource-packs");
                Directory.CreateDirectory(minecraft_dir + "logs");
                Directory.CreateDirectory(minecraft_dir + "versions\\" + forge_decode["jar"] + "\\natives");
            }
        }

        private void button_pobierz_Click(object sender, EventArgs e)
        {
            this.Width = 445;
            this.Height = 297;
            speedLabel.Visible = true;
            procentLabel.Visible = true;
            downloLabel.Visible = true;
            button_pobierz.Enabled = false;
            button_pobierz.Location = new Point(12, 211);
            backgroundWorker1.RunWorkerAsync();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            this.Width = 445;
            this.Height = 156;
            button_pobierz.Location = new Point(12, 74);
            speedLabel.Visible = false;
            procentLabel.Visible = false;
            downloLabel.Visible = false;

            if (Form1.updater == "Minecraft")
            {
                if (Form1.readXml(Form1.appdata + "\\skymin\\config\\launcher.xml", "Instaled_Minecraft") == "false")
                {
                    button_pobierz.Text = "Pobierz";
                    text.Text = "Aby pobrac minecraft kliknij przycisk pobierz";
                }
                else
                {
                    button_pobierz.Text = "Aktualizuj";
                    text.Text = "Aby zaktualizowac minecraft kliknij przycisk aktualizuj";
                }
            }
            else if(Form1.updater == "Launcher")
            {
                button_pobierz.Text = "Aktualizuj";
                text.Text = "Aby zaktualizowac launchera kliknij przycisk aktualizuj";
            }
            else if(Form1.updater == "ModPack")
            {
                button_pobierz.Text = "Aktualizuj";
                text.Text = "Aby zaktualizowac ModPacka kliknij przycisk aktualizuj";
            }
            else if(Form1.updater == "Forge")
            {
                button_pobierz.Text = "Aktualizuj";
                text.Text = "Aby zaktualizowac forga kliknij przycisk aktualizuj";
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("Czy na pewno chcesz wyjść?", "Skymin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Form1.updater == "Minecraft")
            {
                Form1.WriteLine("Tworzenie linkow");
                try
                { 
                    Action updateLabel = () => text.Text = "Tworzenie linków";
                    text.Invoke(updateLabel);
                }
                catch { }

                CreateDownloadDirectory(Form1.version_minecraft);
                List<string> links = CreateDownladsLinks(Form1.version_minecraft);
                List<string> dir = CreateDirectoryList(Form1.version_minecraft);
                List<string> hashs = CreateAssetsHash(Form1.version_minecraft);

                long_num = links.Count;
                bool isMD5 = false;

                for (int i = 0; i <= (links.Count) - 1; i++)
                {
                    int ile = i;
                    if (i <= (links_long - 1))
                    {
                        if (File.Exists(dir[i]) == true)
                        {
                            isMD5 = checkFile(dir[i], links[i + 1], "false");
                        }
                        else isMD5 = false;
                    }
                    else
                    {
                        if (File.Exists(dir[i]) == true)
                        {
                            isMD5 = checkFile(dir[i], "false", hashs[ile - links_long]);
                        }
                        else isMD5 = false;
                    }

                    if (isMD5 == false)
                    {
                        try
                        { 
                            Action updateLabel2 = () => text.Text = "Trwa pobieranie pliku";
                            text.Invoke(updateLabel2);
                        }
                        catch { }
                        Form1.WriteLine("Pobieranie pliku: " + dir[i]);
                        DownloadFile(links[i], dir[i]);
                    }
                    num += 1;
                }
            }
            else if (Form1.updater == "ModPack")
            {
                Form1.WriteLine("Pobieranie aktualizacji modpacka");

                try
                {
                    Action updateLabel2 = () => text.Text = "Trwa pobieranie pliku";
                    text.Invoke(updateLabel2);
                }
                catch { }

                Form1.WriteLine("Pobieranie pliku: "+appdata+"\\skymin\\download\\mods.zip");
                DownloadFile("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/modpack_download.txt", appdata + "\\skymin\\download\\mods.zip");
                Form1.WriteLine("Pobieranie pliku: " + appdata + "\\skymin\\download\\config.zip");
                DownloadFile("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/config_download.txt", appdata + "\\skymin\\download\\config.zip");
            }
            else if (Form1.updater == "Forge")
            {
                Form1.WriteLine("Pobieranie aktualizacji forga");
                Form1.WriteLine("Tworzenie linkow");
                try
                {
                    Action updateLabel = () => text.Text = "Tworzenie linków";
                    text.Invoke(updateLabel);
                }
                catch { }

                CreateDownloadDirectory(Form1.version_minecraft);
                List<string> links = CreateDownloadForgeLinks(Form1.version_minecraft);
                List<string> dir = CreateDirectoryForgeList(Form1.version_minecraft);

                long_num = links.Count;
                bool isMD5 = false;

                for(int i=0; i<=(long_num-1);i++)
                {
                    if (i != long_num - 1)
                    {
                        if (File.Exists(dir[i]) == true)
                        {
                            isMD5 = checkFile(dir[i], links[i + 1], "false");
                        }
                        else isMD5 = false;
                    }
                    else isMD5 = false;

                    if (isMD5==false)
                    {
                        try
                        {
                            Action updateLabel2 = () => text.Text = "Trwa pobieranie pliku";
                            text.Invoke(updateLabel2);
                        }
                        catch { }

                        Form1.WriteLine("Pobieranie pliku: " + dir[i]);
                        DownloadFile(links[i], dir[i]);
                    }
                    num+=1;
                }

            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            ctime = sw.ElapsedMilliseconds;
            speedLabel.Text = ((int)(((recivied - previous) / (double)1024) / ((ctime - ptime) / (double)1000))).ToString() + " Kbp/s";
            previous = recivied;
            ptime = ctime;

            // Update the progressbar percentage only when the value is not the same.
            progressBar1.Value = e.ProgressPercentage;

            // Show the percentage on our label.
            procentLabel.Text = e.ProgressPercentage.ToString() + "% " + num + "/" + long_num;

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            downloLabel.Text = string.Format("{0} MB's / {1} MB's",
                (recivied / 1024d / 1024d).ToString("0.00"),
                (total/ 1024d / 1024d).ToString("0.00"));
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Form1.updater == "Minecraft")
            {
                Form1.WriteLine("Zakonczono sciaganie plikow");              
                Form1.WriteLine("Rozpakowywanie natives");
                ExtractNatives(Form1.version_minecraft);
                sw.Reset();
                Form1.editXml(appdata + "\\skymin\\config\\launcher.xml", "Launcher", "Minecraft_Version", Form1.version_minecraft);
                if (Form1.version_minecraft[Form1.version_minecraft.Length - 1] == 'F')
                {
                    Form1.WriteLine("Przygotowywanie modow");
                    Form1.editXml(appdata + "\\skymin\\config\\launcher.xml", "Launcher", "Forge_Version", Form1.version_forge);
                    Form1.editXml(appdata + "\\skymin\\config\\launcher.xml", "Launcher", "ModPack_Version", Form1.version_pack);
                    ZipFile.ExtractToDirectory(appdata + "\\skymin\\download\\config.zip", appdata + "\\skymin\\minecraft");
                    ZipFile.ExtractToDirectory(appdata + "\\skymin\\download\\mods.zip", appdata + "\\skymin\\minecraft");
                    File.Delete(appdata + "\\skymin\\download\\mods.zip");
                    File.Delete(appdata + "\\skymin\\download\\config.zip");
                    Form1.WriteLine("Zakonczono przygotowywanie modow");
                }
                Form1.editXml(appdata + "\\skymin\\config\\launcher.xml", "Launcher", "Instaled_Minecraft", "true");
                MessageBox.Show("Pobieranie zostalo poprawnie zakonczone", "Aktualizacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Form1.updater == "ModPack")
            {
                Form1.WriteLine("Zakonczono sciaganie plikow");
                Form1.editXml(appdata + "\\skymin\\config\\launcher.xml", "Launcher", "ModPack_Version", Form1.version_pack);
                Directory.Delete(appdata + "\\skymin\\minecraft\\mods", true);
                Directory.Delete(appdata + "\\skymin\\minecraft\\config", true);
                ZipFile.ExtractToDirectory(appdata + "\\skymin\\download\\config.zip", appdata + "\\skymin\\minecraft");
                ZipFile.ExtractToDirectory(appdata + "\\skymin\\download\\mods.zip", appdata + "\\skymin\\minecraft");
                File.Delete(appdata + "\\skymin\\download\\mods.zip");
                File.Delete(appdata + "\\skymin\\download\\config.zip");
                MessageBox.Show("Zakonczono sciaganie plikow", "Aktualizacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Changelog chan = new Changelog("modpack");
                if (Form1.readXml(appdata + "\\skymin\\config\\user.xml", "Show_Changelog") == "true") chan.ShowDialog();
            }
            else if (Form1.updater == "Forge")
            {
                Form1.WriteLine("Zakonczono sciaganie plikow");
                MessageBox.Show("Pobieranie zostalo poprawnie zakonczone", "Aktualizacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1.editXml(appdata + "\\skymin\\config\\launcher.xml", "Launcher", "Forge_Version", Form1.version_forge);
            }

            Form1 frm1 = new Form1(); 
            frm1.Activate();
            this.Hide();
        }
    }
}
