using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Minecraft_Launcher.Forms;
using System.Diagnostics;

namespace Minecraft_Launcher.Class
{
    public class TextStreamWriter : TextWriter
    {
        public override void Write(char value)
        {
            base.Write(value);
            if (Konsola.Instance != null)
            {
                Konsola.Instance.AppendText(value.ToString());
                //Konsola.Instance.FileStreamWriter(value.ToString());
            }

        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
