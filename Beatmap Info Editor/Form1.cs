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
        DataHandler dh;
        FileContainer fc;
        string dir;
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
                dir = (new FileInfo(FileDialog.FileName)).DirectoryName;
                fc = new FileContainer(dir);
                Bitmap bmp = new Bitmap(800, 600, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                g.DrawString(fc.FileList.Count.ToString(), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), 20, 40);

                var sb = new StringBuilder();
                for (int i = 0; i < fc.FileList.Count; i++)
                {
                    sb.AppendLine((i + 1) + ". " + fc.FileList[i].Metadata.Version);
                }

                g.DrawString(sb.ToString(), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), 20, 60);
                g.Dispose();

                BackgroundImage = bmp;
            }
            //var tC_Main = new TabControl
            //{
            //    Dock = DockStyle.Fill
            //};
            //Controls.Add(tC_Main);
            dh = new DataHandler();
            dh.Compare(fc);
            var sb2 = new StringBuilder();
            sb2.AppendLine("一共有" + fc.FileList.Count + "个难度.");
            for (int i = 0; i < dh.InfoList.Count; i++)
            {
                if (dh.InfoList[i].Name != "Tags") continue;
                else
                {
                    if (dh.InfoList[i].Same == true) sb2.AppendLine("每个难度的tags都一样，还行.");
                    else
                    {
                        sb2.AppendLine("这堆难度中tags都不一样的，不改能忍？.");
                        for (int j = 0; j < dh.InfoList[i].DifferentInfo.Count; j++)
                        {
                            sb2.AppendLine((j + 1) + ": ");
                            for (int k = 0; k < dh.InfoList[i].DifferentInfo[j].Difficulty.Count; k++)
                            {
                                sb2.AppendLine("  " + (k == dh.InfoList[i].DifferentInfo[j].Difficulty.Count - 1 ?
                                    dh.InfoList[i].DifferentInfo[j].Difficulty[k] + ":" : dh.InfoList[i].DifferentInfo[j].Difficulty[k] + "; "));
                            }
                            sb2.AppendLine("    " + dh.InfoList[i].DifferentInfo[j].Information);
                        }
                    }
                }
            }
            label1.Text = sb2.ToString();
            textBox1.Top = label1.Bottom + 5;
            textBox1.Width = panel1.Width - textBox1.Left * 2;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            textBox1.Top = label1.Bottom + 5;
            textBox1.Width = panel1.Width - textBox1.Left * 2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in fc.FileList)
            {
                item.Metadata.Tags = textBox1.Text;
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(dir + "\\osu_file_backup"))
                Directory.CreateDirectory(dir + "\\osu_file_backup");
            
        }
    }
}
