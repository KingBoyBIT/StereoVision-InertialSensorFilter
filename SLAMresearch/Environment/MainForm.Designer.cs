namespace Environment
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.MapGenBtn = new System.Windows.Forms.Button();
			this.MapPictureBox = new System.Windows.Forms.PictureBox();
			this.MapContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.标记为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.地形边界点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.障碍边界点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.导航路标点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.路径路标点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.定位点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DrawSelect = new System.Windows.Forms.CheckedListBox();
			this.Rec_text = new System.Windows.Forms.TextBox();
			this.PosPointsList = new System.Windows.Forms.CheckedListBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.EdgePointsList = new System.Windows.Forms.CheckedListBox();
			this.mouse_pos_label = new System.Windows.Forms.Label();
			this.GridcheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.GridSizetextBox = new System.Windows.Forms.TextBox();
			this.serialPort_1 = new System.IO.Ports.SerialPort(this.components);
			this.backgroundWorkerSerial = new System.ComponentModel.BackgroundWorker();
			this.MapLoadBtn = new System.Windows.Forms.Button();
			this.MapExportBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).BeginInit();
			this.MapContextMenuStrip.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// MapGenBtn
			// 
			this.MapGenBtn.Location = new System.Drawing.Point(13, 13);
			this.MapGenBtn.Name = "MapGenBtn";
			this.MapGenBtn.Size = new System.Drawing.Size(75, 23);
			this.MapGenBtn.TabIndex = 0;
			this.MapGenBtn.Text = "生成地图";
			this.MapGenBtn.UseVisualStyleBackColor = true;
			this.MapGenBtn.Click += new System.EventHandler(this.MapGenBtn_Click);
			// 
			// MapPictureBox
			// 
			this.MapPictureBox.ContextMenuStrip = this.MapContextMenuStrip;
			this.MapPictureBox.Location = new System.Drawing.Point(785, 12);
			this.MapPictureBox.Name = "MapPictureBox";
			this.MapPictureBox.Size = new System.Drawing.Size(500, 500);
			this.MapPictureBox.TabIndex = 1;
			this.MapPictureBox.TabStop = false;
			this.MapPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.MapPictureBox_Paint);
			this.MapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MapPictureBox_MouseClick);
			this.MapPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapPictureBox_MouseDown);
			this.MapPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapPictureBox_MouseMove);
			// 
			// MapContextMenuStrip
			// 
			this.MapContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.标记为ToolStripMenuItem,
            this.删除ToolStripMenuItem});
			this.MapContextMenuStrip.Name = "MapContextMenuStrip";
			this.MapContextMenuStrip.Size = new System.Drawing.Size(113, 48);
			// 
			// 标记为ToolStripMenuItem
			// 
			this.标记为ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.地形边界点ToolStripMenuItem,
            this.障碍边界点ToolStripMenuItem,
            this.导航路标点ToolStripMenuItem,
            this.路径路标点ToolStripMenuItem,
            this.定位点ToolStripMenuItem});
			this.标记为ToolStripMenuItem.Name = "标记为ToolStripMenuItem";
			this.标记为ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.标记为ToolStripMenuItem.Text = "标记为";
			// 
			// 地形边界点ToolStripMenuItem
			// 
			this.地形边界点ToolStripMenuItem.Name = "地形边界点ToolStripMenuItem";
			this.地形边界点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.地形边界点ToolStripMenuItem.Text = "地形边界点";
			this.地形边界点ToolStripMenuItem.Click += new System.EventHandler(this.地形边界点ToolStripMenuItem_Click);
			// 
			// 障碍边界点ToolStripMenuItem
			// 
			this.障碍边界点ToolStripMenuItem.Name = "障碍边界点ToolStripMenuItem";
			this.障碍边界点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.障碍边界点ToolStripMenuItem.Text = "障碍边界点";
			// 
			// 导航路标点ToolStripMenuItem
			// 
			this.导航路标点ToolStripMenuItem.Name = "导航路标点ToolStripMenuItem";
			this.导航路标点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.导航路标点ToolStripMenuItem.Text = "导航路标点";
			// 
			// 路径路标点ToolStripMenuItem
			// 
			this.路径路标点ToolStripMenuItem.Name = "路径路标点ToolStripMenuItem";
			this.路径路标点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.路径路标点ToolStripMenuItem.Text = "路径路标点";
			// 
			// 定位点ToolStripMenuItem
			// 
			this.定位点ToolStripMenuItem.Name = "定位点ToolStripMenuItem";
			this.定位点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.定位点ToolStripMenuItem.Text = "定位点";
			// 
			// 删除ToolStripMenuItem
			// 
			this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
			this.删除ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.删除ToolStripMenuItem.Text = "删除";
			this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
			// 
			// DrawSelect
			// 
			this.DrawSelect.CheckOnClick = true;
			this.DrawSelect.FormattingEnabled = true;
			this.DrawSelect.Items.AddRange(new object[] {
            "绘制边界",
            "绘制障碍",
            "绘制路径",
            "设置定位点",
            "删除定位点"});
			this.DrawSelect.Location = new System.Drawing.Point(584, 13);
			this.DrawSelect.Name = "DrawSelect";
			this.DrawSelect.Size = new System.Drawing.Size(195, 84);
			this.DrawSelect.TabIndex = 2;
			this.DrawSelect.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.DrawSelect_ItemCheck);
			// 
			// Rec_text
			// 
			this.Rec_text.Location = new System.Drawing.Point(584, 103);
			this.Rec_text.Multiline = true;
			this.Rec_text.Name = "Rec_text";
			this.Rec_text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.Rec_text.Size = new System.Drawing.Size(195, 244);
			this.Rec_text.TabIndex = 3;
			this.Rec_text.WordWrap = false;
			// 
			// PosPointsList
			// 
			this.PosPointsList.CheckOnClick = true;
			this.PosPointsList.FormattingEnabled = true;
			this.PosPointsList.Location = new System.Drawing.Point(6, 6);
			this.PosPointsList.Name = "PosPointsList";
			this.PosPointsList.ScrollAlwaysVisible = true;
			this.PosPointsList.Size = new System.Drawing.Size(195, 196);
			this.PosPointsList.TabIndex = 2;
			this.PosPointsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.DrawSelect_ItemCheck);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(94, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(484, 499);
			this.tabControl1.TabIndex = 4;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.PosPointsList);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(476, 473);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "定位点管理";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.EdgePointsList);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(476, 473);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "边界点管理";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// EdgePointsList
			// 
			this.EdgePointsList.CheckOnClick = true;
			this.EdgePointsList.FormattingEnabled = true;
			this.EdgePointsList.Location = new System.Drawing.Point(6, 6);
			this.EdgePointsList.Name = "EdgePointsList";
			this.EdgePointsList.ScrollAlwaysVisible = true;
			this.EdgePointsList.Size = new System.Drawing.Size(200, 180);
			this.EdgePointsList.TabIndex = 3;
			// 
			// mouse_pos_label
			// 
			this.mouse_pos_label.AutoSize = true;
			this.mouse_pos_label.Location = new System.Drawing.Point(584, 350);
			this.mouse_pos_label.Name = "mouse_pos_label";
			this.mouse_pos_label.Size = new System.Drawing.Size(65, 12);
			this.mouse_pos_label.TabIndex = 5;
			this.mouse_pos_label.Text = "鼠标坐标：";
			// 
			// GridcheckBox
			// 
			this.GridcheckBox.AutoSize = true;
			this.GridcheckBox.Location = new System.Drawing.Point(584, 365);
			this.GridcheckBox.Name = "GridcheckBox";
			this.GridcheckBox.Size = new System.Drawing.Size(60, 16);
			this.GridcheckBox.TabIndex = 6;
			this.GridcheckBox.Text = "栅格化";
			this.GridcheckBox.UseVisualStyleBackColor = true;
			this.GridcheckBox.CheckStateChanged += new System.EventHandler(this.GridcheckBox_CheckStateChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(650, 366);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 7;
			this.label1.Text = "间距";
			// 
			// GridSizetextBox
			// 
			this.GridSizetextBox.Location = new System.Drawing.Point(685, 363);
			this.GridSizetextBox.Name = "GridSizetextBox";
			this.GridSizetextBox.Size = new System.Drawing.Size(94, 21);
			this.GridSizetextBox.TabIndex = 8;
			this.GridSizetextBox.Text = "10";
			// 
			// backgroundWorkerSerial
			// 
			this.backgroundWorkerSerial.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSerial_DoWork);
			// 
			// MapLoadBtn
			// 
			this.MapLoadBtn.Location = new System.Drawing.Point(12, 42);
			this.MapLoadBtn.Name = "MapLoadBtn";
			this.MapLoadBtn.Size = new System.Drawing.Size(75, 23);
			this.MapLoadBtn.TabIndex = 0;
			this.MapLoadBtn.Text = "导入地图";
			this.MapLoadBtn.UseVisualStyleBackColor = true;
			this.MapLoadBtn.Click += new System.EventHandler(this.MapLoadBtn_Click);
			// 
			// MapExportBtn
			// 
			this.MapExportBtn.Location = new System.Drawing.Point(12, 71);
			this.MapExportBtn.Name = "MapExportBtn";
			this.MapExportBtn.Size = new System.Drawing.Size(75, 23);
			this.MapExportBtn.TabIndex = 0;
			this.MapExportBtn.Text = "导出地图";
			this.MapExportBtn.UseVisualStyleBackColor = true;
			this.MapExportBtn.Click += new System.EventHandler(this.MapExportBtn_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1297, 523);
			this.Controls.Add(this.GridSizetextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.GridcheckBox);
			this.Controls.Add(this.mouse_pos_label);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.Rec_text);
			this.Controls.Add(this.DrawSelect);
			this.Controls.Add(this.MapPictureBox);
			this.Controls.Add(this.MapExportBtn);
			this.Controls.Add(this.MapLoadBtn);
			this.Controls.Add(this.MapGenBtn);
			this.Name = "MainForm";
			this.Text = "地图及数据生成工具";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).EndInit();
			this.MapContextMenuStrip.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button MapGenBtn;
		private System.Windows.Forms.PictureBox MapPictureBox;
		private System.Windows.Forms.CheckedListBox DrawSelect;
		private System.Windows.Forms.TextBox Rec_text;
		private System.Windows.Forms.ContextMenuStrip MapContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem 标记为ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 地形边界点ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 障碍边界点ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 导航路标点ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 路径路标点ToolStripMenuItem;
		private System.Windows.Forms.CheckedListBox PosPointsList;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label mouse_pos_label;
		private System.Windows.Forms.CheckBox GridcheckBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox GridSizetextBox;
		private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 定位点ToolStripMenuItem;
		private System.Windows.Forms.CheckedListBox EdgePointsList;
		private System.IO.Ports.SerialPort serialPort_1;
		private System.ComponentModel.BackgroundWorker backgroundWorkerSerial;
		private System.Windows.Forms.Button MapLoadBtn;
		private System.Windows.Forms.Button MapExportBtn;
	}
}

