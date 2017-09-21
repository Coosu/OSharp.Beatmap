namespace Editor
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNewFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Tags = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lb_Tags = new System.Windows.Forms.Label();
            this.lb_Source = new System.Windows.Forms.Label();
            this.tb_Source = new System.Windows.Forms.TextBox();
            this.lb_UTitle = new System.Windows.Forms.Label();
            this.tb_UTitle = new System.Windows.Forms.TextBox();
            this.lb_Title = new System.Windows.Forms.Label();
            this.tb_Title = new System.Windows.Forms.TextBox();
            this.lb_Artist = new System.Windows.Forms.Label();
            this.tb_Artist = new System.Windows.Forms.TextBox();
            this.lb_UArtist = new System.Windows.Forms.Label();
            this.tb_UArtist = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_start = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_end = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_step = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.cb_diff = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(464, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openNewFileToolStripMenuItem,
            this.saveFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openNewFileToolStripMenuItem
            // 
            this.openNewFileToolStripMenuItem.Name = "openNewFileToolStripMenuItem";
            this.openNewFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openNewFileToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openNewFileToolStripMenuItem.Text = "&Open new file...";
            this.openNewFileToolStripMenuItem.Click += new System.EventHandler(this.openNewFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.saveFileToolStripMenuItem.Text = "Save file...";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_Artist);
            this.panel1.Controls.Add(this.tb_Artist);
            this.panel1.Controls.Add(this.lb_UArtist);
            this.panel1.Controls.Add(this.tb_UArtist);
            this.panel1.Controls.Add(this.lb_Title);
            this.panel1.Controls.Add(this.tb_Title);
            this.panel1.Controls.Add(this.lb_UTitle);
            this.panel1.Controls.Add(this.tb_UTitle);
            this.panel1.Controls.Add(this.lb_Source);
            this.panel1.Controls.Add(this.tb_Source);
            this.panel1.Controls.Add(this.lb_Tags);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_Tags);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 350);
            this.panel1.TabIndex = 3;
            this.panel1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "更改";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // tb_Tags
            // 
            this.tb_Tags.Location = new System.Drawing.Point(64, 166);
            this.tb_Tags.Name = "tb_Tags";
            this.tb_Tags.Size = new System.Drawing.Size(53, 23);
            this.tb_Tags.TabIndex = 0;
            this.tb_Tags.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(464, 386);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(456, 356);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.cb_diff);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.tb_step);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tb_end);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.tb_start);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(456, 356);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "给锋哥的";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lb_Tags
            // 
            this.lb_Tags.AutoSize = true;
            this.lb_Tags.Location = new System.Drawing.Point(7, 169);
            this.lb_Tags.Name = "lb_Tags";
            this.lb_Tags.Size = new System.Drawing.Size(43, 17);
            this.lb_Tags.TabIndex = 3;
            this.lb_Tags.Text = "Tags: ";
            // 
            // lb_Source
            // 
            this.lb_Source.AutoSize = true;
            this.lb_Source.Location = new System.Drawing.Point(7, 142);
            this.lb_Source.Name = "lb_Source";
            this.lb_Source.Size = new System.Drawing.Size(51, 17);
            this.lb_Source.TabIndex = 5;
            this.lb_Source.Text = "Source:";
            // 
            // tb_Source
            // 
            this.tb_Source.Location = new System.Drawing.Point(64, 139);
            this.tb_Source.Name = "tb_Source";
            this.tb_Source.Size = new System.Drawing.Size(53, 23);
            this.tb_Source.TabIndex = 4;
            this.tb_Source.TextChanged += new System.EventHandler(this.tb_Source_TextChanged);
            // 
            // lb_UTitle
            // 
            this.lb_UTitle.AutoSize = true;
            this.lb_UTitle.Location = new System.Drawing.Point(7, 113);
            this.lb_UTitle.Name = "lb_UTitle";
            this.lb_UTitle.Size = new System.Drawing.Size(44, 17);
            this.lb_UTitle.TabIndex = 7;
            this.lb_UTitle.Text = "UTitle:";
            // 
            // tb_UTitle
            // 
            this.tb_UTitle.Location = new System.Drawing.Point(64, 110);
            this.tb_UTitle.Name = "tb_UTitle";
            this.tb_UTitle.Size = new System.Drawing.Size(53, 23);
            this.tb_UTitle.TabIndex = 6;
            this.tb_UTitle.TextChanged += new System.EventHandler(this.tb_UTitle_TextChanged);
            // 
            // lb_Title
            // 
            this.lb_Title.AutoSize = true;
            this.lb_Title.Location = new System.Drawing.Point(7, 84);
            this.lb_Title.Name = "lb_Title";
            this.lb_Title.Size = new System.Drawing.Size(35, 17);
            this.lb_Title.TabIndex = 9;
            this.lb_Title.Text = "Title:";
            // 
            // tb_Title
            // 
            this.tb_Title.Location = new System.Drawing.Point(64, 81);
            this.tb_Title.Name = "tb_Title";
            this.tb_Title.Size = new System.Drawing.Size(53, 23);
            this.tb_Title.TabIndex = 8;
            this.tb_Title.TextChanged += new System.EventHandler(this.tb_Title_TextChanged);
            // 
            // lb_Artist
            // 
            this.lb_Artist.AutoSize = true;
            this.lb_Artist.Location = new System.Drawing.Point(7, 26);
            this.lb_Artist.Name = "lb_Artist";
            this.lb_Artist.Size = new System.Drawing.Size(45, 17);
            this.lb_Artist.TabIndex = 13;
            this.lb_Artist.Text = "Artist: ";
            // 
            // tb_Artist
            // 
            this.tb_Artist.Location = new System.Drawing.Point(64, 23);
            this.tb_Artist.Name = "tb_Artist";
            this.tb_Artist.Size = new System.Drawing.Size(53, 23);
            this.tb_Artist.TabIndex = 12;
            this.tb_Artist.TextChanged += new System.EventHandler(this.tb_Artist_TextChanged);
            this.tb_Artist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Artist_KeyDown);
            // 
            // lb_UArtist
            // 
            this.lb_UArtist.AutoSize = true;
            this.lb_UArtist.Location = new System.Drawing.Point(7, 55);
            this.lb_UArtist.Name = "lb_UArtist";
            this.lb_UArtist.Size = new System.Drawing.Size(50, 17);
            this.lb_UArtist.TabIndex = 11;
            this.lb_UArtist.Text = "UArtist:";
            // 
            // tb_UArtist
            // 
            this.tb_UArtist.Location = new System.Drawing.Point(64, 52);
            this.tb_UArtist.Name = "tb_UArtist";
            this.tb_UArtist.Size = new System.Drawing.Size(53, 23);
            this.tb_UArtist.TabIndex = 10;
            this.tb_UArtist.TextChanged += new System.EventHandler(this.tb_UArtist_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "offset移动相对范围：";
            // 
            // tb_start
            // 
            this.tb_start.Location = new System.Drawing.Point(139, 6);
            this.tb_start.Name = "tb_start";
            this.tb_start.Size = new System.Drawing.Size(66, 23);
            this.tb_start.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "-";
            // 
            // tb_end
            // 
            this.tb_end.Location = new System.Drawing.Point(230, 6);
            this.tb_end.Name = "tb_end";
            this.tb_end.Size = new System.Drawing.Size(66, 23);
            this.tb_end.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "步长：";
            // 
            // tb_step
            // 
            this.tb_step.Location = new System.Drawing.Point(58, 38);
            this.tb_step.Name = "tb_step";
            this.tb_step.Size = new System.Drawing.Size(66, 23);
            this.tb_step.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 98);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(78, 25);
            this.button3.TabIndex = 7;
            this.button3.Text = "开搞";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cb_diff
            // 
            this.cb_diff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_diff.FormattingEnabled = true;
            this.cb_diff.Location = new System.Drawing.Point(58, 67);
            this.cb_diff.Name = "cb_diff";
            this.cb_diff.Size = new System.Drawing.Size(147, 25);
            this.cb_diff.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "难度：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(464, 411);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNewFileToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Tags;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lb_Tags;
        private System.Windows.Forms.Label lb_Artist;
        private System.Windows.Forms.TextBox tb_Artist;
        private System.Windows.Forms.Label lb_UArtist;
        private System.Windows.Forms.TextBox tb_UArtist;
        private System.Windows.Forms.Label lb_Title;
        private System.Windows.Forms.TextBox tb_Title;
        private System.Windows.Forms.Label lb_UTitle;
        private System.Windows.Forms.TextBox tb_UTitle;
        private System.Windows.Forms.Label lb_Source;
        private System.Windows.Forms.TextBox tb_Source;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_diff;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tb_step;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_end;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_start;
        private System.Windows.Forms.Label label2;
    }
}

