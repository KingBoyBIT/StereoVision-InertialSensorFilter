namespace CommProgram
{
	partial class ServerForm
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
			this.StartServiceBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// StartServiceBtn
			// 
			this.StartServiceBtn.Location = new System.Drawing.Point(13, 13);
			this.StartServiceBtn.Name = "StartServiceBtn";
			this.StartServiceBtn.Size = new System.Drawing.Size(75, 23);
			this.StartServiceBtn.TabIndex = 0;
			this.StartServiceBtn.Text = "开启服务";
			this.StartServiceBtn.UseVisualStyleBackColor = true;
			this.StartServiceBtn.Click += new System.EventHandler(this.StartServiceBtn_Click);
			// 
			// ServerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.StartServiceBtn);
			this.Name = "ServerForm";
			this.Text = "ServerForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button StartServiceBtn;
	}
}

