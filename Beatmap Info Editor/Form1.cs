using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Editor.Object;

namespace Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OsuFile x = new OsuFile();
            x = OsuFileReader.ReadFromFile(@"D:\Program Files (x86)\osu!\Songs\484442 fourfolium - SAKURA Skip\fourfolium - SAKURA Skip (Binguo) [test].osu");
        }
    }
}
