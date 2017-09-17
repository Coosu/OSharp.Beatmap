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
using System.IO;

namespace Editor
{
    public partial class Form1 : Form
    {
        FileContainer fc;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openNewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FileDialog = new OpenFileDialog()
            {
                Filter = "All types|*.*",
                FileName = "",
                CheckFileExists = true,
                ValidateNames = true
            };
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                fc = new FileContainer((new FileInfo(FileDialog.FileName)).DirectoryName);
                Bitmap bmp = new Bitmap(200, 200, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                g.DrawString(fc.FileList.Count.ToString(), new Font("微软雅黑", 10, FontStyle.Regular), new SolidBrush(Color.Black), 20, 20);

                var sb = new StringBuilder();
                for (int i = 0; i < fc.FileList.Count; i++)
                {
                    sb.AppendLine((i + 1) + ". " + fc.FileList[i].Metadata.Version);
                }

                g.DrawString(sb.ToString(), new Font("微软雅黑", 10, FontStyle.Regular), new SolidBrush(Color.Black), 20, 40);
                g.Dispose();

                BackgroundImage = bmp;
            }
            var tC_Main = new TabControl {
                Dock = DockStyle.Fill
            };
            Controls.Add(tC_Main);
            var dh = new DataHandler();
            dh.Compare(fc);
        }
    }
}
