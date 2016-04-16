using Minecraft_Launcher.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Minecraft_Launcher.Class
{
    public class Minecraft
    {
        private static void SortOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                if (Konsola.Instance != null)
                {
                    Konsola.Instance.AppendText(Environment.NewLine + outLine.Data);
                }            
            }
        }

        public static string getResponse(string username, string password)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://authserver.mojang.com/authenticate");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new System.IO.StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"agent\":{\"name\":\"Minecraft\",\"version\":1},\"username\":\"" + username + "\",\"password\":\"" + password + "\",\"clientToken\":\"6c9d237d-8fbf-44ef-b46b-0b8a854bf391\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        return result;
                    }
                }
                catch (WebException e)
                {
                    int code = (int)((HttpWebResponse)e.Response).StatusCode;
                    return Convert.ToString(code);
                }
            }
        }

        public static void startMinecraft(string version, string name, string ram, string uuid, string accessToken)
        {
            string arg = "";
            if (Form1.isOnline)
            {
                if (version[version.Length - 1] != 'F')
                {
                    string minecraft_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + version + "/" + version + ".json");
                    dynamic decoded = SimpleJson.DeserializeObject(minecraft_json);
                    var minecraft_dir = Form1.appdata + "\\skymin\\minecraft";
                    string temp = decoded["libraries"][0]["name"];
                    string[] temp_lines = temp.Split(' ', ':');
                    string temp_natives;

                    if (string.IsNullOrEmpty(Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Java-args")) || Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Java-args").Equals("false")) arg += " -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump -Xmx{0}M ";
                    else arg += " -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump -Xmx{0}M " + Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Java-args") + " ";
                    arg += @"-Djava.library.path=" + minecraft_dir + "\\versions\\" + version + "\\natives ";
                    arg += @"-cp " + minecraft_dir + "\\libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + ".jar;";
                    for (int i = 1; i <= (decoded["libraries"].Count) - 1; i++)
                    {
                        temp = decoded["libraries"][i]["name"];
                        try
                        {
                            temp_natives = decoded["libraries"][i]["natives"]["windows"];
                        }
                        catch
                        {
                            temp_lines = temp.Split(' ', ':');
                            arg += minecraft_dir + "\\libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + ".jar;";
                        }
                    }
                    arg += minecraft_dir + "\\versions\\" + version + "\\" + version + ".jar";
                    arg += @" " + decoded["mainClass"] + " --username " + name + " --version " + decoded["id"] + " --gameDir " + minecraft_dir + " --assetsDir " + minecraft_dir + "\\assets" + " --assetIndex " + decoded["assets"] + " --uuid " + uuid + " --accessToken " + accessToken + " --userProperties [] ";

                    Form1.editXml(Form1.appdata + "\\skymin\\config\\launcher.xml", "Launcher", "Minecraft_Launch_Code", arg);
                }
                else //else
                {
                    string forge_json = Form1.webClient.DownloadString("https://raw.githubusercontent.com/dom133/SkyMin-Launcher/master/Forge/"+version+".json");
                    dynamic forge_decoded = SimpleJson.DeserializeObject(forge_json);
                    string minecraft_json = Form1.webClient.DownloadString("http://s3.amazonaws.com/Minecraft.Download/versions/" + forge_decoded["jar"] + "/" + forge_decoded["jar"] + ".json");
                    dynamic decoded = SimpleJson.DeserializeObject(minecraft_json);
                    var minecraft_dir = Form1.appdata + "\\skymin\\minecraft";
                    string temp = decoded["libraries"][0]["name"];
                    string[] temp_lines = temp.Split(' ', ':');
                    string temp_natives;

                    if (string.IsNullOrEmpty(Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Java-args")) || Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Java-args").Equals("false")) arg += " -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump -Xmx{0}M ";
                    else arg += " -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump -Xmx{0}M " + Form1.readXml(Form1.appdata + "\\skymin\\config\\user.xml", "Java-args") + " ";
                    arg += @"-Djava.library.path=" + minecraft_dir + "\\versions\\" + forge_decoded["jar"] + "\\natives ";
                    arg += @"-cp " + minecraft_dir + "\\libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + ".jar;";

                    for (int i = 0; i <= (forge_decoded["libraries"].Count) - 1; i++)
                    {
                        temp = forge_decoded["libraries"][i]["name"];
                        temp_lines = temp.Split(' ', ':');
                        arg += minecraft_dir + "\\libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + ".jar;";
                    }

                    for (int i = 1; i <= (decoded["libraries"].Count) - 1; i++)
                    {
                        temp = decoded["libraries"][i]["name"];
                        try
                        {
                            temp_natives = decoded["libraries"][i]["natives"]["windows"];
                        }
                        catch
                        {
                            temp_lines = temp.Split(' ', ':');
                            arg += minecraft_dir + "\\libraries\\" + temp_lines[0].Replace(".", "\\") + "\\" + temp_lines[1] + "\\" + temp_lines[2] + "\\" + temp_lines[1] + "-" + temp_lines[2] + ".jar;";
                        }
                    }
                    arg += minecraft_dir + "\\versions\\" + forge_decoded["jar"] + "\\" + forge_decoded["jar"] + ".jar";
                    arg += @" " + forge_decoded["mainClass"] + " --username " + name + " --version " + decoded["id"] + " --gameDir " + minecraft_dir + " --assetsDir " + minecraft_dir + "\\assets" + " --assetIndex " + decoded["assets"] + " --uuid " + uuid + " --accessToken " + accessToken + " --userProperties [] ";
                    arg += @" --tweakClass "+forge_decoded["tweakClass"];
                    Form1.editXml(Form1.appdata + "\\skymin\\config\\launcher.xml", "Launcher", "Minecraft_Launch_Code", arg);
                }
            }
            else
            {
                arg = Form1.readXml(Form1.appdata+"\\skymin\\config\\launcher.xml", "Minecraft_Launch_Code");
            }

            arg = String.Format(arg, ram);
            Process p = new Process();
            p.StartInfo.FileName = Form1.java_patch;
            p.StartInfo.Arguments = arg;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.EnableRaisingEvents = true;

            p.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);
            p.ErrorDataReceived += new DataReceivedEventHandler(SortOutputHandler);

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();           

            if(Konsola.Instance != null)
            {
                Form1.WriteLine(Environment.NewLine);
            }

            if (Form1.Instance.launcher_state == 1)
            {
                Action button4 = () => Form1.Instance.button4.Enabled = true;
                Form1.Instance.button4.Invoke(button4);
                Action button3 = () => Form1.Instance.button3.Enabled = true;
                Form1.Instance.button3.Invoke(button3);
                Action button2 = () => Form1.Instance.button2.Enabled = true;
                Form1.Instance.button2.Invoke(button2);
                Action button1 = () => Form1.Instance.button1.Enabled = true;
                Form1.Instance.button1.Invoke(button1);
            }
            else if (Form1.Instance.launcher_state == 2)
            {
                Form1.Instance.CloseApplication();
            }
            else if (Form1.Instance.launcher_state == 3)
            {
                Action visible = () => Form1.Instance.Visible = true;
                Form1.Instance.Invoke(visible);
            }

            Form1.WriteLine("Minecraft zostal wylaczony");
        }
    }
}
