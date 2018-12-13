namespace ImageForm
{
	partial class ImageForm
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
			this.LoadImageBtn = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// LoadImageBtn
			// 
			this.LoadImageBtn.Location = new System.Drawing.Point(12, 12);
			this.LoadImageBtn.Name = "LoadImageBtn";
			this.LoadImageBtn.Size = new System.Drawing.Size(75, 23);
			this.LoadImageBtn.TabIndex = 0;
			this.LoadImageBtn.Text = "载入";
			this.LoadImageBtn.UseVisualStyleBackColor = true;
			this.LoadImageBtn.Click += new System.EventHandler(this.LoadImageBtn_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(13, 42);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(1205, 460);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// ImageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1230, 514);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.LoadImageBtn);
			this.Name = "ImageForm";
			this.Text = "ImageForm";
			this.Load += new System.EventHandler(this.ImageForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button LoadImageBtn;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

