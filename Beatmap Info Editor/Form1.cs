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
        FileContainer FC;
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
                FC = new FileContainer((new FileInfo(FileDialog.FileName)).DirectoryName);
                Bitmap bmp = new Bitmap(200, 100,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawString(FC.FileList.Count.ToString(), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), 20, 20);

                var sb = new StringBuilder();
                for (int i = 0; i < FC.FileList.Count; i++)
                {
                    sb.AppendLine((i + 1) + ". " + FC.FileList[i].Metadata.Version);
                }

                g.DrawString(FC.FileList.Count.ToString(), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), 20, 40);
                g.DrawString(sb.ToString(), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), 20, 40);
                g.Dispose();

                BackgroundImage = bmp;
            }

        }
    }
}
