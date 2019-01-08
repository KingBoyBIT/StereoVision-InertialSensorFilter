using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Environment
{
	/// <summary>
	/// 地图对象类
	/// </summary>
	public class MapClass
	{
		/// <summary>
		/// 全部地形点
		/// </summary>
		public List<MapKeyPoint> Posptlst;
		/// <summary>
		/// 全部连接线
		/// </summary>
		public List<LineStr> linestr;

		/// <summary>
		/// 创建地图
		/// </summary>
		public MapClass()
		{
			Posptlst = new List<MapKeyPoint>();
			linestr = new List<LineStr>();
		}
	}
}
