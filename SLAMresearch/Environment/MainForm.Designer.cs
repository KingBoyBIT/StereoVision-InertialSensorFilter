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
			this.MapGenBtn = new System.Windows.Forms.Button();
			this.MapPictureBox = new System.Windows.Forms.PictureBox();
			this.DrawSelect = new System.Windows.Forms.CheckedListBox();
			this.Rec_text = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).BeginInit();
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
			this.MapPictureBox.Location = new System.Drawing.Point(374, 12);
			this.MapPictureBox.Name = "MapPictureBox";
			this.MapPictureBox.Size = new System.Drawing.Size(900, 490);
			this.MapPictureBox.TabIndex = 1;
			this.MapPictureBox.TabStop = false;
			this.MapPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.MapPictureBox_Paint);
			this.MapPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapPictureBox_MouseDown);
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
			this.DrawSelect.Location = new System.Drawing.Point(173, 13);
			this.DrawSelect.Name = "DrawSelect";
			this.DrawSelect.Size = new System.Drawing.Size(195, 84);
			this.DrawSelect.TabIndex = 2;
			this.DrawSelect.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.DrawSelect_ItemCheck);
			// 
			// Rec_text
			// 
			this.Rec_text.Location = new System.Drawing.Point(12, 258);
			this.Rec_text.Multiline = true;
			this.Rec_text.Name = "Rec_text";
			this.Rec_text.Size = new System.Drawing.Size(356, 244);
			this.Rec_text.TabIndex = 3;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1286, 519);
			this.Controls.Add(this.Rec_text);
			this.Controls.Add(this.DrawSelect);
			this.Controls.Add(this.MapPictureBox);
			this.Controls.Add(this.MapGenBtn);
			this.Name = "MainForm";
			this.Text = "地图及数据生成工具";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button MapGenBtn;
		private System.Windows.Forms.PictureBox MapPictureBox;
		private System.Windows.Forms.CheckedListBox DrawSelect;
		private System.Windows.Forms.TextBox Rec_text;
	}
}

