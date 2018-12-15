using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environment
{
	/// <summary>
	/// 主程序界面
	/// </summary>
	public partial class MainForm : Form
	{
		MouseOpr mo;//控制鼠标范围
		bool paint = false;//是否跟随鼠标绘制
		int size = 4;
		int snapsize = 10;
		public List<PointF> keypoints = new List<PointF>();
		public List<int> delete_pt_idx = new List<int>();

		public MainForm()
		{
			InitializeComponent();
			mo = new MouseOpr();
			mo.globMoCtx = 0;
		}
		/// <summary>
		/// 生成地图
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapGenBtn_Click(object sender, EventArgs e)
		{
			FileStream fs = new FileStream("map.bmp", FileMode.Create);
			Bitmap bp = new Bitmap(this.MapPictureBox.Width, this.MapPictureBox.Height);//实例化一个和窗体一样大的bitmap
			Graphics g = Graphics.FromImage(bp);
			g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
			g.CopyFromScreen(this.MapPictureBox.PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(this.MapPictureBox.Width, this.MapPictureBox.Height));
			//g.CopyFromScreen(this.MapPictureBox.Left, this.Top + this.MapPictureBox.Top, 0, 0, new Size(this.MapPictureBox.Width, this.MapPictureBox.Height));//保存整个窗体为图片
			//bit.Save("weiboTemp.png");

			bp.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
			fs.Close();
			MessageBox.Show("生成成功");
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
			#region 左键事件

			if (e.Button == MouseButtons.Left)
			{
				#region 删除点
				if (this.DrawSelect.GetItemCheckState(4) == CheckState.Checked
						&& this.DrawSelect.GetItemCheckState(3) == CheckState.Unchecked)//删除关键点
				{
					PointF pf = new PointF(e.X, e.Y);
					int width = 10;
					int height = 10;
					for (int i = 0; i < keypoints.Count; i++)
					{
						for (int x = (int)keypoints[i].X - Width; x < (int)keypoints[i].X + width; x++)
						{
							for (int y = (int)keypoints[i].Y - height; y < (int)keypoints[i].Y + height; y++)
							{
								if (x == e.X && y == e.Y)
								{
									delete_pt_idx.Add(i);
								}
							}
						}
					}
					//remove point and draw again
					if (delete_pt_idx.Count == 1)//一次删除一个点
					{
						PointF pfd = keypoints[delete_pt_idx[0]];
						keypoints.RemoveAt(delete_pt_idx[0]);

						Graphics g = this.MapPictureBox.CreateGraphics();
						Pen p = new Pen(Color.White, 1);
						//g.DrawRectangle(p, e.X - size / 2, e.Y - size / 2, size, size);
						Brush b = new SolidBrush(Color.White);
						g.FillRectangle(b, pfd.X - size / 2, pfd.Y - size / 2, size, size);
						delete_pt_idx.Clear();
					}
					else if (delete_pt_idx.Count > 1)//附近有多个点
					{
						SelectForm sf = new SelectForm(this);
						sf.ShowDialog();

						delete_pt_idx.Clear();
					}
					else//没有点
					{
						delete_pt_idx.Clear();
					}
				}
				#endregion
				#region 添加点
				else if (this.DrawSelect.GetItemCheckState(3) == CheckState.Checked
								&& this.DrawSelect.GetItemCheckState(4) == CheckState.Unchecked)//设置关键点
				{
					#region 去重
					foreach (PointF item in keypoints)
					{
						if (item.X==e.X&&item.Y==e.Y)
						{
							Rec_text.AppendText("该点重复！\r\n");
							return;
						}
					}
					#endregion
					PointF pt = new PointF(e.X, e.Y);
					keypoints.Add(pt);
					Graphics g = this.MapPictureBox.CreateGraphics();
					Pen p = new Pen(Color.Red, 1);
					//g.DrawRectangle(p, e.X - size / 2, e.Y - size / 2, size, size);
					Brush b = new SolidBrush(Color.Red);
					g.FillRectangle(b, e.X - size / 2, e.Y - size / 2, size, size);

					Rec_text.AppendText("坐标：" + e.X.ToString() + " " + e.Y.ToString() + "\r\n");
				}
				#endregion
			}
			#endregion
			#region 右键事件
			else if (e.Button == MouseButtons.Right)
			{
				Rec_text.AppendText("坐标：" + e.X.ToString() + " " + e.Y.ToString() + "\r\n");
			}
			#endregion
			#region 其他事件
			else
			{
				//do nothing
			}
			#endregion
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
			g.DrawRectangle(p, 0, 0, MapPictureBox.Width - len, MapPictureBox.Height - len);
			Brush b = new SolidBrush(Color.White);
			g.FillRectangle(b, 0, 0, MapPictureBox.Width, MapPictureBox.Height);
			//g.DrawLines(p, pts.ToArray());
			
			//Bitmap bp = new Bitmap()
		}

		private void DrawSelect_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			//this.DrawSelect.SetItemCheckState(e.Index, e.NewValue);
			if (e.Index == 4 && e.NewValue == CheckState.Checked)
			{
				this.DrawSelect.SetItemCheckState(3, CheckState.Unchecked);
			}
			if (e.Index == 3 && e.NewValue == CheckState.Checked)
			{
				this.DrawSelect.SetItemCheckState(4, CheckState.Unchecked);
			}
		}

		/// <summary>
		/// 鼠标关键点吸附
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapPictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			//Rec_text.AppendText("坐标：" + e.X.ToString() + " " + e.Y.ToString() + "\r\n");
			for (int i = 0; i < keypoints.Count; i++)
			{
				if (keypoints[i].X - snapsize / 2 < e.X &&
					keypoints[i].X + snapsize / 2 > e.X &&
					keypoints[i].Y - snapsize / 2 < e.Y &&
					keypoints[i].Y + snapsize / 2 > e.Y &&
					mo.globMoCtx < snapsize
					)
				{
					mo.MoveMouseToPoint(this.MapPictureBox.PointToScreen(new Point((int)keypoints[i].X, (int)keypoints[i].Y)));
					break;
				}
			}
		}

		private void 路标点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//获取该位置的keypoints
			//keypoints.IndexOf()
		}

		private void MapPictureBox_MouseClick(object sender, MouseEventArgs e)
		{

		}
	}
}
