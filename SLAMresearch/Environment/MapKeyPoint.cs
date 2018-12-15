using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
