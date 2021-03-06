﻿using System;
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
using System.Xml;

namespace Environment
{
	/// <summary>
	/// 主程序界面
	/// </summary>
	public partial class MainForm : Form
	{
		
		MouseOpr mo;//控制鼠标范围
		//bool paint = false;//是否跟随鼠标绘制
		int size = 4;
		int snapsize = 4;
		//bool snapupdate = true;
		//Point presnappt;
		MapKeyPoint curRightPoint;
		MapClass map = new MapClass();
		public List<MapKeyPoint> Posptlst = new List<MapKeyPoint>();
		public List<int> delete_pt_idx = new List<int>();//距离近的被删除点列表
		bool gridon = false;
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
			fs.Flush();
			fs.Close();
			MessageBox.Show("生成成功");
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Rec_text.Text = "操作记录：\r\n";
			this.DrawSelect.SetItemCheckState(3, CheckState.Checked);//默认设置关键点

			this.GridcheckBox.CheckState = CheckState.Unchecked;
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
					for (int i = 0; i < Posptlst.Count; i++)
					{
						for (int x = (int)Posptlst[i].p.X - Width; x < (int)Posptlst[i].p.X + width; x++)
						{
							for (int y = (int)Posptlst[i].p.Y - height; y < (int)Posptlst[i].p.Y + height; y++)
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
						MapKeyPoint pfd = Posptlst[delete_pt_idx[0]];

						Posptlst.RemoveAt(delete_pt_idx[0]);
						reportUI(Posptlst, MapKeyPoint.ptype.定位点);
						clear1point(pfd);
						delete_pt_idx.Clear();
					}
					else if (delete_pt_idx.Count > 1)//附近有多个点
					{
						SelectForm sf = new SelectForm(this);
						sf.Owner = this;
						sf.ShowDialog();
						int ct = 0;
						while (ct < Posptlst.Count)
						{
							if (Posptlst[ct].t == MapKeyPoint.ptype.NULL)
							{
								clear1point(Posptlst[ct]);
								Posptlst.Remove(Posptlst[ct]);
								ct = 0;
							}
							ct++;
						}
						reportUI(Posptlst, MapKeyPoint.ptype.定位点);
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
					foreach (MapKeyPoint item in Posptlst)
					{
						if (item.p.X == e.X && item.p.Y == e.Y)
						{
							Rec_text.AppendText("该点重复！\r\n");
							return;
						}
					}
					#endregion
					PointF pt = new PointF(e.X, e.Y);
					MapKeyPoint p = new MapKeyPoint(pt, MapKeyPoint.ptype.定位点);
					Posptlst.Add(p);
					reportUI(Posptlst, p.t);
					add1point(p);
					Rec_text.AppendText("坐标：" + e.X.ToString() + " " + e.Y.ToString() + "\r\n");
				}
				#endregion
			}
			#endregion
			#region 右键事件
			else if (e.Button == MouseButtons.Right)
			{
				Rec_text.AppendText("右键坐标：" + e.X.ToString() + " " + e.Y.ToString() + "\r\n");
				curRightPoint = null;
				foreach (MapKeyPoint item in Posptlst)
				{
					if (item.p.X==e.X&&item.p.Y==e.Y)
					{
						curRightPoint = item;
					}
				}
				if (curRightPoint==null)
				{
					MessageBox.Show("未找到定位点!");
					return;
				}
			}
			#endregion
			#region 其他事件
			else
			{
				//do nothing
			}
			#endregion
		}
		/// <summary>
		/// 更新界面列表
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		private void reportUI(List<MapKeyPoint> s,MapKeyPoint.ptype t)
		{
			switch (t)
			{
				case MapKeyPoint.ptype.地形边界点:
					this.EdgePointsList.Items.Clear();
					for (int i = 0; i < s.Count; i++)
					{
						if (s[i].t == MapKeyPoint.ptype.地形边界点)
						{
							string str = "地形边界点 " + i.ToString() + " " + s[i].p.X + "," + s[i].p.Y;
							this.EdgePointsList.Items.Add(str);
						}
					}
					break;
				case MapKeyPoint.ptype.障碍边界点:
					break;
				case MapKeyPoint.ptype.导航路标点:
					break;
				case MapKeyPoint.ptype.路径路标点:
					break;
				case MapKeyPoint.ptype.定位点:
					this.PosPointsList.Items.Clear();
					for (int i = 0; i < s.Count; i++)
					{
						if (s[i].t == MapKeyPoint.ptype.定位点)
						{
							string str = "定位点 " + i.ToString() + " " + s[i].p.X + "," + s[i].p.Y;
							this.PosPointsList.Items.Add(str);
						}
					}
					break;
				case MapKeyPoint.ptype.NULL:
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// 增加一个定位点
		/// </summary>
		/// <param name="pfd"></param>
		private void add1point(MapKeyPoint pfd)
		{
			Graphics g = this.MapPictureBox.CreateGraphics();
			//Pen p = new Pen(Color.White, 1);
			//g.DrawRectangle(p, e.X - size / 2, e.Y - size / 2, size, size);
			Brush b;
			switch (pfd.t)
			{
				case MapKeyPoint.ptype.地形边界点:
					b = new SolidBrush(Color.Blue);
					break;
				case MapKeyPoint.ptype.障碍边界点:
					b = new SolidBrush(Color.AliceBlue);
					break;
				case MapKeyPoint.ptype.导航路标点:
					b = new SolidBrush(Color.Green);
					break;
				case MapKeyPoint.ptype.路径路标点:
					b = new SolidBrush(Color.YellowGreen);
					break;
				case MapKeyPoint.ptype.定位点:
					b = new SolidBrush(Color.Red);
					break;
				case MapKeyPoint.ptype.NULL:
					b = new SolidBrush(Color.White);
					break;
				default:
					b = new SolidBrush(Color.White);
					break;
			}
			g.FillRectangle(b, pfd.p.X - size / 2, pfd.p.Y - size / 2, size, size);
			map.Posptlst.Add(pfd);
		}
		/// <summary>
		/// 删除一个定位点
		/// </summary>
		/// <param name="pfd"></param>
		private void clear1point(MapKeyPoint pfd)
		{
			Graphics g = this.MapPictureBox.CreateGraphics();
			Pen p = new Pen(Color.White, 1);
			//g.DrawRectangle(p, e.X - size / 2, e.Y - size / 2, size, size);
			Brush b = new SolidBrush(Color.White);
			g.FillRectangle(b, pfd.p.X - size / 2, pfd.p.Y - size / 2, size, size);
			map.Posptlst.Remove(pfd);
		}
		/// <summary>
		/// 绘制一条边界线
		/// </summary>
		/// <param name="st">起始点</param>
		/// <param name="et">终止点</param>
		private void draw1line(MapKeyPoint st, MapKeyPoint et)
		{
			Graphics g = this.MapPictureBox.CreateGraphics();
			this.Show();
			//Pen p = new Pen(Color.White, 1);
			//g.DrawRectangle(p, e.X - size / 2, e.Y - size / 2, size, size);
			Pen b;
			//Pen pen = new Pen(Color.Red, 5);
			if (st.t!=et.t)
			{
				MessageBox.Show("点类型不一致");
				return;
			}
			//int size = 100;
			switch (st.t)
			{
				case MapKeyPoint.ptype.地形边界点:
					b = new Pen(Color.Gray,size);
					break;
				case MapKeyPoint.ptype.障碍边界点:
					b = new Pen(Color.AliceBlue, size);
					break;
				case MapKeyPoint.ptype.导航路标点:
					b = new Pen(Color.Green, size);
					break;
				case MapKeyPoint.ptype.路径路标点:
					b = new Pen(Color.YellowGreen, size);
					break;
				case MapKeyPoint.ptype.定位点:
					b = new Pen(Color.Red, size);
					break;
				case MapKeyPoint.ptype.NULL:
					b = new Pen(Color.White, size);
					break;
				default:
					b = new Pen(Color.White, size);
					break;
			}
			//g.FillRectangle(b, pfd.p.X - size / 2, pfd.p.Y - size / 2, size, size);
			//g.FillPolygon(b, ps);
			g.DrawLine(b, st.p, et.p);
			LineStr ls = new LineStr(st, et);
			map.linestrlst.Add(ls);
		}

		/// <summary>
		/// 初始化地图画布
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
			for (int i = 0; i < this.DrawSelect.Items.Count; i++)
			{
				if (e.Index == i&&e.NewValue==CheckState.Unchecked)
				{
					this.DrawSelect.SetItemCheckState(i, CheckState.Checked);
				}
				else if (e.Index!=i&&this.DrawSelect.GetItemCheckState(i)==CheckState.Checked)
				{
					this.DrawSelect.SetItemCheckState(i, CheckState.Unchecked);
				}
			}
		}

		/// <summary>
		/// 鼠标关键点吸附
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapPictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			//snapupdate = false;
			//Rec_text.AppendText("坐标：" + e.X.ToString() + " " + e.Y.ToString() + "\r\n");
			this.mouse_pos_label.Text = "鼠标坐标：" + e.X.ToString() + " " + e.Y.ToString();
			for (int i = 0; i < Posptlst.Count; i++)
			{
				if (Posptlst[i].p.X - snapsize / 2.0 < e.X &&
					Posptlst[i].p.X + snapsize / 2.0 > e.X &&
					Posptlst[i].p.Y - snapsize / 2.0 < e.Y &&
					Posptlst[i].p.Y + snapsize / 2.0 > e.Y &&
					mo.globMoCtx < snapsize
					)
				{
					mo.MoveMouseToPoint(this.MapPictureBox.PointToScreen(new Point((int)Posptlst[i].p.X, (int)Posptlst[i].p.Y)));
					return;
				}
			}
			if (gridon == true)//开启栅格化坐标
			{
				int gridsize = Convert.ToInt32(GridSizetextBox.Text);
				Point plt = new Point((int)(e.X / gridsize * gridsize), (int)((e.Y / gridsize * gridsize)));
				Point plb = new Point((int)(e.X / gridsize * gridsize), (int)(((e.Y / gridsize+1) * gridsize)));
				Point prt = new Point((int)((e.X / gridsize+1) * gridsize), (int)((e.Y / gridsize * gridsize)));
				Point prb = new Point((int)((e.X / gridsize+1) * gridsize), (int)(((e.Y / gridsize+1) * gridsize)));
				List<Point> PPlst = new List<Point>();
				PPlst.Add(plt);
				PPlst.Add(plb);
				PPlst.Add(prt);
				PPlst.Add(prb);
				Point cp = new Point(e.X, e.Y);
				double dis = MapKeyPoint.PtDis(cp, PPlst[0]);
				int pct = 0;
				for (int i = 0; i < PPlst.Count; i++)
				{
					double tmpdis = MapKeyPoint.PtDis(cp, PPlst[i]);
					if (tmpdis<dis)
					{
						dis = tmpdis;
						pct = i;
					}
				}
				if (dis <= snapsize
					//&& !(PPlst[pct].X == presnappt.X && PPlst[pct].Y == presnappt.Y)
					)
				{
					mo.MoveMouseToPoint(this.MapPictureBox.PointToScreen(PPlst[pct]));
					//presnappt = PPlst[pct];
					//if (snapupdate == false)
					//{
					//	snapupdate = true;
					//}
					//else
				}
				//else
				//{
				//	snapupdate = true;
				//}
			}
		}

