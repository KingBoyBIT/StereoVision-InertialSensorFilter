using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environment
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MapGenBtn_Click(object sender, EventArgs e)
		{

		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Rec_text.Text = "操作记录：\r\n";
			this.DrawSelect.SetItemCheckState(3, CheckState.Checked);//默认设置关键点
		}
		/// <summary>
		/// 单点
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapPictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			Rec_text.AppendText("坐标："+e.X.ToString() +" "+e.Y.ToString() + "\r\n");
			
		}

		private void MapPictureBox_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics; //创建画板,这里的画板是由Form提供的.
			Pen p = new Pen(Color.Black, 1);//定义了一个蓝色,宽度为的画笔
			//g.DrawLine(p, 0, 0, this.Width, 0);//在画板上画直线,起始坐标为(10,10),终点坐标为(100,100)
			List<PointF> pts = new List<PointF>();
			pts.Add(new PointF(0, 0));
			pts.Add(new PointF(-MapPictureBox.Width, 0));
			pts.Add(new PointF(-MapPictureBox.Width, MapPictureBox.Height));
			int len = 1;//1个像素的边框大小
			g.DrawRectangle(p, 0, 0, MapPictureBox.Width- len, MapPictureBox.Height- len);

			//g.DrawLines(p, pts.ToArray());
		}
	}
}
