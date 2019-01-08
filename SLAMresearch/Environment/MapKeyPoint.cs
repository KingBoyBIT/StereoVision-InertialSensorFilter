using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environment
{
	public class MapKeyPoint
	{
		public enum ptype:int
		{
			地形边界点,
			障碍边界点,
			导航路标点,
			路径路标点,
			定位点,
			NULL
		}
		public ptype t;
		public PointF p;
		public MapKeyPoint()
		{
			p = new PointF(0, 0);
			t = ptype.地形边界点;
		}

		public MapKeyPoint(PointF p,ptype t)
		{
			this.p = p;
			this.t = t;
		}
		public static double PtDis(Point a,Point b)
		{
			double dis = Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
			return dis;
		}
	}

	/// <summary>
	/// 有向直线连接两个点
	/// </summary>
	public class LineStr
	{
		/// <summary>
		/// 起始点
		/// </summary>
		public MapKeyPoint startpt;
		/// <summary>
		/// 终点
		/// </summary>
		public MapKeyPoint endpt;

		/// <summary>
		/// 创建连接线
		/// </summary>
		/// <param name="startpt">起始点</param>
		/// <param name="endpt">结束点</param>
		public LineStr(MapKeyPoint startpt, MapKeyPoint endpt)
		{
			if (startpt.t==endpt.t)
			{
				this.startpt = startpt;
				this.endpt = endpt;
			}
			else
			{
				MessageBox.Show("不同类型的点连接为一体！");
				
				this.startpt = null;
				this.endpt = null;
			}
		}
	}
}
