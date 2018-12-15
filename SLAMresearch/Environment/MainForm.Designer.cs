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
			this.路标点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.障碍点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.导航路标点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.估计路标点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DrawSelect = new System.Windows.Forms.CheckedListBox();
			this.Rec_text = new System.Windows.Forms.TextBox();
			this.KeyPointsList = new System.Windows.Forms.CheckedListBox();
			((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).BeginInit();
			this.MapContextMenuStrip.SuspendLayout();
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
            this.标记为ToolStripMenuItem});
			this.MapContextMenuStrip.Name = "MapContextMenuStrip";
			this.MapContextMenuStrip.Size = new System.Drawing.Size(113, 26);
			// 
			// 标记为ToolStripMenuItem
			// 
			this.标记为ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.路标点ToolStripMenuItem,
            this.障碍点ToolStripMenuItem,
            this.导航路标点ToolStripMenuItem,
            this.估计路标点ToolStripMenuItem});
			this.标记为ToolStripMenuItem.Name = "标记为ToolStripMenuItem";
			this.标记为ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.标记为ToolStripMenuItem.Text = "标记为";
			// 
			// 路标点ToolStripMenuItem
			// 
			this.路标点ToolStripMenuItem.Name = "路标点ToolStripMenuItem";
			this.路标点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.路标点ToolStripMenuItem.Text = "边界点";
			this.路标点ToolStripMenuItem.Click += new System.EventHandler(this.路标点ToolStripMenuItem_Click);
			// 
			// 障碍点ToolStripMenuItem
			// 
			this.障碍点ToolStripMenuItem.Name = "障碍点ToolStripMenuItem";
			this.障碍点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.障碍点ToolStripMenuItem.Text = "障碍点";
			// 
			// 导航路标点ToolStripMenuItem
			// 
			this.导航路标点ToolStripMenuItem.Name = "导航路标点ToolStripMenuItem";
			this.导航路标点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.导航路标点ToolStripMenuItem.Text = "导航路标点";
			// 
			// 估计路标点ToolStripMenuItem
			// 
			this.估计路标点ToolStripMenuItem.Name = "估计路标点ToolStripMenuItem";
			this.估计路标点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.估计路标点ToolStripMenuItem.Text = "估计路标点";
			// 
			// DrawSelect
			// 
			this.DrawSelect.CheckOnClick = true;
			this.DrawSelect.FormattingEnabled = true;
			this.DrawSelect.Items.AddRange(new object[] {
            "绘制边界",
            "绘制障碍",
            "绘制路径",
            "设置特殊位置坐标点",
            "删除特殊位置坐标点"});
			this.DrawSelect.Location = new System.Drawing.Point(584, 13);
			this.DrawSelect.Name = "DrawSelect";
			this.DrawSelect.Size = new System.Drawing.Size(195, 84);
			this.DrawSelect.TabIndex = 2;
			this.DrawSelect.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.DrawSelect_ItemCheck);
			// 
			// Rec_text
			// 
			this.Rec_text.Location = new System.Drawing.Point(584, 267);
			this.Rec_text.Multiline = true;
			this.Rec_text.Name = "Rec_text";
			this.Rec_text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.Rec_text.Size = new System.Drawing.Size(195, 244);
			this.Rec_text.TabIndex = 3;
			this.Rec_text.WordWrap = false;
			// 
			// KeyPointsList
			// 
			this.KeyPointsList.CheckOnClick = true;
			this.KeyPointsList.FormattingEnabled = true;
			this.KeyPointsList.Location = new System.Drawing.Point(94, 12);
			this.KeyPointsList.Name = "KeyPointsList";
			this.KeyPointsList.ScrollAlwaysVisible = true;
			this.KeyPointsList.Size = new System.Drawing.Size(195, 196);
			this.KeyPointsList.TabIndex = 2;
			this.KeyPointsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.DrawSelect_ItemCheck);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1297, 523);
			this.Controls.Add(this.Rec_text);
			this.Controls.Add(this.KeyPointsList);
			this.Controls.Add(this.DrawSelect);
			this.Controls.Add(this.MapPictureBox);
			this.Controls.Add(this.MapGenBtn);
			this.Name = "MainForm";
			this.Text = "地图及数据生成工具";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).EndInit();
			this.MapContextMenuStrip.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripMenuItem 路标点ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 障碍点ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 导航路标点ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 估计路标点ToolStripMenuItem;
		private System.Windows.Forms.CheckedListBox KeyPointsList;
	}
}

