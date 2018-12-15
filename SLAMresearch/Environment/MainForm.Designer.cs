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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(202, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(900, 490);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1114, 517);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.MapGenBtn);
			this.Name = "MainForm";
			this.Text = "地图及数据生成工具";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button MapGenBtn;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