		private void MapPictureBox_MouseClick(object sender, MouseEventArgs e)
		{

		}

		private void GridcheckBox_CheckStateChanged(object sender, EventArgs e)
		{
			if (this.GridcheckBox.CheckState==CheckState.Checked)
			{
				gridon = true;
			}
			else
			{
				gridon = false;
			}
		}

		private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool findflag = false;
			foreach (MapKeyPoint item in Posptlst)
			{
				if (item==curRightPoint)
				{
					clear1point(item);
					Posptlst.Remove(item);
					reportUI(Posptlst, MapKeyPoint.ptype.定位点);
					findflag = true;
					break;
				}
			}
			if (findflag==false)
			{
				MessageBox.Show("未找到需要删除的定位点！");
			}
		}
		
		private void 地形边界点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (curRightPoint != null)
			{
				foreach (MapKeyPoint item in Posptlst)
				{
					if (item == curRightPoint)
					{
						item.t = MapKeyPoint.ptype.地形边界点;
						add1point(item);
						reportUI(Posptlst, MapKeyPoint.ptype.地形边界点);
					}
				}
			}
			else
			{
				MessageBox.Show("未找到定位点！");
			}
		}

		private void backgroundWorkerSerial_DoWork(object sender, DoWorkEventArgs e)
		{

		}

		public void WriteMap()
		{

			string path =("test1.xml");//创建或者覆盖文件
			XmlTextWriter xml = new XmlTextWriter(path, Encoding.UTF8);
			xml.Formatting = Formatting.Indented;
			xml.WriteStartDocument();
			xml.WriteStartElement("MESSAGE");
			xml.WriteStartElement("feedback");

			xml.WriteStartElement("邮箱");
			xml.WriteCData("qwert@163.com");
			xml.WriteEndElement();
			xml.WriteStartElement("电话");
			xml.WriteCData("134000000");
			xml.WriteEndElement();

			xml.WriteEndElement();
			xml.WriteEndElement();
			xml.WriteEndDocument();
			xml.Flush();
			xml.Close();
		}

		private void MapLoadBtn_Click(object sender, EventArgs e)
		{
			//地图数据由XML编写
			#region 默认数据
			MapClass defaultmap = new MapClass();//临时地图
			map.Posptlst.Clear();//清空地图
			map.linestrlst.Clear();//清空地图
			double scale = 18;//地图缩放比例
			double Xoffset = 10;
			double Yoffset = 10;
			//[wall,point,xy]
			double[,,] defaultMapWallData = { { { 0, 0}, { 0, 15} },
							{ { 0, 15}, { 25, 15} },
							{ { 25, 15}, { 25, 0} },
							{ { 25, 0}, { 0, 0} },
							{ { 0, 0}, { 0, 15} },
							{ { 8.5, 15}, { 8.5, 10} },
							{ { 8.5, 10}, { 7.5, 10} },
							{ { 0, 7.5}, { 5, 7.5} },
							{ { 5, 7.5}, { 5.0, 10} },
							{ { 0.0, 10}, { 2.5, 10} },
							{ { 4.5, 10}, { 5, 10} },
							{ { 7.5, 7.5}, { 8.5, 7.5} },
							{ { 8.5, 7.5}, { 8.5, 4.5} },
							{ { 8.5, 2.5}, { 8.5, 0} },
							{ { 8.5, 1.5}, { 9, 1.5} },
							{ { 8.5, 5.5}, { 14.5, 5.5} },
							{ { 11, 5.5}, { 11, 1.5} },
							{ { 11, 1.5}, { 10.5, 1.5} },
							{ { 16.5, 5.5}, { 20, 5.5} },
							{ { 17.5, 5.5}, { 17.5, 0} },
							{ { 10.5, 1.5}, { 11, 1.5} },
							{ { 11, 1.5}, { 11.0, 0} },
							{ { 15.5, 15}, { 15.5, 12.5} },
							{ { 15.5, 10.5}, { 19.5, 10.5} },
							{ { 19.5, 10.5}, { 19.5, 8} },
							{ { 19.5, 8}, { 15.5, 8} },
							{ { 15.5, 8}, { 15.5, 10.5} } };
			MapKeyPoint st = new MapKeyPoint();
			MapKeyPoint et = new MapKeyPoint();
			for (int i = 0; i < defaultMapWallData.GetLength(0); i++)
			{
				for (int j = 0; j < defaultMapWallData.GetLength(1); j++)
				{
					PointF p = new PointF();
					for (int k = 0; k < defaultMapWallData.GetLength(2); k++)
					{
						if (k == 0)
						{
							p.X = (float)(defaultMapWallData[i, j, k] * scale + Xoffset);
						}
						else
						{
							p.Y = (float)(defaultMapWallData[i, j, k] * scale + Yoffset);
						}
					}
					MapKeyPoint mkp = new MapKeyPoint(p, MapKeyPoint.ptype.地形边界点);
					defaultmap.Posptlst.Add(mkp);
					if (j == 0)
					{
						st = mkp;
					}
					else
					{
						et = mkp;
					}
				}
				LineStr ls = new LineStr(st, et);
				defaultmap.linestrlst.Add(ls);
			}
			#endregion
			#region 画布重绘，加载地图
			for (int i = 0; i < defaultmap.Posptlst.Count; i++)
			{
				add1point(defaultmap.Posptlst[i]);
			}
			for (int i = 0; i < defaultmap.linestrlst.Count; i++)
			{
				draw1line(defaultmap.linestrlst[i].startpt, defaultmap.linestrlst[i].endpt);
			}
			#endregion
		}

		private void MapExportBtn_Click(object sender, EventArgs e)
		{
			DataIO.CreateXmlMapFile(map, "data.xml");
			WriteMap();
			MessageBox.Show("导出完成！");
		}

		private void PathLoadBtn_Click(object sender, EventArgs e)
		{
			//仅导入单点
		}
	}
}
