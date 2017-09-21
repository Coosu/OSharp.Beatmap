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

        public void Reload()
        {
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

            tb_Artist.Top = label1.Bottom + 5;
            tb_UArtist.Top = tb_Artist.Bottom + 5;
            tb_Title.Top = tb_UArtist.Bottom + 5;
            tb_UTitle.Top = tb_Title.Bottom + 5;
            tb_Source.Top = tb_UTitle.Bottom + 5;
            tb_Tags.Top = tb_Source.Bottom + 5;
            button1.Top = tb_Tags.Bottom + 5;

            lb_Artist.Top = tb_Artist.Top + 3;
            lb_UArtist.Top = tb_UArtist.Top + 3;
            lb_Title.Top = tb_Title.Top + 3;
            lb_UTitle.Top = tb_UTitle.Top + 3;
            lb_Source.Top = tb_Source.Top + 3;
            lb_Tags.Top = tb_Tags.Top + 3;

            tb_Artist.Width = panel1.Width - tb_Tags.Left - 5;
            tb_UArtist.Width = panel1.Width - tb_Tags.Left - 5;
            tb_Title.Width = panel1.Width - tb_Tags.Left - 5;
            tb_UTitle.Width = panel1.Width - tb_Tags.Left - 5;
            tb_Source.Width = panel1.Width - tb_Tags.Left - 5;
            tb_Tags.Width = panel1.Width - tb_Tags.Left - 5;

            tb_Tags.Text = "";
            button1.Enabled = false;
            if (!panel1.Visible) panel1.Visible = true;
            tabControl1.Visible = true;
            BackColor = Color.White;
            cb_diff.Items.Add("全部");
            foreach (var item in fc.FileList)
            {
                cb_diff.Items.Add(item);
            }
            cb_diff.SelectedItem = cb_diff.Items[0];
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
                Reload();
            }

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tb_Artist.Width = panel1.Width - tb_Tags.Left - 5;
            tb_UArtist.Width = panel1.Width - tb_Tags.Left - 5;
            tb_Title.Width = panel1.Width - tb_Tags.Left - 5;
            tb_UTitle.Width = panel1.Width - tb_Tags.Left - 5;
            tb_Source.Width = panel1.Width - tb_Tags.Left - 5;
            tb_Tags.Width = panel1.Width - tb_Tags.Left - 5;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in fc.FileList)
            {
                item.Metadata.Artist = tb_Artist.Text;
                item.Metadata.ArtistUnicode = tb_UArtist.Text;
                item.Metadata.Title = tb_Title.Text;
                item.Metadata.TitleUnicode = tb_UTitle.Text;
                item.Metadata.Source = tb_Source.Text;
                item.Metadata.Tags = tb_Tags.Text;
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(dir + "\\osu_file_backup"))
                Directory.CreateDirectory(dir + "\\osu_file_backup");

            foreach (var item in fc.FileList)
            {
                File.Copy(dir + "\\" + item.FileName + ".osu", dir + "\\osu_file_backup\\" + DateTime.Now.ToLongTimeString().Replace("/", "_").Replace(":", "_")
                    + " - " + item.FileName + ".bak");
                File.WriteAllText(dir + "\\" + item.FileName + ".osu", item.ToAllTextString());
            }
            Reload();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = tb_Tags.Text != "" && tb_Source.Text != "" && tb_Title.Text != "" &&
                tb_UArtist.Text != "" && tb_UTitle.Text != "" && tb_Artist.Text != "";
        }

        private void tb_Artist_KeyDown(object sender, KeyEventArgs e)
        {
            //Text = e.KeyValue.ToString() +"//"+e.KeyCode.ToString();
        }

        private void tb_Artist_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = tb_Tags.Text != "" && tb_Source.Text != "" && tb_Title.Text != "" &&
               tb_UArtist.Text != "" && tb_UTitle.Text != "" && tb_Artist.Text != "";
        }

        private void tb_UArtist_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = tb_Tags.Text != "" && tb_Source.Text != "" && tb_Title.Text != "" &&
               tb_UArtist.Text != "" && tb_UTitle.Text != "" && tb_Artist.Text != "";
        }

        private void tb_Title_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = tb_Tags.Text != "" && tb_Source.Text != "" && tb_Title.Text != "" &&
               tb_UArtist.Text != "" && tb_UTitle.Text != "" && tb_Artist.Text != "";
        }

        private void tb_UTitle_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = tb_Tags.Text != "" && tb_Source.Text != "" && tb_Title.Text != "" &&
               tb_UArtist.Text != "" && tb_UTitle.Text != "" && tb_Artist.Text != "";
        }

        private void tb_Source_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = tb_Tags.Text != "" && tb_Source.Text != "" && tb_Title.Text != "" &&
               tb_UArtist.Text != "" && tb_UTitle.Text != "" && tb_Artist.Text != "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int startTime = int.Parse(tb_start.Text), endTime = int.Parse(tb_end.Text), step = int.Parse(tb_step.Text);

            if (!Directory.Exists(dir + "\\osu_tool_timing"))
                Directory.CreateDirectory(dir + "\\osu_tool_timing");

            if (cb_diff.SelectedIndex == 0)
            {
                foreach (var item in fc.FileList)
                {
                    var times = (int)((endTime - startTime) / (double)step);
                    var diff = item.Metadata.Version;
                    for (int i = 0; i < times; i++)
                    {
                        foreach (var timing in item.TimingPoints.TimingPointList)
                        {
                            if (i == 0) timing.Offset += startTime;
                            else timing.Offset += step;
                        }
                        foreach (var hitObject in item.HitObjects.HitObjectList)
                        {
                            if (i == 0) hitObject.Offset += startTime;
                            else hitObject.Offset += step;
                        }
                        item.Metadata.Version = diff + " " +( startTime + i * step )+ "ms";
                        File.WriteAllText(dir + "\\osu_tool_timing\\" + item.FileName + ".osu", item.ToAllTextString());
                    }
                  
                }
            }
            else
            {
                OsuFile item = (OsuFile)cb_diff.SelectedItem;
                int times = (int)((endTime - startTime) / (double)step);
                var diff = item.Metadata.Version;
                for (int i = 0; i < times; i++)
                {
                    foreach (var timing in item.TimingPoints.TimingPointList)
                    {
                        if (i == 0) timing.Offset += startTime;
                        else timing.Offset += step;
                    }
                    foreach (var hitObject in item.HitObjects.HitObjectList)
                    {
                        if (i == 0) hitObject.Offset += startTime;
                        else hitObject.Offset += step;
                    }
                    item.Metadata.Version = diff + " " + (startTime + i * step) + "ms";
                    File.WriteAllText(dir + "\\osu_tool_timing\\" + item.FileName + ".osu", item.ToAllTextString());
                }
            }
        }
    }
}
